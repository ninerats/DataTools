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
        public string ConnectionString =
            //   @"Data Source=(localdb)\v11.0;Initial Catalog=DataTools;Integrated Security=True";
            @"Data Source=(localdb)\v11.0;Initial Catalog=DataTools;User ID=DataToolsSerTestLogin;Password=Password1";

        [Test,Ignore("only for code generation")]
        public void GenerateTestTables()
        {
            var datSer = new DataTableSerializer(ConnectionString);
            var folder = Directory.CreateDirectory("Export");
            var result = datSer.ExportTable("Job", string.Format(@"{0}\{1}", folder.FullName, "Job.xml"));
            AssertResult(result);
            result = datSer.ExportTable("Person", string.Format(@"{0}\{1}", folder.FullName, "Person.xml"));
            AssertResult(result);
            result = datSer.ExportTable("Tools", string.Format(@"{0}\{1}", folder.FullName, "Tools.xml"));
            AssertResult(result);
            Console.Write(string.Format("tables written to: {0}", folder.FullName));
        }

        [Test]
        public void LoadStandaloneTableTest()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = File.ReadAllText(@"SQL\reset_tools_table.sql");
                conn.ExecSql(sql);
            }
            using (var datSer = new DataTableSerializer(ConnectionString))
            {
                var result = datSer.ImportTable(@"Resources\Tools.xml");
                AssertResult(result);
            }

        }
        [Test]
        public void LoadTableWithConstraintsTest()
        {
            ResetTestTables();
            using (var datSer = new DataTableSerializer(ConnectionString))
            {
                var result = datSer.ImportTableWithBulkCopy(@"Resources\person.xml");
                AssertResult(result);
                AssertConstraintsAreEnabledAndTrusted();
            }
        }

        [Test]
        public void LoadTableWithConstraintsWithBadDataTest()
        {
            ResetTestTables();
            using (var datSer = new DataTableSerializer(ConnectionString))
            {
                var result = datSer.ImportTableWithBulkCopy(@"Resources\person-missing-bob.xml");
                Assert.IsFalse(result.Success);
            }

            AssertConstraintsAreEnabledAndTrusted();

            // make sure all the rows are still there.
            CollectionAssert.AreEquivalent(new[] { "Bob", "Alice", "Ted" }, 
                new SqlConnection(ConnectionString).ExecQuery("SELECT person from person").Rows.Cast<DataRow>().Select(r => r.Field<string>("person")));
        }

        #region helpers
        private void ResetTestTables()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.ExecSql(File.ReadAllText(@"SQL\reset_jobs_and_person.sql"));

            }
        }

        private void AssertResult(ReturnValue result)
        {
            if (!result.Success)
            {
                Assert.Fail(result.ToString());
            }
        }

        private void AssertConstraintsAreEnabledAndTrusted()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var dt = conn.ExecQuery("select name, is_disabled, is_not_trusted from sys.foreign_keys");
                var badones =
                    dt.Rows.Cast<DataRow>().Where(r => r.Field<bool>("is_disabled") || r.Field<bool>("is_not_trusted"));
                CollectionAssert.IsEmpty(badones);
            }
        }
        #endregion
    }
}
