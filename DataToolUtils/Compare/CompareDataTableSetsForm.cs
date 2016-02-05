using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
                var options = TableCompareOptions.None;
                if (chkIgnoreWhitespace.Checked)
                    options = options | TableCompareOptions.IgnoreWhitespace;

                // string rootFolder = Properties.Settings.Default.workspaceRoot;
                ShowStatus("Loading Master Dataset...");
                var masterSetResult = DataTableSet.FromConfigFile(txtMasterDtSet.Text);
                // var masterSetResult = DataTableSet.FromRelativeFolderConfigFile(txtMasterDtSet.Text, rootFolder);
                if (!ShowStatus(masterSetResult, "Loading Master Dataset"))
                    return;
                ShowStatus("Master Dataset loaded.  Creating Replica Dataset...");
                var repSetResult = DataTableSet.FromConfigFile(txtReplicaDtSet.Text, false);
                if (!ShowStatus(repSetResult, "Creating Replica Dataset"))
                    return;
                ShowStatus("Loading Replica Dataset...");
                var replicaSet = repSetResult.Value;
                replicaSet.TableList = masterSetResult.Value.TableList;
                if (!ShowStatus(replicaSet.Load(), "Loading Replica Dataset"))
                    return;
                ShowStatus("Comparing Datasets...");
                var dataSetDiffResult = DataTableComparer.CompareSets(masterSetResult.Value, replicaSet, options);
                if (!ShowStatus(dataSetDiffResult, "Comparing Datasets"))
                    return;
                lvCompareResults.TableSetDiff = dataSetDiffResult.Value;
                lvCompareResults.ShowIdentical = chkShowIdentical.Checked;

            }, "Comparing tables");
        }

        private void ShowStatus(string msg, Color color = default(Color))
        {
            if (color == default(Color)) color = Color.DarkBlue;
            lblStatus.ForeColor = color;
            lblStatus.Text = msg;
            Application.DoEvents();

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
                MessageBox.Show(result.ToString(), context,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowStatus(string.Format("{0} failed.", context), Color.Red);
                Debug.WriteLine(result);
            }
            ShowStatus(string.Format("{0} complete.", context));
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
                ReturnValueForm.Show(selected);
                return;
            }

            DataDiffDetailsForm.Invoke(selected.Value);
        }

        private void cmdTools_Click(object sender, EventArgs e)
        {
            UiTry(() => { new DataToolUtilForm().ShowDialog(); }, "Showing Tools form");
        }

        private void cmdDumpDiff_Click(object sender, EventArgs e)
        {
            string exportPath = "";
            UiTry(() =>
            {
                exportPath = new FileInfo("Compare_report.txt").FullName;
                // File.WriteAllText("TableSetDiffDump.txt",lvCompareResults.TableSetDiff.ToString());
                var items = lvCompareResults.Items.Cast<ListViewItem>()
                    .Select(i => string.Format("{0}\t{1}\t{2}\t{3}",
                        i.Text.PadRight(lvCompareResults.Columns[0].Width / 6),
                         i.Group.Header.PadRight(25),
                         i.SubItems[1].Text.PadRight(lvCompareResults.Columns[1].Width / 5),
                         i.SubItems[2].Text));
                var header = string.Format("Master Dataset: {0}\r\nReplica DataSet: {1}\r\nTables Compared:{2}\r\nComparison ran: {3}",
                    txtMasterDtSet.Text, txtReplicaDtSet.Text, items.Count(), DateTime.Now);
                var details = string.Format("Table                                               Comparison                  Schema              Description\r\n" +
                                            "-------------------------------------------------------------------------------------------------------------------------------------\r\n{0}",
                    string.Join("\r\n", items));

                ;
                File.WriteAllText(exportPath, string.Format("{0}\r\n\r\n{1}", header, details));
            }, string.Format("Dumping compare report to {0}", exportPath));
        }
    }
}
