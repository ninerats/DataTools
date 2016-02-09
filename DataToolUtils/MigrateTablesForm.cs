using System;
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
    public partial class MigrateTablesForm : DataToolsFormBase
    {
        public MigrateTablesForm()
        {
            InitializeComponent();
            StatusLabel = lblStatus;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdMigrateTables_Click(object sender, System.EventArgs e)
        {
            UiWrap(() =>
            {
                // migrate tables from modified to target.
                var workingFolder = txtWorkingFolder.Text;
                if (!Directory.Exists(workingFolder))
                    throw new AbortException(string.Format("The working folder '{0}' does not exist.", txtWorkingFolder));

                var selectedTableNames = lvCompareResults.CheckedItems.Cast<ListViewItem>().Select(lvi => lvi.Text).ToArray();
                if (!selectedTableNames.Any())
                {
                    ShowStatus("No tables selected to migrate.", Color.Yellow);
                    return;
                }

                var source = lvCompareResults.TableSetDiff.MasterDts as DatabaseDataTableSet;
                Contract.Assert(source != null);
                source.TableList = selectedTableNames.ToList();
           

                var msg = string.Format("Exporting tables to '{0}'...", workingFolder);
                ShowStatus(msg);
                ShowStatus(source.ExportTables(workingFolder), msg);

                msg = "Importing tables...";
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
                File.WriteAllText(Path.Combine(workingFolder,"TableMigrationReport.txt"),successMsg);
                MessageBox.Show(string.Format("Migration complete. {0} tables migrated.", importResult.Value.Count())); ;
            }, "Migrating tables");
        }

        private void MigrateTablesForm_Load(object sender, System.EventArgs e)
        {
            UiWrap(() =>
            {
                if (string.IsNullOrEmpty(txtWorkingFolder.Text))
                {
                    txtWorkingFolder.Text = Path.Combine(Path.GetTempPath(), "MigrateDatabase");
                    if (!Directory.Exists(txtWorkingFolder.Text))
                    {
                        Directory.CreateDirectory(txtWorkingFolder.Text);
                    }
                }
            }, "Initializing temp directory");
        }

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        [Obsolete("C&P code is temporary for testing.")]
        private void cmdCompare_Click(object sender, System.EventArgs e)
        {
            UiWrap(() =>
            {
                var options = TableCompareOptions.CaptureValues;
                if (chkIgnoreWhitespace.Checked)
                    options = options | TableCompareOptions.IgnoreWhitespace;

                // string rootFolder = Properties.Settings.Default.workspaceRoot;
                ShowStatus("Loading Source Dataset...");
                var originalDbSet = DataTableSet.FromConfigFile(txtOriginalDataSet.Text, false).AbortOnFail();
                if (originalDbSet.GetType() != typeof (DatabaseDataTableSet))
                {
                    ReturnValue.Abort(string.Format("The underlying data source for '{0}' must be a database.", txtOriginalDataSet.Text));
                }
                // var masterSetResult = DataTableSet.FromRelativeFolderConfigFile(txtMasterDtSet.Text, rootFolder);
                if (!ShowStatus(originalDbSet.Load(), "Loading Original Dataset"))
                    return;
                ShowStatus("Original Dataset loaded.  Loading Modified Dataset...");
                var modifiedDbSet = DataTableSet.FromConfigFile(txtModifiedDataSet.Text).AbortOnFail();
                if (!ShowStatus(modifiedDbSet.Load(), "Loading Modified Dataset"))
                    return;


                ShowStatus("Comparing Datasets...");
                var dataSetDiffResult = DataTableComparer.CompareSets(originalDbSet, modifiedDbSet, options);
                if (!ShowStatus(dataSetDiffResult, "Comparing Datasets"))
                    return;
                lvCompareResults.TableSetDiff = dataSetDiffResult.Value;
                lvCompareResults.ShowIdentical = chkShowIdentical.Checked;

            }, "Comparing tables");
        }

        [Obsolete("C&P code is temporary for testing.")]
        private void cmdViewDetails_Click(object sender, System.EventArgs e)
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

            ShowStatus(DataDiffDetailsForm.Invoke(selected.Value), string.Format("Details for {0}", selected.Value.Master.TableName));
        }

        private void lvCompareResults_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            cmdMigrateTables.Enabled = (lvCompareResults.CheckedItems.Count > 0)
            ;
        }
    }
}
