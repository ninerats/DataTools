using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.DataTools.Forms;

namespace DataTool.Search.Forms
{
    public partial class SelectTableForm : DataToolsFormBase
    {
        protected string DtsConfigPath { get; set; }
        public SelectTableForm()
        {
            InitializeComponent();
            StatusLabel = lblStatus;
        }

        private void cmdConnect_Click(object sender, System.EventArgs e)
        {
            tlvTables.ConnectionString = txtConnectionString.Text;
            tlvTables.Connect();
            tlvTables.SelectAllTables();
        }



        private void SelectTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            UiWrap(() =>
            {
                if (string.IsNullOrEmpty(txtConnectionString.Text)) return;
                var dts = new DatabaseDataTableSet(txtConnectionString.Text);
                if (tlvTables.CheckedItems.Count > 0)
                {
                    dts.TableList =
                        tlvTables.CheckedItems.Cast<ListViewItem>().Select(lvi => lvi.Text).ToList();
                }
                dts.SaveConfig(DtsConfigPath);
            }, "Ok clicked");
        }

        private void SelectTableForm_Load(object sender, System.EventArgs e)
        {
            UiWrap(() =>
            {
                if (File.Exists(DtsConfigPath))
                {
                    LoadFromConfigFile();
                }
            }, "Loanding");
            ;
        }

        private void LoadFromConfigFile()
        {

            var dts = DataTableSet.FromConfigFile(DtsConfigPath, false);
            if (!dts.Success)
            {
                ReturnValueForm.Show(dts);
                return;
            }
            var databaseDts = dts.Value as DatabaseDataTableSet;
            txtConnectionString.Text = databaseDts.ConnStr;

            tlvTables.ConnectionString = txtConnectionString.Text;
            tlvTables.Connect();
            if (databaseDts.TableList.Any())
            {
                tlvTables.SelectTables(databaseDts.TableList);
            }
            else
            {
                tlvTables.SelectAllTables();
            }
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {

        }

        public static void ShowSelect(string dataSourceConfigPath)
        {
            var form = new SelectTableForm {DtsConfigPath = dataSourceConfigPath};
            form.ShowDialog();
        }
    }
}
