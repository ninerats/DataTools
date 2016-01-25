using Craftsmaneer.DataTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Craftsmaneer.DataToolUtils
{
    public partial class DataToolUtilForm : Form
    {
        public DataToolUtilForm()
        {
            InitializeComponent();
        }

        public void Connect()
        {
            lvwTables.Connect(txtConnStr.Text);
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void DataToolUtilForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
        #region Export DataSet

        private void cmdExport_Click(object sender, EventArgs e)
        {
            var datatables = lvwTables.GetDataSet().Tables.Cast<DataTable>();
            foreach (var table in datatables)
            {
                var path = string.Format(@"{0}\{1}.xml", txtExportPath.Text, table.TableName);
                SerializeData(table, path);
            }
        }

        public static void SerializeData(DataTable table, string path)
        {
            table.WriteXml(path, XmlWriteMode.WriteSchema);
        }
        #endregion

        #region Import Dataset

        private void cmdImportDataTable_Click(object sender, EventArgs e)
        {
            ofd.FileName = txtImportDataTablePath.Text;
            var result = ofd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtImportDataTablePath.Text = ofd.FileName;
                var fi = new FileInfo(ofd.FileName);
                var tableName = fi.Name.Substring(0,(fi.Name.Length - fi.Extension.Length));
                var dt = new DataTable();
                dt.ReadXml(txtImportDataTablePath.Text);
                dgvImported.DataSource = dt;
                txtCompareToTable.Text = tableName;
                lblTableName.Text = tableName;
            }
        }

        private void cmdCompare_Click(object sender, EventArgs e)
        {
            var dtImported = (DataTable)dgvImported.DataSource;
          //  var dtActualTable = TableListView.ExecSql(new SqlConnection(txtConnStr.Text), string.Format("select * from {0}", txtCompareToTable.Text) );
            var dtActualTable = new SqlConnection(txtConnStr.Text).GetTable(dtImported.TableName);
            //var delta = CompareDataTables(dtImported, dtActualTable);
            var dtc = new DataTableComparer();
            //!dtImported.PrimaryKey = dtActualTable.PrimaryKey;
            var deltaResult = dtc.Compare(dtImported, dtActualTable);
           if (!deltaResult.Success)
           {
               MessageBox.Show(deltaResult.ToString(), "Error Comparing table");
           } 
           else
           {
               var delta = deltaResult.Value;
               if (delta.DiffType == TableDiffType.IncompatibleSchema)
               {
                   MessageBox.Show(delta.SchemaDiff.ToString(), "Incompatible schema");
               }
               else
               {
                   HighlightDifferences(dgvImported, delta);
               }
           }
        }

        #region Diff highlight
        private void HighlightDifferences(DataGridView dgvImported, TableDiff delta)
        {
            if (delta.DiffType == TableDiffType.IncompatibleSchema)
            {
              //  dgvImported.Enabled = false;
                return;
            }

            var rows= dgvImported.Rows.Cast<DataGridViewRow>();
           
            foreach (var item in delta.RowDiffs)
            {

                var thisRow = rows.FirstOrDefault(r => (r.DataBoundItem as DataRowView).Row == item.Row);

                thisRow.DefaultCellStyle.BackColor = Color.Yellow;

                if (item.DiffType == DiffType.Missing)
                {
                   
                }
            }
        }
        #endregion
        private DataTable CompareDataTables(DataTable FirstDataTable, DataTable SecondDataTable)
        {
            var ResultDataTable = new DataTable();
            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation   
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                
                //Create columns for return table   
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }
                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }
        #endregion
        private void FindMissingRows(DataTable TableA, DataTable TableB)
        {
            var idsNotInB = TableA.AsEnumerable().Select(r => r.Field<int>("id"))
        .Except(TableB.AsEnumerable().Select(r => r.Field<int>("id")));
DataTable TableC = (from row in TableA.AsEnumerable()
                   join id in idsNotInB 
                   on row.Field<int>("id") equals id
                   select row).CopyToDataTable();
        }

        private void miSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwTables.Items)
            {
                item.Checked = true;
            }
        }

        private void miSelectNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwTables.Items)
            {
                item.Checked = false;
            }
        }

        private void cmdMakeConfig_Click(object sender, EventArgs e)
        {
            var tableNames = lvwTables.CheckedItems.Cast<ListViewItem>().Select(lvi => lvi.Text);
            var doc = new XDocument();
            doc.Add(new XElement("DataTableCollection", new XComment(string.Format("Generated by DataFormUtil @ {0}.", DateTime.Now))));
            doc.Root.Add(new XElement("Type", "Database"));
            doc.Root.Add(new XElement("ConnectionString", txtConnStr.Text));
            doc.Root.Add(new XElement("Tables", tableNames.Select(tn => new XElement("Table", tn))));
            doc.Save(string.Format(@"{0}\DataTableCollection.config", txtExportPath.Text));
        }
    }

}