using System.Linq;
using Craftsmaneer.DataTools.Compare;
using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test.Compare
{
    [TestFixture]
    public class IncompatibleSchemaCompareTest
    {
        [Test]
        public void MissingColumnsTest()
        {
            var master = TestHelper.BasicDataTable();
            var replica = TestHelper.BasicDataTableMissingInt1Column();
            var result = new DataTableComparer(master,replica).Compare(TableCompareOptions.AllowIncompatibleSchema | TableCompareOptions.CaptureValues);
            Assert.IsTrue(result.Success);
            var missingRow = result.Value.RowDiffs.Single(rd => rd.DiffType == DiffType.Missing);
            var extraRow = result.Value.RowDiffs.Single(rd => rd.DiffType == DiffType.Extra);
            var moddedRows = result.Value.RowDiffs.Where(rd => rd.DiffType == DiffType.DataMismatch);
            CollectionAssert.AreEquivalent(new[] { 1, 3 }, moddedRows.Select(r => r.Row["pk"]).ToArray());
            var rowWithMissingColumnAndOtherMod = moddedRows.Single(r => r.Row["pk"].Equals(3));
            var diffCells = rowWithMissingColumnAndOtherMod.ColumnDiffs;
            Assert.AreEqual(2, diffCells.Count);
            var missingCell = diffCells.Single(cd => cd.DiffType == DiffType.Missing);
            Assert.AreEqual("int1", missingCell.Column.ColumnName);
            var moddedCell = diffCells.Single(cd => cd.DiffType == DiffType.DataMismatch);
            Assert.AreEqual("dateTime1", moddedCell.Column.ColumnName);
        }

        [Test]
        public void MissingColumnEqualsDbNullTest()
        {
            var master = TestHelper.BasicDataTableWithNullInt1();
            var replica = TestHelper.BasicDataTableMissingInt1Column();
            var result = new DataTableComparer(master,replica).Compare(TableCompareOptions.AllowIncompatibleSchema | TableCompareOptions.CaptureValues);
            Assert.IsFalse(result.Value.RowDiffs.Any());
        }

        [Test]
        public void DontCompareByDefaultTest()
        {
            var master = TestHelper.BasicDataTable();
            var replica = TestHelper.BasicDataTableMissingInt1Column();
            var result = new DataTableComparer(master,replica).Compare();
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The schema for replica 'DataTable1' is not compatible with 'DataTable1' and the AllowIncompatibleSchema option is not set", result.Context);
        }
    }
}
