using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Craftsmaneer.Data;
using Craftsmaneer.Lang;
using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test.IO
{
    public static class DataSerTestHelper
    {
        public static string DataToolsConnectionString =          
            @"Data Source=(localdb)\v11.0;Initial Catalog=DataTools;User ID=DataToolsSerTestLogin;Password=Password1";
        public static string DataDiffConnectionString =          
            @"Data Source=(localdb)\v11.0;Initial Catalog=DataDiff;User ID=DataToolsSerTestLogin;Password=Password1";

        public static void AssertResult(ReturnValue result)
        {
            if (!result.Success)
            {
                Assert.Fail(result.ToString());
            }
        }

        public static void AssertConstraintsAreEnabledAndTrusted()
        {
            using (var conn = new SqlConnection(DataToolsConnectionString))
            {
                conn.Open();
                var dt = conn.ExecQuery("select name, is_disabled, is_not_trusted from sys.foreign_keys");
                var badones =
                    dt.Rows.Cast<DataRow>().Where(r => r.Field<bool>("is_disabled") || r.Field<bool>("is_not_trusted"));
                CollectionAssert.IsEmpty(badones);
            }
        }
    }
}
