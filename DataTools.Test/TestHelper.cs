using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTools.Test
{
    class TestHelper
    {
        public static DataTable GetDataTable1()
        {
            var dt = GetBlankDataTable();
            dt.Rows.Add();
            return dt;
        }

        private static DataTable GetBlankDataTable()
        {
            var dt = new DataTable("DataTable1");
            var pk = new DataColumn("pk", typeof(string));
            pk.Unique = true;
            pk.AllowDBNull = false;
            dt.Columns.Add(pk);
            dt.PrimaryKey = new DataColumn[] {  pk};
            dt.Columns.Add("varchar1", typeof(string));
            return dt;
        }
    }
}
