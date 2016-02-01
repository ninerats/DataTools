using System.Data;
using System.Data.SqlClient;
using System.IO;
using Craftsmaneer.Data;
using Craftsmaneer.DataTools.IO;
using Craftsmaneer.Lang;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test.IO
{
    [TestFixture]
    public class DataTableSerializerTest
    {
        

        [Test,Ignore("only for code generation")]
        public void GenerateTestTables()
        {
            var datSer = new DataTableSerializer(DataSerTestHelper.DataToolsConnectionString);
            var folder = Directory.CreateDirectory("Export");
            var result = datSer.ExportTable("Job", string.Format(@"{0}\{1}", folder.FullName, "Job.xml"));
            DataSerTestHelper.AssertResult(result);
            result = datSer.ExportTable("Person", string.Format(@"{0}\{1}", folder.FullName, "Person.xml"));
            DataSerTestHelper.AssertResult(result);
            result = datSer.ExportTable("Tools", string.Format(@"{0}\{1}", folder.FullName, "Tools.xml"));
            DataSerTestHelper.AssertResult(result);
            Console.Write(string.Format("tables written to: {0}", folder.FullName));
        }

        [Test]
        public void LoadStandaloneTableTest()
        {
            using (var conn = new SqlConnection(DataSerTestHelper.DataToolsConnectionString))
            {
                var sql = File.ReadAllText(@"SQL\reset_tools_table.sql");
                conn.ExecSql(sql);
            }
            using (var datSer = new DataTableSerializer(DataSerTestHelper.DataToolsConnectionString))
            {
                var result = datSer.ImportTable(@"Resources\Tools.xml");
                DataSerTestHelper.AssertResult(result);
            }

        }
        [Test]
        public void LoadTableWithConstraintsTest()
        {
            ResetTestTables();
            using (var datSer = new DataTableSerializer(DataSerTestHelper.DataToolsConnectionString))
            {
                var result = datSer.ImportTableWithBulkCopy(@"Resources\person.xml");
                DataSerTestHelper.AssertResult(result);
                DataSerTestHelper.AssertConstraintsAreEnabledAndTrusted();
            }
        }

        [Test]
        public void LoadTableWithConstraintsWithBadDataTest()
        {
            ResetTestTables();
            using (var datSer = new DataTableSerializer(DataSerTestHelper.DataToolsConnectionString))
            {
                var result = datSer.ImportTableWithBulkCopy(@"Resources\person-missing-bob.xml");
                Assert.IsFalse(result.Success);
            }

            DataSerTestHelper.AssertConstraintsAreEnabledAndTrusted();

            // make sure all the rows are still there.
            CollectionAssert.AreEquivalent(new[] { "Bob", "Alice", "Ted" },
                new SqlConnection(DataSerTestHelper.DataToolsConnectionString).ExecQuery("SELECT person from person").Rows.Cast<DataRow>().Select(r => r.Field<string>("person")));
        }

        #region helpers
        private void ResetTestTables()
        {
            using (var conn = new SqlConnection(DataSerTestHelper.DataToolsConnectionString))
            {
                conn.ExecSql(File.ReadAllText(@"SQL\reset_jobs_and_person.sql"));

            }
        }

      
        #endregion
    }
}
