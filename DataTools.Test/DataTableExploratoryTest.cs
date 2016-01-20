using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class DataTableExploratoryTest
    {
        [Test]
        public void DataTableCopyIsDeep()
        {
            var t1 = TestHelper.BasicDataTable();
            var t2 = t1.Copy();

            var t1R1 = t1.Rows.Find(1);
            var t2R1 = t2.Rows.Find(1);



            Assert.AreNotSame(t1R1, t2R1);

            t1R1["int1"] = 777;
            Assert.AreEqual(55, t2R1["int1"]);
        }

        [Test]
        public void RowLookupByKeyTest()
        {
            var t1 = TestHelper.CompositeKeyTable();

            var copy = t1.Copy();


            var copyRow = copy.Rows.Find(new object[] { "Bob", 2 });


            var keyCols = t1.PrimaryKey;
            var pkVals = keyCols.Select(c => copyRow[c.ColumnName]).ToArray();

            var t1Row = t1.Rows.Find(pkVals);
            Assert.AreEqual("Bob", t1Row["pkstr"]);
            Assert.AreEqual(2, t1Row["pkint"]);

            //var masterRow = t1.Rows.Find(new object[] { "Bob", 2 });





        }
    }
}
