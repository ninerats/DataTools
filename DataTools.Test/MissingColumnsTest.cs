using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using Craftsmaneer.Data;
using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class MissingColumnsTest
    {
        [Test]
        public void SET_REALEC_DOCTYPES_Test()
        {
            string tableName = "EMPOWER.SET_REALEC_DOCTYPES";
            using (
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ExtraTestsConnectionString"]))
            {
                conn.Open();
                var cmd = new SqlCommand(string.Format("SELECT * FROM {0}", tableName), conn);

                using (var dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    var dt = new DataTable(tableName);
                    dt.Load(dr);
                    var actualColumns = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
                    var expectedColumns = new[] { "REALEC_ENUM_VALUE", "DOC_TYPE_ID", "FINALVERSION", "FILEEXT", "DESCRIPTION", "EXCLUDEGDR" };
                    Assert.That(actualColumns, Is.EquivalentTo(expectedColumns));
                }
            }
        }

        [Test]
        public void UseGetTablesTest()
        {
            string tableName = "EMPOWER.SET_REALEC_DOCTYPES";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ExtraTestsConnectionString"]))
            {
                conn.Open();
                var dt = conn.GetTable(tableName);
            }
        }


    }
}