using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.DataToolUtils.Compare;
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

                string rootFolder = Properties.Settings.Default.workspaceRoot;
                var masterSetResult = DataTableSet.FromRelativeFolderConfigFile(txtMasterDtSet.Text, rootFolder);
                if (!ShowStatus(masterSetResult, "Loading Master Dataset"))
                    return;
                var repSetResult = DataTableSet.FromConfigFile(txtReplicaDtSet.Text,false);
                if (!ShowStatus(repSetResult, "Creating Replica Dataset"))
                    return;
                var replicaSet = repSetResult.Value;
                replicaSet.TableList = masterSetResult.Value.TableList;
                if (!ShowStatus(replicaSet.Load(), "Loading Replica Dataset"))
                    return;
                var dataSetDiffResult = DataTableComparer.CompareSets(masterSetResult.Value, replicaSet);
                if (!ShowStatus(dataSetDiffResult, "Comparing Datasets"))
                    return;
                lvCompareResults.TableSetDiff = dataSetDiffResult.Value;

            },"Comparing tables");
        }

        private void UiTry(Action action, string context)
        {
            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Text = context + "...";
            var result = ReturnValue.Wrap(action);
            ShowStatus(result, context);
        }

        private bool ShowStatus(ReturnValue result, string context)
        {
            if (!result.Success)
            {
                MessageBox.Show(result.ToString(),context,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = string.Format("{0} failed.", context);
                lblStatus.ForeColor = Color.Red;
                Debug.WriteLine(result);
            }
            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Text = string.Format("{0} complete.", context);
            return result.Success;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkGroup_CheckedChanged(object sender, EventArgs e)
        {
            lvCompareResults.ShowGroups = chkGroup.Checked;
        }

        private void cmdViewDetails_Click(object sender, EventArgs e)
        {
            var selected = lvCompareResults.SelectedTable();
            if (selected == null)
            {
                MessageBox.Show("Couldn't get underlying data for the selected item.");
                return;
            }

            if (!selected.Success)
            {
                MessageBox.Show("Can't show details for this table because the comparison was unsuccessful.");
                return;
            }

            DataDiffDetailsForm.Invoke(selected.Value);
        }
    }
}
