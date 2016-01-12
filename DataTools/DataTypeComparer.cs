using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Craftsmaneer.DataTools
{
    public class DataTypeComparer
    {
        public TableDiff Compare(DataTable master, DataTable replica)
        {
            SchemaDiff schemaDiff = CompareSchema(master, replica);
            var tableDiff = new TableDiff()
            {
                SchemaDiff = schemaDiff,
                DiffType = TableDiffType.None

            };

            if (!schemaDiff.IsCompatible)
            {
                tableDiff.DiffType = TableDiffType.IncompatibleSchema;
                return tableDiff;
            }

            var dataDiffs = GetRowDiffs(master, replica);

            tableDiff.RowDiffs = dataDiffs;

            return tableDiff;

        }

        public List<RowDiff> GetRowDiffs(DataTable master, DataTable replica)
        {
            var rowDiffs = new List<RowDiff>();

            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { master.Copy(), replica.Copy() });

                //Get Columns for DataRelation   
                var numKeyCols = ds.Tables[0].PrimaryKey.Count();
                DataColumn[] masterCols = new DataColumn[numKeyCols];
                DataColumn[] repCols = new DataColumn[numKeyCols];
                for (int i = 0; i < masterCols.Length; i++)
                {
                    var keyCol = ds.Tables[0].PrimaryKey[i];
                    masterCols[i] = keyCol;
                    repCols[i] = ds.Tables[1].PrimaryKey.First(k => k.ColumnName == keyCol.ColumnName);

                }


                //Create DataRelation   
                DataRelation findMissingRelat = new DataRelation(string.Empty, masterCols, repCols, false);
                ds.Relations.Add(findMissingRelat);

                DataRelation findExtraRelat = new DataRelation(string.Empty, repCols, masterCols, false);
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
                            Row = parentrow
                        });
                    }
                    else if (childrows.Count() > 1)
                    {
                        rowDiffs.Add(new RowDiff()
                        {
                            DiffType = DiffType.TypeMismatch,
                            Row = parentrow
                        });
                    }
                    else
                    {
                        RowDiff matchRows = CompareRowData(parentrow, childrows[0]);
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
                            Row = parentrow
                        });
                }



            }

            return rowDiffs;
        }

        /// <summary>
        /// compares the values of the cells in each row.
        /// Does not do a schema check.
        /// </summary>
        /// <param name="masterRow"></param>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public RowDiff CompareRowData(DataRow masterRow, DataRow repRow)
        {
            var colDiffs = new List<ColumnDiff>();
            var rowDiff = new RowDiff()
            {
                DiffType = DiffType.None,
                ColumnDiffs = colDiffs
            };

            var masterFieldNames = masterRow.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            var replicaFieldNames = repRow.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName);

            var matchFieldNames = masterFieldNames.Intersect(replicaFieldNames);

            foreach (var fieldName in matchFieldNames)
            {
                if (masterRow[fieldName] != repRow[fieldName])
                {
                    rowDiff.DiffType = DiffType.DataMismatch;
                    colDiffs.Add(new ColumnDiff()
                    {
                        Column = masterRow.Table.Columns[fieldName],
                        DiffType = DiffType.DataMismatch
                    });
                }
            }

            if ((matchFieldNames.Count() == masterFieldNames.Count()) && (masterFieldNames.Count() == replicaFieldNames.Count()))
            {
                return rowDiff;
            }

            var missingFieldNames = masterFieldNames.Except(replicaFieldNames);
            foreach (var fieldName in missingFieldNames)
            {
                rowDiff.DiffType = DiffType.DataMismatch;
                colDiffs.Add(new ColumnDiff()
                {
                    Column = masterRow.Table.Columns[fieldName],
                    DiffType = DiffType.Missing
                });
            }

            var extraFieldNames = replicaFieldNames.Except(masterFieldNames);
            foreach (var fieldName in extraFieldNames)
            {
                rowDiff.DiffType = DiffType.DataMismatch;
                colDiffs.Add(new ColumnDiff()
                {
                    Column = masterRow.Table.Columns[fieldName],
                    DiffType = DiffType.Extra
                });
            }

            return rowDiff;
        }

        /// <summary>
        /// Compares the schema of the rows, and if they are compatible, compares the data.
        /// </summary>
        /// <param name="master"></param>
        /// <param name="replica"></param>
        /// <returns></returns>
        public RowDiff CompareRows(DataRow master, DataRow replica)
        {
            var schemaDiff = CompareSchema(master.Table.Columns, replica.Table.Columns);
            if (!schemaDiff.IsCompatible)
            {
                return new RowDiff()
                {
                    DiffType = DiffType.TypeMismatch
                };
            }
            return CompareRowData(master, replica);
        }

        /// <summary>
        /// compares the schemas of the two data tables, and returns any differences.  The master datatable must have primary keys to be comparable.
        /// </summary>
        /// <param name="master"></param>
        /// <param name="replica"></param>
        /// <returns></returns>
        public SchemaDiff CompareSchema(DataTable master, DataTable replica)
        {

            if (!master.PrimaryKey.Any())
            {
                return new SchemaDiff()
                {
                    IsCompatible = false
                };
            }

            return CompareSchema(master.Columns, replica.Columns);

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
