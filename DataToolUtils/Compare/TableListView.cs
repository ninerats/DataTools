using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Craftsmaneer.Data;
using Craftsmaneer.DataTools;

namespace Craftsmaneer.DataToolUtils
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
                _tableInfo = conn.ExecQuery("SELECT t.NAME AS TableName, s.Name AS SchemaName,max( p.rows) AS RowCounts, SUM(a.total_pages) * 8 AS TotalSpaceKB " +
                    "FROM  sys.tables t  " +
                    "INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id " +
                    "INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id " +
                    "INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id " +
                    "LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                    "WHERE (t.NAME NOT LIKE 'dt%') AND (t.is_ms_shipped = 0) AND (i.OBJECT_ID > 255 ) " +
                    "GROUP BY t.Name, s.Name " +
                    "ORDER BY t.Name");
                DisplayTables();
            }


        }

        private void DisplayTables()
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


        public TableListView()
            : base()
        {
            var colTableName = new ColumnHeader()
            {
                Name = "TableName",
                Text = "Table Name",
                Width = 400
            };

            var colRecCount = new ColumnHeader()
            {
                Text = "Records",
                Width = 100,
            };
            var colTableSize = new ColumnHeader()
             {
                 Text = "Table Size",
                 Width = 150,
             };

            CheckBoxes = true;


            /*  var listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
              "Table 1",
              "132"}, -1);*/

            Columns.AddRange(new[] {
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
                    var dt = conn.GetTable(tableName);
                    ds.Tables.Add(dt);

                }
                return ds;
            }
        }

       
    }
}
