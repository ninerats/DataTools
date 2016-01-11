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


            if (schemaDiff.IsCompatible)
            {
                return new TableDiff()
                {
                    DiffType = TableDiffType.IncompatibleSchema
                };
            }

            return null;
        }

        /// <summary>
        /// compares the schemas of the two data tables, and returns any differences.  The master datatable must have primary keys to be comparable.
        /// </summary>
        /// <param name="master"></param>
        /// <param name="replica"></param>
        /// <returns></returns>
        private SchemaDiff CompareSchema(DataTable master, DataTable replica)
        {
            if (!master.PrimaryKey.Any())
            {
                return new SchemaDiff()
                {
                    IsCompatible = false
                };
            }

            var diff = new SchemaDiff() { IsCompatible = true };

            var masterCols = master.Columns.Cast<DataColumn>();
            var repCols = replica.Columns.Cast<DataColumn>();

            var missingColNames = masterCols.Select(col => col.ColumnName).Except(repCols.Select(col => col.ColumnName ) );
            if (missingColNames.Count() > 0)
            {
                diff.ColumnDiffs.AddRange(missingColNames.Select(name => new ColumnDiff()
                {
                    Column = masterCols.First(col => col.ColumnName == name),
                    DiffType = DiffType.Missing
                }));
                diff.IsCompatible = false;
            }

            var extraColNames = repCols.Select(col => col.ColumnName).Except(repCols.Select(col => col.ColumnName));
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
                select new ColumnDiff() {
                    DiffType = DiffType.TypeMismatch,
                    Column = repCol
                };
                
            if (diffCols.Any()) {
                diff.IsCompatible = false;
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
