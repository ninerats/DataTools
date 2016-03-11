using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.DataToolUtils.Compare;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataToolUtils
{
    public partial class CompareDataTableSetsForm : DataToolsFormBase
    {
        public TableSetDiff DataSetDiff { get; set; }

        public CompareDataTableSetsForm()
        {
            InitializeComponent();
            StatusLabel = lblStatus;

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
            UiWrap(() =>
            {
                var options = TableCompareOptions.CaptureValues;
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
                cmdMigrate.Visible = true;

            }, "Comparing tables");
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
            var selected = lvCompareResults.SelectedTableDiff();
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

            ShowStatus( DataDiffDetailsForm.Invoke(selected.Value), string.Format("Details for {0}",selected.Value.Master.TableName));
        }

        private void cmdTools_Click(object sender, EventArgs e)
        {
            UiWrap(() => { new DataToolUtilForm().ShowDialog(); }, "Showing Tools form");
        }

        private void cmdDumpDiff_Click(object sender, EventArgs e)
        {
            string exportPath = "";
            UiWrap(() =>
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

        private void lvCompareResults_DoubleClick(object sender, EventArgs e)
        {
            cmdViewDetails_Click(sender, e);
        }

        #region Migrate

        private void cmdMigrate_Click(object sender, EventArgs e)
        {
            UiWrap(() =>
            {
                lvCompareResults.CheckBoxes = true;
                
                var delta = 173 - pnlTop.Height;
                pnlTop.Height += delta;
                this.Height += delta;
                if (string.IsNullOrEmpty(txtWorkingFolder.Text))
                {
                    txtWorkingFolder.Text = Path.Combine(Path.GetTempPath(), "MigrateDatabase");
                    if (!Directory.Exists(txtWorkingFolder.Text))
                    {
                        Directory.CreateDirectory(txtWorkingFolder.Text);
                    }
                }
            }, "Initializing working folder");
            
        }


        private void cmdExecuteMigration_Click(object sender, System.EventArgs e)
        {
            UiWrap(() =>
            {
                var source = GetSource().AbortOnFail();
                string workingFolder = ExportToWorkingFolder(source).AbortOnFail();
                string msg = "Importing tables...";
                ShowStatus(msg);
                var importSource = new FolderDataTableSet(workingFolder)
                {
                    Id = "Source Import Folder",
                    TableList = source.TableList
                };
                var importResult = importSource.ImportTables(txtTargetDb.Text);
                if (!importResult.Success)
                {
                    ShowStatus(importResult, "Importing tables");
                    return;
                }
                var successMsg = string.Format("Migration to database target '{0}' complete. The following tables were imported:\r\n{1}",
                   txtTargetDb.Text, string.Join("\r\n", importResult.Value.Select(t => "\t" + t)));
                ShowStatus(successMsg);
                File.WriteAllText(Path.Combine(workingFolder, "TableMigrationReport.txt"), successMsg);
                MessageBox.Show(string.Format("Migration complete. {0} tables migrated.", importResult.Value.Count())); ;
            }, "Migrating tables");
        }

        private ReturnValue<string> ExportToWorkingFolder(DatabaseDataTableSet source)
        {
            var workingFolder = txtWorkingFolder.Text;
            return ReturnValue<string>.Wrap(() =>
            {
                if (!Directory.Exists(workingFolder))
                    ReturnValue.Abort(string.Format("The working folder '{0}' does not exist.", txtWorkingFolder));
                var msg = string.Format("Exporting tables to '{0}'...", workingFolder);
                ShowStatus(msg);
                ShowStatus(source.ExportTables(workingFolder), msg);
                return workingFolder;
            }, string.Format("Exporting DatatableSet to '{0}.", workingFolder));
        }

        private ReturnValue<DatabaseDataTableSet> GetSource()
        {
            var selectedTableNames = lvCompareResults.CheckedItems.Cast<ListViewItem>().Select(lvi => lvi.Text).ToArray();
            if (!selectedTableNames.Any())
            {
                return ReturnValue<DatabaseDataTableSet>.FailResult("No tables selected to migrate.");
            }
            var source = new DatabaseDataTableSet(txtSourceDb.Text) ;
            Contract.Assert(source != null);
            source.TableList = selectedTableNames.ToList();
            return ReturnValue<DatabaseDataTableSet>.SuccessResult(source);
        }


        #endregion

        private void lvCompareResults_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            cmdExecuteMigration.Enabled = (lvCompareResults.CheckedItems.Count > 0);
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            UiWrap(() =>
            {
                var source = GetSource().AbortOnFail();
                string workingFolder = ExportToWorkingFolder(source).AbortOnFail();
            }, "Exporting Selectd tables.");
        }
    }
}
