using Craftsmaneer.DataTools.Compare;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class SchemaDiffTest
    {
        [Test]
        public void MatchingFieldsTest()
        {
            var dtMaster = TestHelper.BobTableV1();
            var dtReplica = dtMaster.Copy();
            var dtc = new DataTableComparer(dtMaster,dtReplica);
            var result = dtc.CompareSchema().Value;
            Assert.IsTrue(result.IsCompatible);
            Assert.IsFalse(result.HasDiffs);
            
        }
        [Test]
        public void ExtraFieldsTest()
        {
            var dtMaster = TestHelper.BobTableV1();
            var dtReplica =TestHelper.BobTableAddField();
            var dtc = new DataTableComparer(dtMaster, dtReplica);
            var result = dtc.CompareSchema().Value;
            Assert.IsTrue(result.IsCompatible);
            Assert.IsTrue(result.HasDiffs);
            Assert.AreEqual(DiffType.Extra,  result.ColumnDiffs[0].DiffType);
                 Assert.AreEqual("field5",  result.ColumnDiffs[0].Column.ColumnName);
        }
        [Test]
        public void MissingFieldsTest()
        {
            var dtMaster = TestHelper.BobTableV1();
            var dtReplica =TestHelper.BobTableRemoveField();
            var dtc = new DataTableComparer(dtMaster, dtReplica);
            var result = dtc.CompareSchema().Value;
            Assert.IsFalse(result.IsCompatible);
            Assert.IsTrue(result.HasDiffs);
            Assert.AreEqual(1, result.ColumnDiffs.Count());
            Assert.AreEqual(DiffType.Missing, result.ColumnDiffs[0].DiffType);
            Assert.AreEqual("field3",  result.ColumnDiffs[0].Column.ColumnName);
        }

        [Test]
        public void FieldTypeChangeTest()
        {
            var dtMaster = TestHelper.BobTableV1();
            var dtReplica = TestHelper.BobTableChangeDT();
            var dtc = new DataTableComparer(dtMaster, dtReplica);
            var result = dtc.CompareSchema().Value;
            Assert.IsFalse(result.IsCompatible);
            Assert.IsTrue(result.HasDiffs);
            Assert.AreEqual(1, result.ColumnDiffs.Count());
            Assert.AreEqual(DiffType.TypeMismatch, result.ColumnDiffs[0].DiffType);
            Assert.AreEqual("field1", result.ColumnDiffs[0].Column.ColumnName);
        }

        [Test]
        public void FieldAddAndRemoveTest()
        {
            var dtMaster = TestHelper.BobTableV1();
            var dtReplica = TestHelper.BobTableAddAndRemove();
            var dtc = new DataTableComparer(dtMaster, dtReplica); ;
            var result = dtc.CompareSchema().Value;
            Assert.IsFalse(result.IsCompatible);
            Assert.IsTrue(result.HasDiffs);
            Assert.AreEqual(2, result.ColumnDiffs.Count());
            var extraCol = result.ColumnDiffs.First(cd => cd.DiffType == DiffType.Extra).Column;
            var missingCol = result.ColumnDiffs.First(cd => cd.DiffType == DiffType.Missing).Column;
            Assert.AreEqual("field3", missingCol.ColumnName);
            Assert.AreEqual("field5",extraCol.ColumnName);
        }
    }
}
