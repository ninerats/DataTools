using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Compare
{
    /// <summary>
    ///  Analyses the difference between the data in 2 tables.  Differences are relative to a *master* table.
    /// Will also check schema, to determine if a data compare operation is even valid.
    /// Schemas are considered *compatible* if the data in the replica can be made to match the data in the master.
    /// Comparisons are done between keys.  Only the master DataTable needs to have columns defined in the PrimaryKey property.

    /// </summary>
    public class DataTableComparer
    {
        //HACK: this probably should go somewhere else.
        /// <summary>
        /// compares each table in the master collection with the corresponding replica table.
        /// This version will only compare tables that appear in the master collection table list.
        /// </summary>
        /// <returns></returns>
        public static ReturnValue<TableSetDiff> CompareSets(DataTableSet master, DataTableSet replica, TableCompareOptions options = TableCompareOptions.None)
        {
            var tdDict = new TableSetDiff();
            try
            {
                foreach (DataTable masterTable in master)
                {
                    var replicaTable = replica[masterTable.TableName];
                    ReturnValue<TableDiff> result;
                    if (replicaTable == null)
                    {
                        result = ReturnValue<TableDiff>.FailResult(string.Format(
                            "Table '{0}' was not found in the replica collection [{1}]", masterTable.TableName,
                            replica.Id));
                    }
                    else
                    {
                        var dtc = new DataTableComparer();
                        result = dtc.Compare(masterTable, replicaTable, options);
                    }
                    tdDict.Add(masterTable.TableName, result);
                }
                return ReturnValue<TableSetDiff>.SuccessResult(tdDict);
            }
            catch (Exception ex)
            {
                return ReturnValue<TableSetDiff>.FailResult("Error comparing table ", ex);
            }
        }

        // TODO: promote master / replica to fields
        public ReturnValue<TableDiff> Compare(DataTable master, DataTable replica, TableCompareOptions options = TableCompareOptions.None)
        {
            try
            {

                ReturnValue<SchemaDiff> schemaDiffResult = CompareSchema(master, replica);
                if (!schemaDiffResult.Success)
                    return ReturnValue<TableDiff>.Cascade(schemaDiffResult);

                var schemaDiff = schemaDiffResult.Value;
                var tableDiff = new TableDiff()
                {
                    SchemaDiff = schemaDiff,
                    DiffType = TableDiffType.None

                };

                if (!schemaDiff.IsCompatible)
                {
                    tableDiff.DiffType = TableDiffType.IncompatibleSchema;
                    if (!options.HasFlag(TableCompareOptions.AllowIncompatibleSchema))
                    {
                        return ReturnValue<TableDiff>.FailResult(string.Format("The schema for replica '{0}' is not compatible with '{1}' and the AllowIncompatibleSchema option is not set",
                            replica.TableName, master.TableName));
                    }
                }
                else if (schemaDiff.HasDiffs)
                {
                    tableDiff.DiffType = TableDiffType.CompatibleSchema;
                }

                var dataDiffsResult = GetRowDiffs(master, replica, options);
                if (!dataDiffsResult.Success)
                {
                    ReturnValue<TableDiff>.Cascade(dataDiffsResult,
                        string.Format("Unable to compare rows for {0} and {1}.", master.TableName, replica.TableName));
                }

                tableDiff.RowDiffs = dataDiffsResult.Value;
                if (tableDiff.RowDiffs.Any())
                {
                    tableDiff.DiffType = TableDiffType.Data;
                }


                return ReturnValue<TableDiff>.SuccessResult(tableDiff);
            }
            catch (Exception ex)
            {
                return ReturnValue<TableDiff>.FailResult(string.Format("Unhandled error comparing tables {0}.", master.TableName), ex);
            }

        }
        /// <summary>
        /// returns a list of rows with differences.  will early exit with empty list if schema is not compatible.
        /// </summary>

        /// <returns></returns>
        public ReturnValue<List<RowDiff>> GetRowDiffs(DataTable master, DataTable replica, TableCompareOptions options = TableCompareOptions.None)
        {


            var rowDiffs = new List<RowDiff>();

            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                var hasPk = ((master.PrimaryKey != null) && master.PrimaryKey.Any());
                if (!hasPk && !options.HasFlag(TableCompareOptions.KeysOptional))
                {
                    return ReturnValue<List<RowDiff>>.FailResult("Master table has no Primary Key, and the KeysOptional flag was not set.");
                }

                //Add tables   
                var masterCopy = master.Copy();
                masterCopy.TableName = "master";
                var repCopy = replica.Copy();
                repCopy.TableName = "replica";
                ds.Tables.AddRange(new DataTable[] { masterCopy, repCopy });


                //Get Columns for DataRelation   
                List<DataColumn> keyCols = new List<DataColumn>();
                if (hasPk)
                {
                    keyCols = ds.Tables[0].PrimaryKey.ToList();
                }
                else
                {
                    var fic = FieldsInCommon(master, replica).ToList();
                    keyCols = ds.Tables[0].Columns.Cast<DataColumn>().Where(c => fic.Contains(c.ColumnName)).ToList();
                }
                var numKeyCols = keyCols.Count();
                DataColumn[] masterKeyCols = new DataColumn[numKeyCols];
                DataColumn[] repKeyCols = new DataColumn[numKeyCols];
                for (int i = 0; i < masterKeyCols.Length; i++)
                {
                    var keyCol = keyCols[i];
                    masterKeyCols[i] = keyCol;
                    repKeyCols[i] = ds.Tables[1].Columns.Cast<DataColumn>().First(k => k.ColumnName == keyCol.ColumnName);

                }


                //Create DataRelation   
                DataRelation findMissingRelat = new DataRelation(string.Empty, masterKeyCols, repKeyCols, false);
                ds.Relations.Add(findMissingRelat);

                DataRelation findExtraRelat = new DataRelation(string.Empty, repKeyCols, masterKeyCols, false);
                ds.Relations.Add(findExtraRelat);

                // check for missing rows  and mismatched rows            
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(findMissingRelat);

                    if (childrows == null || childrows.Length == 0)
                    {
                        rowDiffs.Add(new RowDiff()
                        {
                            DiffType = DiffType.Missing,
                            Row = FindMasterRow(parentrow, master)
                        });
                    }
                    else if (childrows.Count() > 1)
                    {
                        rowDiffs.Add(new RowDiff()
                        {
                            DiffType = DiffType.TypeMismatch,
                            Row = FindMasterRow(parentrow, master)
                        });
                    }
                    else
                    {
                        var masterRow = FindMasterRow(parentrow, master);
                        RowDiff matchRows = CompareRowData(masterRow, childrows[0], options);
                        if (matchRows.DiffType != DiffType.None)
                        {
                            rowDiffs.Add(matchRows);

                        }
                    }
                }

                // check for extra rows
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(findExtraRelat);
                    if (childrows == null || childrows.Length == 0)
                        rowDiffs.Add(new RowDiff()
                        {
                            DiffType = DiffType.Extra,
                            Row = FindMasterRow(parentrow, replica)
                        });
                }
            }

            return ReturnValue<List<RowDiff>>.SuccessResult(rowDiffs);
        }

        /// <summary>
        /// finds the row in the master table by keyed look of a copy.
        /// </summary>
        private DataRow FindMasterRow(DataRow copyRow, DataTable master)
        {
            var keyCols = master.PrimaryKey;
            var pkVals = keyCols.Select(c => copyRow[c.ColumnName]).ToArray();
            return master.Rows.Find(pkVals);
        }

        /// <summary>
        /// compares the values of the cells in each row.
        /// Does not do a schema check.
        /// </summary>

        private RowDiff CompareRowData(DataRow masterRow, DataRow repRow, TableCompareOptions options)
        {
            var colDiffs = new List<ColumnDiff>();
            var rowDiff = new RowDiff()
            {
                DiffType = DiffType.None,
                ColumnDiffs = colDiffs,
                Row = masterRow
            };

            //TODO: needs general clean up & optimization.

            var matchFieldNames = FieldsInCommon(masterRow.Table, repRow.Table).ToArray();

            foreach (var fieldName in matchFieldNames)
            {
                var masterValue = masterRow[fieldName];
                var repValue = repRow[fieldName];

                if (!ValuesMatch(masterValue, repValue, options))
                {
                    rowDiff.DiffType = DiffType.DataMismatch;
                    var colDiff = new ColumnDiff()
                    {
                        Column = masterRow.Table.Columns[fieldName],
                        DiffType = DiffType.DataMismatch,


                    };
                    if (options.HasFlag(TableCompareOptions.CaptureValues))
                    {
                        colDiff.ReplicaValue = repValue;
                        colDiff.MasterValue = masterValue;
                    }
                    colDiffs.Add(colDiff);
                }
            }

            if ((matchFieldNames.Count() == masterRow.Table.Columns.Count) && (matchFieldNames.Count() == repRow.Table.Columns.Count))
            {
                return rowDiff;
            }

            var masterFieldNames = masterRow.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();


            var missingFieldNames = masterFieldNames.Except(matchFieldNames).ToArray();
            if (missingFieldNames.Any())
            {
                // this means the table is not schema comptabile.  
                //(question - if the all the values in the master column are missing, could this be considered "compatible" even if it's not 
                // schema compatible?  This might be part of a tighter compatibility check, but for now that will be punted.)
                Contract.Assert(options.HasFlag(TableCompareOptions.AllowIncompatibleSchema), "Missing column in replica during compare, and AllowIncompatibleSchema is not set.");
                
                foreach (var fieldName in missingFieldNames)
                {
                    bool valuesMatch = ValuesMatch(masterRow[fieldName], DBNull.Value, options);
                   if (!valuesMatch)
                    {
                        rowDiff.DiffType = DiffType.DataMismatch;
                        var colDiff = new ColumnDiff()
                        {
                            Column = masterRow.Table.Columns[fieldName],
                            DiffType = DiffType.Missing,


                        };
                        if (options.HasFlag(TableCompareOptions.CaptureValues))
                        {
                            colDiff.ReplicaValue = DBNull.Value;
                            colDiff.MasterValue = masterRow[fieldName];
                        }
                        colDiffs.Add(colDiff);

                    }
                }
            }

            var replicaFieldNames = repRow.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
            var extraFieldNames = replicaFieldNames.Except(matchFieldNames).ToArray();
            if (extraFieldNames.Any())
            {
                
                foreach (var fieldName in extraFieldNames)
                {
                    rowDiff.DiffType = DiffType.DataMismatch;
                    bool valuesMatch = ValuesMatch( DBNull.Value, repRow[fieldName], options);
                   if (!valuesMatch)
                    {
                        var colDiff = new ColumnDiff()
                        {
                            Column = repRow.Table.Columns[fieldName],
                            DiffType = DiffType.Extra,


                        };
                        if (options.HasFlag(TableCompareOptions.CaptureValues))
                        {
                            colDiff.ReplicaValue = repRow[fieldName];
                            colDiff.MasterValue = DBNull.Value;
                        }
                        colDiffs.Add(colDiff);
                    }
                }
            }


            return rowDiff;
        }

        private bool ValuesMatch(object masterValue, object repValue, TableCompareOptions options)
        {
            if (options.HasFlag(TableCompareOptions.TreatDefaultsAsNull))
            {
                // this code will compare the value to the default based on it's datatype, and set it to null if it is the same.
            }
            if (masterValue == null)
            {
                return (repValue == null || repValue== DBNull.Value);
            }

            if (masterValue.GetType().Name == "Byte[]")
            {
                return SlowButSureByteArrayCompare(masterValue as byte[], repValue as byte[]);
            }

            return masterValue.Equals(repValue);
        }

        // http://stackoverflow.com/questions/43289/comparing-two-byte-arrays-in-net/8808245#8808245
        private bool SlowButSureByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }

        private static IEnumerable<string> FieldsInCommon(DataTable master, DataTable replica)
        {
            var masterFieldNames = master.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            var replicaFieldNames = replica.Columns.Cast<DataColumn>().Select(c => c.ColumnName);

            return masterFieldNames.Intersect(replicaFieldNames);//TODO: exclude key fields.
        }

        /// <summary>
        /// Compares the schema of the rows, and if they are compatible, compares the data.
        /// </summary>
        /// <param name="master"></param>
        /// <param name="replica"></param>
        /// <returns></returns>
        public RowDiff CompareRows(DataRow master, DataRow replica, TableCompareOptions options = TableCompareOptions.None)
        {
            var schemaDiff = CompareSchema(master.Table.Columns, replica.Table.Columns);
            if (!schemaDiff.IsCompatible)
            {
                return new RowDiff()
                {
                    DiffType = DiffType.TypeMismatch
                };
            }
            return CompareRowData(master, replica, options);
        }

        /// <summary>
        /// compares the schemas of the two data tables, and returns any differences.  The master datatable must have primary keys to be comparable.
        /// </summary>
        /// <param name="master"></param>
        /// <param name="replica"></param>
        /// <returns></returns>
        public ReturnValue<SchemaDiff> CompareSchema(DataTable master, DataTable replica)
        {

            if (!master.PrimaryKey.Any())
            {

                return ReturnValue<SchemaDiff>.FailResult("Primary key is missing.");
            }

            return ReturnValue<SchemaDiff>.SuccessResult(CompareSchema(master.Columns, replica.Columns));

        }

        public SchemaDiff CompareSchema(DataColumnCollection master, DataColumnCollection replica)
        {



            var diff = new SchemaDiff() { IsCompatible = true };

            var masterCols = master.Cast<DataColumn>();
            var repCols = replica.Cast<DataColumn>();

            var missingColNames = masterCols.Select(col => col.ColumnName).Except(repCols.Select(col => col.ColumnName));
            if (missingColNames.Count() > 0)
            {
                diff.ColumnDiffs.AddRange(missingColNames.Select(name => new ColumnDiff()
                {
                    Column = masterCols.First(col => col.ColumnName == name),
                    DiffType = DiffType.Missing
                }));
                diff.IsCompatible = false;
            }

            var extraColNames = repCols.Select(col => col.ColumnName).Except(masterCols.Select(col => col.ColumnName));
            diff.ColumnDiffs.AddRange(extraColNames.Select(
                colName => new ColumnDiff()
                {
                    Column = repCols.First(col => col.ColumnName == colName),
                    DiffType = DiffType.Extra
                }));

            // naive type checking - an difference in type breaks compatibilty.
            var inCommonNames = masterCols.Select(col => col.ColumnName).Intersect(repCols.Select(col => col.ColumnName));
            var diffCols =
                from masterCol in masterCols
                join repCol in repCols on masterCol.ColumnName equals repCol.ColumnName
                where masterCol.DataType != repCol.DataType
                select new ColumnDiff()
                {
                    DiffType = DiffType.TypeMismatch,
                    Column = repCol
                };

            if (diffCols.Any())
            {
                diff.IsCompatible = false;
                diff.ColumnDiffs.AddRange(diffCols);
            }
            return diff;

            /*
            from o in Orders
from od in o.Order_Details.DefaultIfEmpty()
select new {o, od}
             * */


            /*
            var leftOuterJoinQuery =
 from category in categories
 join prod in products on category.ID equals prod.CategoryID into prodGroup
 from item in prodGroup.DefaultIfEmpty(new Product { Name = String.Empty, CategoryID = 0 })
 select new { CatName = category.Name, ProdName = item.Name };
            */

        }


    }
}
