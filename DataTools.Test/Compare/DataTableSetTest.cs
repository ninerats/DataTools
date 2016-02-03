using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.DataTools.Test.IO;
using Craftsmaneer.Lang;
using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test.Compare
{
    [TestFixture]
    public class DataTableSetTest
    {
        private Stopwatch sw;
        
        [SetUp]
        public void StartTiming()
        {
            sw = new Stopwatch();
            sw.Start();
        }

        [TearDown]
        public void StopTiming()
        {
            Debug.WriteLine(string.Format("elapsed: {0}",sw.Elapsed));
        }

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
            ReturnValue<TableSetDiff> result =
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
           DataSerTestHelper.AssertResult(dbDtcResult);
            DataTableSet folderDtc = dbDtcResult.Value;
            Assert.AreEqual(67, folderDtc.Cast<DataTable>().Count());
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
           var result = DataTableComparer.CompareSets(masterDTC.Value, repDTC.Value);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(TableDiffType.None,
                result.Value.First(kv => kv.Key == "HumanResources.Employee").Value.Value.DiffType);
            ReturnValue<TableDiff> missing = result.Value.First(kv => kv.Key == "HumanResources.Department").Value;
            Assert.IsFalse(missing.Success);
            Assert.AreEqual(missing.Context,
                "Table 'HumanResources.Department' was not found in the replica collection [Folder2DTC]");
        }

        [Test]
        public void ExportTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var exportPath = TestHelper.ResetFolder("Export");
            Directory.CreateDirectory(exportPath);
            ReturnValue<DataTableSet> dtsReturnValue = DataTableSet.FromConfigFile("DatabaseDTC.config",false);
            DataSerTestHelper.AssertResult(dtsReturnValue);
            var exportResult =( dtsReturnValue.Value as DatabaseDataTableSet).ExportTables(exportPath);
            DataSerTestHelper.AssertResult(exportResult);
            Debug.WriteLine(string.Format("Writing tables to {0} took {1}.",exportPath,  sw.Elapsed));
            
        }
    }
}