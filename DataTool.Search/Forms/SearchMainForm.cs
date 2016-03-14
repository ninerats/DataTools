using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Craftsmaneer.DataTools;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.DataTools.Forms;
using Craftsmaneer.Lang;

namespace DataTool.Search.Forms
{
    public partial class SearchMainForm : DataToolsFormBase
    {
        protected DatabaseDataTableSet TableSet { get; set; }

        public string DataSourceConfigPath
        {
            get
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Craftsmaneer.DataSearch", "source.config");
                return folder;
            } 
        }

        public SearchMainForm()
        {
            InitializeComponent();
            StatusLabel = lblStatus;
        }

      

        private void cmdSearch_Click(object sender, System.EventArgs e)
        {
            UiWrap(() =>
            {
                dgvResults.DataSource = new List<SearchResult>();
                var searcher = new DataValueSearcher(TableSet);
                var results = searcher.Search(txtSearchValue.Text);
                dgvResults.DataSource = results;
            }, string.Format("Searching for '{0}'.", txtSearchValue.Text));

        }

        private void SelectTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void cmdSource_Click(object sender, EventArgs e)
        {
            UiWrap(() => SelectTableForm.ShowSelect(DataSourceConfigPath),"Getting Data Source");
        }

        private void SearchMainForm_Load(object sender, EventArgs e)
        {
            UiWrap(() =>
            {
                if (File.Exists(DataSourceConfigPath))
                {
                   Task task = LoadDataSourceAsync();
                }
                ShowDataSourceSummary();
            },"loading Form");
        }

        private async Task LoadDataSourceAsync()
        {
           
            var task = new Task<ReturnValue<DataTableSet>> (() => DataTableSet.FromConfigFile(DataSourceConfigPath));
            var dts = await task;
            if (dts.Success)
            {
                TableSet = dts.Value as DatabaseDataTableSet;
            }
            else
            {
                Log.Debug(dts.ToString());
            }

            var awaiter = task.GetAwaiter();


        }

        private void ShowDataSourceSummary()
        {
            throw new NotImplementedException();
        }
    }
}
