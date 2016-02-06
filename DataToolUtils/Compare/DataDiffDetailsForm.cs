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
            var result = tdgvDelta.Assign(TableDiff);
            if (!result.Success)
            {
                MessageBox.Show(string.Format("The follow error was occurred while highlighting the grid:\r\n\r\n{0})",result),
                    "Error higlighting rows", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            dgvOriginal.DataSource = TableDiff.Master;
            dgvOriginal.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDiffsOnly_Click(object sender, EventArgs e)
        {
            tdgvDelta.DiffsOnly = !tdgvDelta.DiffsOnly;
        }
    }
}
