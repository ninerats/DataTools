using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Craftsmaneer.Data;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Compare.Control
{
    public class DataTableDiffGridView : DataGridView
    {
        private bool _diffsOnly;

        public DataTableDiffGridView()
        {
            DataError += DataTableDiffGridView_DataError;
            Sorted += DataTableDiffGridView_Sorted;
        }

        void DataTableDiffGridView_Sorted(object sender, EventArgs e)
        {
           HighlightRowDiffs();
        }

        [Browsable(false)]
        public TableDiff TableDiff { get; protected set; }

        [Browsable(false)]
        public List<DataRow> DataRows { get; protected set; }

        public bool DiffsOnly

        {
            get { return _diffsOnly; }
            set
            {
                
                if (value != _diffsOnly)
                {
                    try
                    {
                        DataView dv = ((DataTable) DataSource).DefaultView;

                        if (value)
                        {
                            dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        }
                        else
                        {
                            dv.RowStateFilter = DataViewRowState.CurrentRows;
                        }
                        HighlightRowDiffs();
                    }
                    catch (Exception ex)
                    {
                        // log
                        MessageBox.Show(ex.Message);

                    }
                }
                _diffsOnly = value;
            }
        }

        public ReturnValue Assign(TableDiff tdiff)
        {
            return ReturnValue.Wrap(() =>
            {
                if (tdiff.Replica == null) throw new ArgumentNullException("tdiff.Replica");
                TableDiff = tdiff;
                BuildDataSource();
                AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                HighlightDiffs();
            });
        }

        private void BuildDataSource()
        {
            DataRow[] missingRows =
                TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.Missing).Select(rd => rd.Row).ToArray();
            DataTable source = TableDiff.Replica.Copy();
            foreach (DataRow missingRow in missingRows)
            {
                source.Rows.Add(missingRow.ItemArray);
            }
            source.AcceptChanges();
            DataSource = source;
        }

        private void DataTableDiffGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void HighlightDiffs()
        {
            HighlightKeys();
            HighlightSchemaDiffs();
            HighlightRowDiffs();
        }

        private void HighlightKeys()
        {
            foreach (var column in TableDiff.Master.PrimaryKey)
            {
                var col = Columns.Cast<DataGridViewColumn>()
                    .FirstOrDefault(c => c.DataPropertyName == column.ColumnName);
                Contract.Assert(col != null);
                if (col != null)
                {
                    col.DefaultCellStyle.Font = new Font(DefaultFont,FontStyle.Bold);
                    col.HeaderCell.Style.Font = new Font(DefaultFont, FontStyle.Bold);
                    col.HeaderCell.Style.ForeColor = Color.DarkSlateBlue;
                    
                }
            }
        }

        private void HighlightRowDiffs()
        {
            IEnumerable<RowDiff> extraRows = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.Extra);
            foreach (RowDiff rowDiff in extraRows)
            {
                DataGridViewRow thisRow = FindGridViewRow(rowDiff);
                TagRow(thisRow);
                Contract.Assert(thisRow != null);
                thisRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
            }

            IEnumerable<RowDiff> missingRows = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.Missing);
            foreach (RowDiff rowDiff in missingRows)
            {
                DataGridViewRow thisRow = FindGridViewRow(rowDiff);
                Contract.Assert(thisRow != null);
                if (!(thisRow == null))
                {
                    TagRow(thisRow);
                    thisRow.DefaultCellStyle.BackColor = Color.Red;
                    thisRow.DefaultCellStyle.Font = new Font(DefaultCellStyle.Font, FontStyle.Strikeout);
                }
            }


            IEnumerable<RowDiff> moddedRows = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.DataMismatch);
            foreach (RowDiff rowDiff in moddedRows)
            {
                DataGridViewRow thisRow = FindGridViewRow(rowDiff);
                Contract.Assert(thisRow != null);
                TagRow(thisRow);
                if (!(thisRow == null))
                {
                    foreach (ColumnDiff columnDiff in rowDiff.ColumnDiffs)
                    {
                        DataGridViewCell thisCell = FindGridViewCell(thisRow, columnDiff);
                        Contract.Assert(thisCell != null);
                        thisCell.Style.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private static void TagRow(DataGridViewRow thisRow)
        {
            var drv = thisRow.DataBoundItem as DataRowView;
            Contract.Assert(drv != null);
            if (drv.Row.RowState != DataRowState.Modified)
            {
                drv.Row.SetModified();
            }
        }

        private DataGridViewCell FindGridViewCell(DataGridViewRow gvRow, ColumnDiff columnDiff)
        {
            DataGridViewCell cell = gvRow.Cells.Cast<DataGridViewCell>()
                .FirstOrDefault(dgvc => dgvc.OwningColumn.DataPropertyName == columnDiff.Column.ColumnName);
            return cell;
        }

        private DataGridViewRow FindGridViewRow(RowDiff item)
        {
            DataView dv = ((DataTable)DataSource).DefaultView;
            dv.ApplyDefaultSort = true;
            var keyValues = item.Row.KeyValues();
            //int rowIdx = dv.Find(item.Row.KeyValues());
            var dt = (DataTable) DataSource;
            var rowInDataSource = dt.Rows.Find(keyValues);

           // var x = dv.FindRows(keyValues);
            var match = Rows.Cast<DataGridViewRow>().FirstOrDefault(r => ((DataRowView)r.DataBoundItem).Row ==  rowInDataSource);
            if (match == null)
            {
                return Rows[0];
            }
            return match;
            /*
            if (rowIdx >= 0)
            {
                return Rows[rowIdx];
            }
            Contract.Assert(false, string.Format("Row wasn't found for key value: {0}", item.Row.KeyValues()));
            return null;*/
        }


        private void HighlightSchemaDiffs()
        {
            if (!TableDiff.SchemaDiff.HasDiffs)
                return;
            List<ColumnDiff> colDiffs = TableDiff.SchemaDiff.ColumnDiffs;
            foreach (DataGridViewColumn column in Columns)
            {
                ColumnDiff colDiff = colDiffs.FirstOrDefault(c => c.Column.ColumnName == column.DataPropertyName);
                if (colDiff != null)
                {
                    if (colDiff.DiffType == DiffType.Extra)
                    {
                        column.HeaderCell.Style.BackColor = Color.DeepSkyBlue; //! not working.
                        column.HeaderCell.Style.Font = new Font(DefaultFont,FontStyle.Underline); 
                        column.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    }
                }
            }
        }
    }
}