using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.Data
{
    public static class DatabaseExtensions
    {
        public static DataTable GetTableList(this SqlConnection conn)
        {
            var tableInfo = ExecSql(conn, "s.Name AS SchemaName, SELECT t.NAME AS TableName,  " +
                   "FROM  sys.tables t  " +
                   "LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                   "WHERE (t.NAME NOT LIKE 'dt%') AND (t.is_ms_shipped = 0) " +
                   "ORDER BY s.name, t.Name");
            return tableInfo;

        }

        public static DataTable ExecSql(this SqlConnection conn, string sql)
        {
            var cmd = new SqlCommand(sql, conn);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataTable GetTable(this SqlConnection conn,string tableName )
        {

            var sql = string.Format("select * from {0}", tableName);
            var da = new SqlDataAdapter(sql, conn);
            var dt = new DataTable(tableName);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(dt);
            return dt;
        }
    }
}
