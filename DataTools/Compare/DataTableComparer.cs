using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Compare
{
    /// <summary>
    ///     Analyses the difference between the data in 2 tables.  Differences are relative to a *master* table.
    ///     Will also check schema, to determine if a data compare operation is even valid.
    ///     Schemas are considered *compatible* if the data in the replica can be made to match the data in the master.
    ///     Comparisons are done between keys.  Only the master DataTable needs to have columns defined in the PrimaryKey
    ///     property.
    /// </summary>
    public class DataTableComparer
    {

        private readonly DataTable _master;

        private readonly DataTable _replica;
        private string[] _masterFieldNames;
        private string[] _replicaFieldNames;
        private string[] _fieldsInCommon;
        private string[] _extraFieldNames;
        private string[] _missingFieldNames;


        public DataTableComparer(DataTable master, DataTable replica)
        {
            _master = master;
            _replica = replica;
        }

        private IEnumerable<string> MasterFieldNames
        {
            get
            {
                if (_masterFieldNames == null)
                    _masterFieldNames = _master.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
                return _masterFieldNames;
            }
        }

        private IEnumerable<string> ReplicaFieldNames
        {
            get
            {

                if (_replicaFieldNames == null)
                    _replicaFieldNames = _replica.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
                return _replicaFieldNames;
            }
        }

        private string[] FieldsInCommon
        {
            get
            {
                if (_fieldsInCommon == null)
                    _fieldsInCommon = MasterFieldNames.Intersect(ReplicaFieldNames).ToArray();
                return _fieldsInCommon;
            }
        }

        private string[] ExtraFieldNames
        {
            get
            {
                if (_extraFieldNames == null)
                    _extraFieldNames = ReplicaFieldNames.Except(FieldsInCommon.ToArray()).ToArray();
                return _extraFieldNames;
            }
        }

        private string[] MissingFieldNames
        {
            get
            {
                if (_missingFieldNames == null)
                    _missingFieldNames = MasterFieldNames.Except(FieldsInCommon.ToArray()).ToArray();
                return _missingFieldNames;
            }
        }
       


        /// <summary>
        ///     compares each table in the master collection with the corresponding replica table.
        ///     This version will only compare tables that appear in the master collection table list.
        /// </summary>
        /// <returns></returns>
        public static ReturnValue<TableSetDiff> CompareSets(DataTableSet masterDts, DataTableSet replicaDts,
            TableCompareOptions options = TableCompareOptions.None)
        {
            var tdDict = new TableSetDiff(masterDts, replicaDts);
            try
            {
                foreach (DataTable masterTable in masterDts)
                {
                    DataTable replicaTable = replicaDts[masterTable.TableName];
                    ReturnValue<TableDiff> result;
                    if (replicaTable == null)
                    {
                        result = ReturnValue<TableDiff>.FailResult(string.Format(
                            "Table '{0}' was not found in the replica collection [{1}]", masterTable.TableName,
                            replicaDts.Id));
                    }
                    else
                    {
                        var dtc = new DataTableComparer(masterTable, replicaTable);
                        result = dtc.Compare(options);
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

        public ReturnValue<TableDiff> Compare(TableCompareOptions options = TableCompareOptions.None)
        {
            try
            {
                ReturnValue<SchemaDiff> schemaDiffResult = CompareSchema();
                if (!schemaDiffResult.Success)
                    return ReturnValue<TableDiff>.Cascade(schemaDiffResult);

                SchemaDiff schemaDiff = schemaDiffResult.Value;
                var tableDiff = new TableDiff(_master, _replica)
                {
                    SchemaDiff = schemaDiff,
                    DiffType = TableDiffType.None
                };

                if (!schemaDiff.IsCompatible)
                {
                    tableDiff.DiffType = TableDiffType.IncompatibleSchema;
                    if (!options.HasFlag(TableCompareOptions.AllowIncompatibleSchema))
                    {
                        return
                            ReturnValue<TableDiff>.FailResult(
                                string.Format(
                                    "The schema for replica '{0}' is not compatible with '{1}' and the AllowIncompatibleSchema option is not set",
                                    _replica.TableName, _master.TableName));
                    }
                }
                else if (schemaDiff.HasDiffs)
                {
                    tableDiff.DiffType = TableDiffType.CompatibleSchema;
                }

                ReturnValue<List<RowDiff>> dataDiffsResult = GetRowDiffs(options);
                if (!dataDiffsResult.Success)
                {
                    ReturnValue<TableDiff>.Cascade(dataDiffsResult,
                        string.Format("Unable to compare rows for {0} and {1}.", _master.TableName, _replica.TableName));
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
                return
                    ReturnValue<TableDiff>.FailResult(
                        string.Format("Unhandled error comparing tables {0}.", _master.TableName), ex);
            }
        }

        /// <summary>
        ///     returns a list of rows with differences.  will early exit with empty list if schema is not compatible.
        /// </summary>
        /// <returns></returns>
        public ReturnValue<List<RowDiff>> GetRowDiffs(TableCompareOptions options = TableCompareOptions.None)
        {
            var rowDiffs = new List<RowDiff>();

            //use a Dataset to make use of a DataRelation object   
            using (var ds = new DataSet())
            {
                bool hasPk = (_master.PrimaryKey.Any());
                if (!hasPk && !options.HasFlag(TableCompareOptions.KeysOptional))
                {
                    return
                        ReturnValue<List<RowDiff>>.FailResult(
                            "Master table has no Primary Key, and the KeysOptional flag was not set.");
                }

                //Add tables   
                DataTable masterCopy = _master.Copy();
                masterCopy.TableName = "master";
                DataTable repCopy = _replica.Copy();
                repCopy.TableName = "replica";
                ds.Tables.AddRange(new[] { masterCopy, repCopy });


                //Get Columns for DataRelation   
                List<DataColumn> keyCols;
                if (hasPk)
                {
                    keyCols = ds.Tables[0].PrimaryKey.ToList();
                }
                else
                {
                    List<string> fic = FieldsInCommon.ToList();
                    keyCols = ds.Tables[0].Columns.Cast<DataColumn>().Where(c => fic.Contains(c.ColumnName)).ToList();
                }
                int numKeyCols = keyCols.Count();
                var masterKeyCols = new DataColumn[numKeyCols];
                var repKeyCols = new DataColumn[numKeyCols];
                for (int i = 0; i < masterKeyCols.Length; i++)
                {
                    DataColumn keyCol = keyCols[i];
                    masterKeyCols[i] = keyCol;
                    repKeyCols[i] = ds.Tables[1].Columns.Cast<DataColumn>()
                        .First(k => k.ColumnName == keyCol.ColumnName);
                }


                //Create DataRelation   
                var findMissingRelat = new DataRelation(string.Empty, masterKeyCols, repKeyCols, false);
                ds.Relations.Add(findMissingRelat);

                var findExtraRelat = new DataRelation(string.Empty, repKeyCols, masterKeyCols, false);
                ds.Relations.Add(findExtraRelat);

                // check for missing rows  and mismatched rows            
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(findMissingRelat);

                    if (childrows.Length == 0)
                    {
                        rowDiffs.Add(new RowDiff
                        {
                            DiffType = DiffType.Missing,
                            Row = FindRow(_master,parentrow)
                        });
                    }
                    else if (childrows.Count() > 1)
                    {
                        rowDiffs.Add(new RowDiff
                        {
                            DiffType = DiffType.TypeMismatch,
                            Row = FindRow(_master,parentrow)
                        });
                    }
                    else
                    {
                        DataRow masterRow = FindRow(_master,parentrow);
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
                    if (childrows.Length == 0)
                        rowDiffs.Add(new RowDiff
                        {
                            DiffType = DiffType.Extra,
                            Row = FindRow(_replica, parentrow)
                        });
                }
            }

            return ReturnValue<List<RowDiff>>.SuccessResult(rowDiffs);
        }

        /// <summary>
        ///     finds the row in the original table with the same PK value as copyRow.
        /// </summary>
        private static DataRow FindRow(DataTable original, DataRow copyRow)
        {
            DataColumn[] keyCols = original.PrimaryKey;
            object[] pkVals = keyCols.Select(c => copyRow[c.ColumnName]).ToArray();
            return original.Rows.Find(pkVals);
        }

        /// <summary>
        ///     compares the values of the cells in each row.
        ///     Does not do a schema check.
        /// </summary>
        private RowDiff CompareRowData(DataRow masterRow, DataRow repRow, TableCompareOptions options)
        {
            var colDiffs = new List<ColumnDiff>();
            var rowDiff = new RowDiff
            {
                DiffType = DiffType.None,
                ColumnDiffs = colDiffs,
                Row = masterRow
            };

            //TODO: needs general clean up & optimization.

            foreach (string fieldName in FieldsInCommon)
            {

                object masterValue = masterRow[fieldName];
                object repValue = repRow[fieldName];

                if (!ValuesMatch(masterValue, repValue, options))
                {
                    rowDiff.DiffType = DiffType.DataMismatch;
                    var colDiff = new ColumnDiff
                    {
                        Column = _master.Columns[fieldName],
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

            if ((FieldsInCommon.Count() == _master.Columns.Count) &&
                (FieldsInCommon.Count() == _replica.Columns.Count))
            {
                return rowDiff;
            }

            string[] missingFieldNames = _masterFieldNames.Except(FieldsInCommon.ToArray()).ToArray();
            if (missingFieldNames.Any())
            {
                // this means the table is not schema comptabile.  
                //(question - if the all the values in the master column are missing, could this be considered "compatible" even if it's not 
                // schema compatible?  This might be part of a tighter compatibility check, but for now that will be punted.)
                Contract.Assert(options.HasFlag(TableCompareOptions.AllowIncompatibleSchema),
                    "Missing column in replica during compare, and AllowIncompatibleSchema is not set.");

                foreach (string fieldName in missingFieldNames)
                {
                    var masterValue = masterRow[fieldName];
                    bool valuesMatch = ValuesMatch(masterValue, DBNull.Value, options);
                    if (!valuesMatch)
                    {
                        rowDiff.DiffType = DiffType.DataMismatch;
                        var colDiff = new ColumnDiff
                        {
                            Column = _master.Columns[fieldName],
                            DiffType = DiffType.Missing,
                        };
                        if (options.HasFlag(TableCompareOptions.CaptureValues))
                        {
                            colDiff.ReplicaValue = DBNull.Value;
                            colDiff.MasterValue = masterValue;
                        }
                        colDiffs.Add(colDiff);
                    }
                }
            }



            if (ExtraFieldNames.Any())
            {
                foreach (string fieldName in ExtraFieldNames)
                {
                    rowDiff.DiffType = DiffType.DataMismatch;
                    var repValue = repRow[fieldName];
                    bool valuesMatch = ValuesMatch(DBNull.Value, repValue, options);
                    if (!valuesMatch)
                    {
                        var colDiff = new ColumnDiff
                        {
                            Column = _replica.Columns[fieldName],
                            DiffType = DiffType.Extra,
                        };
                        if (options.HasFlag(TableCompareOptions.CaptureValues))
                        {
                            colDiff.ReplicaValue = repValue;
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
                return (repValue == null || repValue == DBNull.Value);
            }

            if (masterValue.GetType().Name == "Byte[]")
            {
                return SlowButSureByteArrayCompare(masterValue as byte[], repValue as byte[]);
            }

            if (masterValue.GetType().Name == "String" && options.HasFlag(TableCompareOptions.IgnoreWhitespace))
            {
                if ((repValue as string) == null)
                {
                    return false;
                }
                return (((string)masterValue).Trim().Equals(((string)repValue).Trim()));
            }

            return masterValue.Equals(repValue);
        }

        // http://stackoverflow.com/questions/43289/comparing-two-byte-arrays-in-net/8808245#8808245
        private bool SlowButSureByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1 == null || a2 == null)
            {
                return (a1 == null && a2 == null);
            }
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }



        /// <summary>
        ///     compares the schemas of the two data tables, and returns any differences.  The master datatable must have primary
        ///     keys to be comparable.
        /// </summary>
        /// <returns></returns>
        public ReturnValue<SchemaDiff> CompareSchema()
        {
            if (!_master.PrimaryKey.Any())
            {
                return ReturnValue<SchemaDiff>.FailResult("Primary key is missing.");
            }

            var diff = new SchemaDiff { IsCompatible = true };

           
            if (MissingFieldNames.Any())
            {
                diff.ColumnDiffs.AddRange(MissingFieldNames.Select(name => new ColumnDiff
                {
                    Column = _master.Columns.Cast<DataColumn>().First(col => col.ColumnName == name),
                    DiffType = DiffType.Missing
                }));
                diff.IsCompatible = false;
            }

           
            diff.ColumnDiffs.AddRange(ExtraFieldNames.Select(
                colName => new ColumnDiff
                {
                    Column = _replica.Columns.Cast<DataColumn>().First(col => col.ColumnName == colName),
                    DiffType = DiffType.Extra
                }));

            // naive type checking - an difference in type breaks compatibilty.
            IEnumerable<ColumnDiff> diffCols =
                from masterCol in _master.Columns.Cast<DataColumn>()
                join repCol in _replica.Columns.Cast<DataColumn>() on masterCol.ColumnName equals repCol.ColumnName
                where masterCol.DataType != repCol.DataType
                select new ColumnDiff
                {
                    DiffType = DiffType.TypeMismatch,
                    Column = repCol
                };

            if (diffCols.Any())
            {
                diff.IsCompatible = false;
                diff.ColumnDiffs.AddRange(diffCols);
            }
            return ReturnValue<SchemaDiff>.SuccessResult(diff);
        }
    }
}