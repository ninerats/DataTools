using System;
using System.Drawing;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataToolUtils
{
    public partial class CompareDataTableSetsForm : Form
    {
        public TableSetDiff DataSetDiff { get; set; }
        public CompareDataTableSetsForm()
        {
            InitializeComponent();
        }


        private void cmdOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CompareDataTableSets_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CompareDataTableSetsForm_Load(object sender, EventArgs e)
        {
           // lvCompareResults.LoadSampleItems();
        }

        private void cmdCompare_Click(object sender, EventArgs e)
        {
            UiTry(() =>
            {
                var masterSet = DataTableSet.FromConfigFile(txtMasterDtSet.Text);
                var repSet = DataTableSet.FromConfigFile(txtReplicaDtSet.Text);
                DataSetDiff = DataTableComparer.CompareSets(masterSet.Value, repSet.Value).Value;
                lvCompareResults.TableSetDiff = DataSetDiff;

            },"Comparing tables");
        }

        private void UiTry(Action action, string context)
        {
            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Text = context + "...";
            var result = ReturnValue.Wrap(action);
            if (!result.Success)
            {
                MessageBox.Show(result.Context + "\r\n\r\nStack Trace:\r\n" + result.Error.StackTrace,
                    result.Error.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = string.Format("{0} failed.", context);
                lblStatus.ForeColor = Color.Red;
            }
            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Text = string.Format("{0} complete.", context);
        }
    }
}
