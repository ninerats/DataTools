using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class DataTableComparerTest
    {
        [Test]
        public void CompareIdenticalTablesTest()
        {
            var master = TestHelper.BasicDataTable();
            var replica = master.Copy();
            var dtc = new DataTableComparer();
            var diff = dtc.Compare(master, replica).Value;

            Assert.IsTrue(diff.SchemaDiff.IsCompatible);
            Assert.IsFalse(diff.SchemaDiff.HasDiffs);
            Assert.IsEmpty(diff.SchemaDiff.ColumnDiffs);
            Assert.IsFalse(diff.HasDiffs);
            Assert.AreEqual(diff.DiffType, TableDiffType.None);
            Assert.IsEmpty(diff.RowDiffs);
           
        }

        [Test]
        public void MissingRowsTest()
        {
            var master = TestHelper.BasicDataTable();
            var replica = TestHelper.BasicDataTableMissingRow();
            var dtc = new DataTableComparer();
            var diff = dtc.Compare(master, replica).Value;

            Assert.AreEqual(TableDiffType.Data, diff.DiffType);
            var rowDiff = diff.RowDiffs.First();
            Assert.AreEqual(3, rowDiff.Row["pk"]);
            Assert.AreEqual(DiffType.Missing, rowDiff.DiffType);
        }

        [Test]
        public void ExtraRowsTest()
        {
            var master = TestHelper.BasicDataTable();
            var replica = TestHelper.BasicDataTableExtraRow();
            var dtc = new DataTableComparer();
            var diff = dtc.Compare(master, replica).Value;

            Assert.AreEqual(TableDiffType.Data, diff.DiffType);
            var rowDiff = diff.RowDiffs.First();
            Assert.AreEqual(99, rowDiff.Row["pk"]);
            Assert.AreEqual(DiffType.Extra, rowDiff.DiffType);
        }

        [Test]
        public void ModdedRowTest()
        {
            var master = TestHelper.BasicDataTable();
            var replica = TestHelper.BasicDataTableModdedRow();
            var dtc = new DataTableComparer();
            var diff = dtc.Compare(master, replica).Value;

            Assert.AreEqual(TableDiffType.Data, diff.DiffType);
            
            var rowDiff = diff.RowDiffs.First();
            Assert.AreEqual(3, rowDiff.Row["pk"]);
            Assert.AreEqual(DiffType.DataMismatch, rowDiff.DiffType);
            Assert.AreEqual(2, rowDiff.ColumnDiffs.Count());

            var colInt1Diff = rowDiff.ColumnDiffs.First(cd => cd.Column.ColumnName == "int1");
            Assert.AreEqual(DiffType.DataMismatch, colInt1Diff.DiffType);
            Assert.AreEqual(67, rowDiff.Row[colInt1Diff.Column] );

            var colDateTime1Diff = rowDiff.ColumnDiffs.First(cd => cd.Column.ColumnName == "dateTime1");
            Assert.AreEqual(DiffType.DataMismatch, colDateTime1Diff.DiffType);
            Assert.AreEqual(new DateTime(2005, 1, 3), rowDiff.Row [colDateTime1Diff.Column]);
        }

         [Test]
        public void AddAndDeleteTest()
        {
            var master = TestHelper.BasicDataTable();
            var replica = TestHelper.BasicDataTableAddAndDelete();
            var dtc = new DataTableComparer();
            var diff = dtc.Compare(master, replica).Value;

            Assert.AreEqual(TableDiffType.Data, diff.DiffType);
            Assert.AreEqual(2, diff.RowDiffs.Count);
             var missingRow = diff.RowDiffs.First(rd => rd.DiffType == DiffType.Missing);
            Assert.AreEqual(3, missingRow.Row["pk"]);

            var extraRow = diff.RowDiffs.First(rd => rd.DiffType == DiffType.Extra);
            Assert.AreEqual(4, extraRow.Row["pk"]);
        }
       
    }
}
