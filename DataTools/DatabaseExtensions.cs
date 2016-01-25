using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools
{
    public static class DatabaseExtensions
    {
        public static DataTable GetTableList(this SqlConnection conn)
        {
            var tableInfo = ExecSql(conn, "SELECT t.NAME AS TableName, s.Name AS SchemaName " +
                   "FROM  sys.tables t  " +
                   "INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id " +
                   "LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                   "WHERE (t.NAME NOT LIKE 'dt%') AND (t.is_ms_shipped = 0) AND (i.OBJECT_ID > 255 ) " +
                   "ORDER BY t.Name");
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
