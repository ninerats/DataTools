using System;
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
                // check for folder existance
                if (!Directory.Exists(txtWorkingFolder.Text))
                    throw new AbortException(string.Format("The working folder '{0}' does not exist.", txtWorkingFolder));

                var selectedTableNames = lvCompareResults.CheckedItems.Cast<ListViewItem>().Select(lvi => lvi.Text);
                var source = new DatabaseDataTableSet(txtSourceDb.Text)
                {
                    Id = "Source Database", TableList = selectedTableNames.ToList()
                };

                var msg = string.Format("Exporting tables to '{0}'...", txtWorkingFolder.Text);
                ShowStatus(msg);
                ShowStatus(source.ExportTables(txtWorkingFolder.Text), msg);

                msg = "Importing tables...";
                ShowStatus(msg);
                var importSource = new FolderDataTableSet(txtWorkingFolder.Text)
                {
                    Id = "Source Import Folder",
                    TableList = source.TableList
                };
                ShowStatus(importSource.ImportTables(txtTargetDb.Text), msg);
                MessageBox.Show("Migration Complete.");
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
                var sourceDbSet = new DatabaseDataTableSet(txtSourceDb.Text);
                // var masterSetResult = DataTableSet.FromRelativeFolderConfigFile(txtMasterDtSet.Text, rootFolder);
                if (!ShowStatus(sourceDbSet.Load(), "Loading Source Dataset"))
                    return;
                ShowStatus("Source Dataset loaded.  Creating Target Dataset...");
                var targetDbSet = new DatabaseDataTableSet(txtTargetDb.Text);
                if (!ShowStatus(targetDbSet.Load(), "Creating Target Dataset"))
                    return;


                ShowStatus("Comparing Datasets...");
                var dataSetDiffResult = DataTableComparer.CompareSets(targetDbSet, sourceDbSet,options);
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
    }
}
