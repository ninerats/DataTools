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
            var loadResult = folderDTC.Value.Load();
            Assert.IsTrue(loadResult.Success);
            var dbDTC = DataTableCollection.FromConfigFile("DatabaseDTC.config");
            loadResult = dbDTC.Value.Load();
            Assert.IsTrue(loadResult.Success);
            var result = DataTableComparer.CompareCollections(folderDTC.Value, dbDTC.Value);
            Assert.IsTrue(result.Success);

            var fails = result.Value.Where(r => !r.Value.Success).ToArray();
            Assert.IsEmpty(fails /*,
                string.Join("\r\n", fails.Select(f => string.Format("{0}, {1}", f.Key, f.Value.ToString()))) */);


            var hasDiffs = result.Value.Where(kv => kv.Value.Value.HasDiffs);
            Assert.IsEmpty(hasDiffs);
            Assert.IsEmpty(result.Value.Values.Where(r => !r.Value.SchemaDiff.IsCompatible));
        }

        [Test]
        [Category("Collection")]
        public void MissingTableTest()
        {
            var masterDTC = DataTableCollection.FromConfigFile("MissingTableMasterDTC.config");
            var loadResult = masterDTC.Value.Load();
            Assert.IsTrue(loadResult.Success);
            var repDTC = DataTableCollection.FromConfigFile("MissingTableReplicaDTC.config");
            loadResult = repDTC.Value.Load();
            Assert.IsTrue(loadResult.Success);
            var result = DataTableComparer.CompareCollections(masterDTC.Value, repDTC.Value);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(TableDiffType.None, result.Value.First(kv => kv.Key == "HumanResources.Employee").Value.Value.DiffType);
            var missing = result.Value.First(kv => kv.Key == "HumanResources.Department").Value;
            Assert.IsFalse(missing.Success);
            Assert.AreEqual(missing.Context, "Table 'HumanResources.Department' was not found in the replica collection [Folder2DTC]");
          
        }
    }
}
