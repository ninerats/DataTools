using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTool.Search.Control
{
    public class DataSearchListView : ListView
    {
        public DataSearchListView()
        {
            var colTableName = new ColumnHeader
            {
                Name = "TableName",
                Text = "Table Name",
                Width = 400
            };

            var colSearchStatus = new ColumnHeader
            {
                Name = "Status",
                Text = "Records",
                Width = 400,
            };
          

           Columns.AddRange(new[]
            {
                colTableName,
                colSearchStatus,
             
            });
            
        }
    }
}
