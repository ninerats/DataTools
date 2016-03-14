using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Craftsmaneer.Data;

using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test.IO
{
    [TestFixture]
    public class ImportingTablesTest
    {

        [Test]
        public void ImportEntireDatabase()
        {
            var datSer = new DataTableSerializer(DataSerTestHelper.DataDiffConnectionString);
            var paths = Directory.GetFiles("FolderDTC").Except(
                new[] {@"FolderDTC\Production.Document.xml" , @"FolderDTC\Production.ProductDocument.xml" });
            var sw = new Stopwatch();
            sw.Start();
            var result = datSer.ImportTables(paths);
            DataSerTestHelper.AssertResult(result);
            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
            
        }

        [Test]
        public void DifferentSchemaTest()
        {
            using (var conn = new SqlConnection(DataSerTestHelper.DataToolsConnectionString))
            {
                conn.ExecSql(File.ReadAllText(@"SQL\reset_BulkLoadTestTable1.sql"));
                var datSer = new DataTableSerializer(DataSerTestHelper.DataToolsConnectionString);
                var result = datSer.ImportTable(@"resources\dbo.BulkLoadTestTable1.xml");
                DataSerTestHelper.AssertResult(result);
                var tableData = conn.ExecQuery("SELECT * FROM BulkLoadTestTable1");
                Assert.AreEqual(3, tableData.Rows.Count);
                var firstRowData = tableData.Rows[0].ItemArray;
                CollectionAssert.AreEqual(new object[] {1, "Alpha", 11, DBNull.Value}, firstRowData);

            }
        }

        [Test]
        public void SpecialSchemaTest()
        {
            using (var conn = new SqlConnection(DataSerTestHelper.DataToolsConnectionString))
            {
                const string tableName = "BulkLoadTestTableSpecial";
                conn.ExecSql(string.Format("DELETE FROM {0}", tableName));
                var datSer = new DataTableSerializer(DataSerTestHelper.DataToolsConnectionString);
                var result = datSer.ImportTable(string.Format(@"resources\dbo.{0}.xml", tableName));
                DataSerTestHelper.AssertResult(result);
                var tableData = conn.ExecQuery(string.Format("SELECT * FROM {0}", tableName));
                Assert.AreEqual(3, tableData.Rows.Count);

            }
        }
    }
}
