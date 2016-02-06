using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Craftsmaneer.Data;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Compare.Control
{
    public class DataTableDiffGridView : DataGridView
    {
        private readonly DataGridViewCellStyle _deletedRowCellStyle = new DataGridViewCellStyle { BackColor = Color.Pink };
        private readonly DataGridViewCellStyle _modCellStyle = new DataGridViewCellStyle { BackColor = Color.Yellow };
        private readonly DataGridViewCellStyle _newRowCellStyle = new DataGridViewCellStyle { BackColor = Color.SkyBlue };
        private bool _diffsOnly;
        // _modCellMap is map indexed by Row, which points to another map of (column name, original value)
        private Dictionary<DataRow, Dictionary<string, object>> _modCellMap;

        public DataTableDiffGridView()
        {
            DataError += DataTableDiffGridView_DataError;
            // Sorted += DataTableDiffGridView_Sorted;
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
                        DataView dv = ((DataTable)DataSource).DefaultView;

                        if (value)
                        {
                            dv.RowStateFilter = DataViewRowState.Added | DataViewRowState.ModifiedCurrent |
                                                DataViewRowState.Deleted;
                        }
                        else
                        {
                            dv.RowStateFilter = DataViewRowState.CurrentRows | DataViewRowState.Deleted;
                        }
                        // HighlightRowDiffs();
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
                AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                CellFormatting += OnCellFormatting;
                HighlightSchemaDiffs();
                HighlightKeys();
            });
        }


        private void BuildDataSource()
        {
            DataRow[] missingRows =
                TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.Missing).Select(rd => rd.Row).ToArray();
            DataTable source = TableDiff.Replica.Copy();
            source.AcceptChanges();
            // deleted rows
            foreach (DataRow missingRow in missingRows)
            {
                DataRow row = source.LoadDataRow(missingRow.ItemArray, true);
                row.Delete();
            }

            // added rows
            IEnumerable<RowDiff> addRowDiffs = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.Extra);
            IEnumerable<DataRow> addedRows = addRowDiffs.Select(r => source.Rows.Find(r.Row.KeyValues()));
            foreach (DataRow row in addedRows)
            {
                row.SetAdded();
            }

            // modded rows, build mod cell map


            RowDiff[] modRowDiffs = TableDiff.RowDiffs.Where(rd => rd.DiffType == DiffType.DataMismatch).ToArray();
            IEnumerable<DataRow> moddedRows = modRowDiffs.Select(r => source.Rows.Find(r.Row.KeyValues()));
            foreach (DataRow row in moddedRows)
            {
                row.SetModified();
            }
            _modCellMap =
                modRowDiffs.Select(rd => new
                {
                    row = source.Rows.Find(rd.Row.KeyValues()),
                    colNames = rd.ColumnDiffs.Select(cd => new
                    {
                        columnName = cd.Column.ColumnName,
                        OriginalValue = cd.MasterValue
                    }).ToDictionary(kv => kv.columnName, kv => kv.OriginalValue)
                }
                    ).ToDictionary(kv => kv.row, kv => kv.colNames);
            source.DefaultView.RowStateFilter = DataViewRowState.CurrentRows | DataViewRowState.Deleted;
            source.DefaultView.ApplyDefaultSort = true;
            DataSource = source;
        }

        private void DataTableDiffGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }




        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            ReturnValue result = ReturnValue.Wrap(() =>
            {
                DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
                DataGridViewRow dgvr = cell.OwningRow;

                var rv = dgvr.DataBoundItem as DataRowView;
                Contract.Assert(rv != null);
                DataRow row = rv.Row;
                if (row.RowState == DataRowState.Added)
                {
                    e.CellStyle.BackColor = _newRowCellStyle.BackColor;
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    e.CellStyle.BackColor = _deletedRowCellStyle.BackColor;
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    string columnName = Columns[e.ColumnIndex].DataPropertyName;
                    Contract.Assert(_modCellMap.ContainsKey(row));
                    if (_modCellMap[row].ContainsKey(columnName))
                    {
                        e.CellStyle.BackColor = _modCellStyle.BackColor;
                        this[e.ColumnIndex, e.RowIndex].ToolTipText =
                            ((_modCellMap[row][columnName] == null) ? "(no value)" : _modCellMap[row][columnName].ToString());
                    }
                }
            }, string.Format("Formatting cell [{0}, {1}].", e.ColumnIndex, e.RowIndex));

            if (!result.Success)
            {
                e.FormattingApplied = false;
                MessageBox.Show(result.ToString());
            }
        }


        private void HighlightKeys()
        {
            foreach (DataColumn column in TableDiff.Master.PrimaryKey)
            {
                DataGridViewColumn col = Columns.Cast<DataGridViewColumn>()
                    .FirstOrDefault(c => c.DataPropertyName == column.ColumnName);
                Contract.Assert(col != null);
                if (col != null)
                {
                    col.DefaultCellStyle.Font = new Font(DefaultFont, FontStyle.Bold);
                    col.HeaderCell.Style.Font = new Font(DefaultFont, FontStyle.Bold);
                    col.HeaderCell.Style.ForeColor = Color.DarkSlateBlue;
                }
            }
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
                        column.HeaderCell.Style.Font = new Font(DefaultFont, FontStyle.Underline);
                        column.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    }
                }
            }
        }
    }
}