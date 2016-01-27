using System.Collections.Generic;
using System.Data;
using System.Linq;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.Lang;
using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class DataTableSetTest
    {
        [Test]
        public void CompareSetsTest()
        {
            ReturnValue<DataTableSet> folderDts = DataTableSet.FromConfigFile("FolderDTC.config");
            //folderDts.Value.TableList = new List<string>() { "Purchasing.Vendor" };
            ReturnValue loadResult = folderDts.Value.Load();
            Assert.IsTrue(loadResult.Success);
            ReturnValue<DataTableSet> dbDtc = DataTableSet.FromConfigFile("DatabaseDTC.config");
            //dbDtc.Value.TableList = folderDts.Value.TableList;
            loadResult = dbDtc.Value.Load();
            Assert.IsTrue(loadResult.Success);
            ReturnValue<Dictionary<string, ReturnValue<TableDiff>>> result =
                DataTableComparer.CompareSets(folderDts.Value, dbDtc.Value, TableCompareOptions.CaptureValues);
            Assert.IsTrue(result.Success);


            //   Assert.AreEqual(1,fails.Count());
            // Assert.AreEqual("Purchasing.Vendor",fails.Single().Key);   


            IEnumerable<KeyValuePair<string, ReturnValue<TableDiff>>> hasDiffs =
                result.Value.Where(kv => kv.Value.Value != null && kv.Value.Value.HasDiffs);
            Assert.IsEmpty(hasDiffs);
            //Debug.WriteLine(hasDiffs.Select( kv => kv.Key).ToArray());
            ReturnValue<TableDiff>[] fails = result.Value.Values.Where(r => !r.Success).ToArray();
            Assert.IsEmpty(result.Value.Values.Where(r => !r.Value.SchemaDiff.IsCompatible));
        }

        [Test]
        public void DatabaseDataTableSetTest()
        {
            ReturnValue<DataTableSet> dbDtcResult = DataTableSet.FromConfigFile("DatabaseDTC.config");
            Assert.IsTrue(dbDtcResult.Success);
            DataTableSet folderDtc = dbDtcResult.Value;
            Assert.AreEqual(70, folderDtc.Cast<DataTable>().Count());
        }

        [Test]
        public void FolderDataTableSetTest()
        {
            ReturnValue<DataTableSet> folderDtcResult = DataTableSet.FromConfigFile("FolderDTC.config");
            Assert.IsTrue(folderDtcResult.Success);
            DataTableSet folderDtc = folderDtcResult.Value;
            Assert.AreEqual(66, folderDtc.Cast<DataTable>().Count());
        }

        [Test]
        public void MissingTableTest()
        {
            ReturnValue<DataTableSet> masterDTC = DataTableSet.FromConfigFile("MissingTableMasterDTC.config");
            ReturnValue loadResult = masterDTC.Value.Load();
            Assert.IsTrue(loadResult.Success);
            ReturnValue<DataTableSet> repDTC = DataTableSet.FromConfigFile("MissingTableReplicaDTC.config");
            loadResult = repDTC.Value.Load();
            Assert.IsTrue(loadResult.Success);
            ReturnValue<Dictionary<string, ReturnValue<TableDiff>>> result =
                DataTableComparer.CompareSets(masterDTC.Value, repDTC.Value);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(TableDiffType.None,
                result.Value.First(kv => kv.Key == "HumanResources.Employee").Value.Value.DiffType);
            ReturnValue<TableDiff> missing = result.Value.First(kv => kv.Key == "HumanResources.Department").Value;
            Assert.IsFalse(missing.Success);
            Assert.AreEqual(missing.Context,
                "Table 'HumanResources.Department' was not found in the replica collection [Folder2DTC]");
        }
    }
}