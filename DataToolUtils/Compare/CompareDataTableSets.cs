using System;
using System.Windows.Forms;

namespace Craftsmaneer.DataToolUtils
{
    public partial class CompareDataTableSets : Form
    {
        public CompareDataTableSets()
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

      
    }
}
