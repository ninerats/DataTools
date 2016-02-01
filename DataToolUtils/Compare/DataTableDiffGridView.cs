using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataToolUtils.Compare
{
    public class DataTableDiffGridView : DataGridView
    {
        [Browsable(false)]
        public TableDiff TableDiff { get; protected set; }

        [Browsable(false)]
        public DataTable MasterTable { get; protected set; }

        public ReturnValue Assign( TableDiff tdiff)
        {
            return ReturnValue.Wrap(() =>
            {
                if (tdiff.Replica == null) throw new ArgumentNullException("tdiff.Replica");
                TableDiff = tdiff;
                MasterTable = tdiff.Replica;
                HighlightDiffs();
            });
        }

        public DataTableDiffGridView()
        {
            DataError += DataTableDiffGridView_DataError;
        }

        void DataTableDiffGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void HighlightDiffs()
        {
            DataSource = MasterTable;
            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells );
            HighlightSchemaDiffs();
            HighlightRowDiffs();
        }

        private void HighlightRowDiffs()
        {
            IEnumerable<RowDiff> extraRows = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.Extra);
            foreach (RowDiff rowDiff in extraRows)
            {
                DataGridViewRow thisRow = FindGridViewRow(rowDiff);
                Contract.Assert(thisRow != null);
                thisRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
               
            }
            if (false)
            {
                {
                    IEnumerable<RowDiff> missingRows = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.Missing);
                    foreach (RowDiff rowDiff in missingRows)
                    {
                        DataGridViewRow thisRow = FindGridViewRow(rowDiff);
                        Contract.Assert(thisRow != null);
                        thisRow.DefaultCellStyle.BackColor = Color.Red;
                        thisRow.DefaultCellStyle.Font = new Font(thisRow.DefaultCellStyle.Font, FontStyle.Strikeout);
                    }
                }
            }

            IEnumerable<RowDiff> moddedRows = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.DataMismatch);
            foreach (RowDiff rowDiff in moddedRows)
            {
                DataGridViewRow thisRow = FindMasterGridViewRow(rowDiff);
                foreach (ColumnDiff columnDiff in rowDiff.ColumnDiffs)
                {
                    DataGridViewCell thisCell = FindGridViewCell(thisRow, columnDiff);
                    Contract.Assert(thisCell != null);
                    thisCell.Style.BackColor = Color.Yellow;
                }
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
            IEnumerable<DataGridViewRow> rows = Rows.Cast<DataGridViewRow>();
            DataGridViewRow thisRow = rows.FirstOrDefault(r => (r.DataBoundItem as DataRowView).Row == item.Row);
            return thisRow;
        }

        private DataGridViewRow FindMasterGridViewRow(RowDiff item)
        {
            IEnumerable<DataGridViewRow> rows = Rows.Cast<DataGridViewRow>();
        
            object[] keyValues = GetKeyValues(item.Row);

            var dataRow = TableDiff.Replica.Rows.Find(keyValues);
            DataGridViewRow thisRow = rows.FirstOrDefault(r => (r.DataBoundItem as DataRowView).Row == dataRow);
            return thisRow;
        }

        private object[] GetKeyValues(DataRow row)
        {
            var keyColumns = row.Table.PrimaryKey;
            return keyColumns.Select(dataColumn => row[dataColumn]).ToArray();
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
                        column.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}