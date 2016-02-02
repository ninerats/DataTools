using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataToolUtils.Compare
{
    public partial class DataDiffDetailsForm : Form
    {
        public DataDiffDetailsForm()
        {
            InitializeComponent();
        }

        public static ReturnValue Invoke( TableDiff tdiff)
        {
            return ReturnValue.Wrap(() =>
            {
                var form = new DataDiffDetailsForm
                {
                    lblTableName = {Text = tdiff.Master == null ? "" : tdiff.Master.TableName},
                    TableDiff = tdiff,
                   
                    
                };

                form.ShowDialog();
            });
        }

        public TableDiff TableDiff { get; private set; }


        private void DataDiffDetailsForm_Shown(object sender, EventArgs e)
        {
            tdgvDelta.Assign(TableDiff);
            dgvOriginal.DataSource = TableDiff.Master;
            dgvOriginal.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
