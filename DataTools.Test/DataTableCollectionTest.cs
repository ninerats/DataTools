using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class DataTableCollectionTest
    {

        [Test]
        public void FolderDataTableCollectionTest()
        {
            var folderDtcResult = DataTableCollection.FromConfigFile("FolderDTC.config");
            Assert.IsTrue(folderDtcResult.Success);
            var folderDtc = folderDtcResult.Value;
            Assert.AreEqual(91, folderDtc.Cast<DataTable>().Count());

        }

        [Test]
        public void DatabaseDataTableCollectionTest()
        {
            var dbDtcResult = DataTableCollection.FromConfigFile("DatabaseDTC.config");
            Assert.IsTrue(dbDtcResult.Success);
            var folderDtc = dbDtcResult.Value;
            Assert.AreEqual(91, folderDtc.Cast<DataTable>().Count());

        }

        [Test]
        [Category("Collection")]
        public void CompareCollections()
        {
            var folderDTC = DataTableCollection.FromConfigFile("FolderDTC.config");
            var dbDTC = DataTableCollection.FromConfigFile("DatabaseDTC.config");
            var result = DataTableComparer.CompareCollections(folderDTC.Value, dbDTC.Value);
            Assert.IsTrue(result.Success);

        }
    }
}
