using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataToolUtils
{
    public class TableListView : ListView
    {
        private DataTable _tableInfo;
        public string ConnectionString { get; set; }
        public void Connect()
        {
            if (ConnectionString == null)
                return;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                _tableInfo = ExecSql(conn, "SELECT t.NAME AS TableName, s.Name AS SchemaName, p.rows AS RowCounts, SUM(a.total_pages) * 8 AS TotalSpaceKB " +
                    "FROM  sys.tables t  " +
                    "INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id " +
                    "INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id " +
                    "INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id " +
                    "LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                    "WHERE (t.NAME NOT LIKE 'dt%') AND (t.is_ms_shipped = 0) AND (i.OBJECT_ID > 255 ) " +
                    "GROUP BY t.Name, s.Name, p.Rows " +
                    "ORDER BY t.Name");
                DisplayTables(conn);
            }


        }

        private void DisplayTables(SqlConnection conn)
        {
            Items.Clear();
            var rows = _tableInfo.Rows.Cast<DataRow>();
            rows = rows.OrderBy(r => r["SchemaName"] + "." + r["TableName"]);
            foreach (var table in rows)
            {
                var li = new ListViewItem(new string[] { string.Format("{0}.{1}", table["SchemaName"], table["TableName"]), 
                    string.Format("{0}",table["RowCounts"]),
                    string.Format("{0}",table["TotalSpaceKB"])});
                Items.Add(li);
            }
        }

        private int GetTableRecordCount(SqlConnection conn, DataRow table)
        {
            string sql = string.Format("SELECT COUNT(*) as recCount FROM {0}.{1}", table["TABLE_SCHEMA"], table["TABLE_NAME"]);
            DataTable result = ExecSql(conn, sql);
            return result.Rows[0].Field<int>("recCount");
        }

        public static DataTable ExecSql(SqlConnection conn, string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public TableListView()
            : base()
        {
            var colTableName = new System.Windows.Forms.ColumnHeader()
            {
                Name = "TableName",
                Text = "Table Name",
                Width = 300
            };

            var colRecCount = new System.Windows.Forms.ColumnHeader()
            {
                Text = "Records",
                Width = 100,
            };
            var colTableSize = new System.Windows.Forms.ColumnHeader()
             {
                 Text = "Table Size",
                 Width = 150,
             };

            CheckBoxes = true;


            /*  var listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
              "Table 1",
              "132"}, -1);*/

            Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            colTableName,
            colRecCount,
            colTableSize});
            //listViewItem1.StateImageIndex = 0;
            /* Items.AddRange(new System.Windows.Forms.ListViewItem[] {
             listViewItem1});*/


        }

        public void Connect(string connStr)
        {
            ConnectionString = connStr;
            Connect();
        }
        public DataSet GetDataSet()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var ds = new DataSet("SelectedTables");

                foreach (var item in this.CheckedItems.Cast<ListViewItem>())
                {
                    var tableName = item.SubItems[0].Text;
                    var dt = GetTable(tableName,conn);
                    ds.Tables.Add(dt);

                }
                return ds;
            }
        }

        public static DataTable GetTable(string tableName, SqlConnection conn)
        {
            
            var sql = string.Format("select * from {0}", tableName);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            var dt = new DataTable(tableName);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(dt);
            return dt;
        }
    }
}
