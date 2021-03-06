﻿using System;
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
            var tableInfo = ExecQuery(conn, "SELECT s.Name AS SchemaName,  t.NAME AS TableName  " +
                   "FROM  sys.tables t  " +
                   "LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                   "WHERE (t.NAME NOT LIKE 'dt%') AND (t.is_ms_shipped = 0) " +
                   "ORDER BY s.name, t.Name");
            return tableInfo;

        }

        public static DataTable ExecQuery(this SqlConnection conn, string sql)
        {
            var cmd = new SqlCommand(sql, conn);
            var da = new SqlDataAdapter(cmd);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static int ExecSql(this SqlConnection conn, string sql, SqlTransaction tran = null)
        {
           
            SqlCommand cmd;
            if (tran != null)
            {
                cmd = new SqlCommand(sql, conn,tran);
            }
            else
            {
                cmd = new SqlCommand(sql, conn);
            }
           
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var result = cmd.ExecuteNonQuery();
            return result;
        }

        public static DataTable GetTable(this SqlConnection conn, string tableName)
        {


            var cmd = new SqlCommand(string.Format("SELECT * FROM {0}", tableName), conn);
           
            using (var dr = cmd.ExecuteReader(CommandBehavior.KeyInfo ))
            {
                var dt = new DataTable(tableName);
                dt.Load(dr);
                return dt;
            }
            
        }

        public static object[] KeyValues(this DataRow row)
        {
            return row.Table.PrimaryKey.Select(c => row[c.ColumnName]).ToArray();
        }

       
    }
}
