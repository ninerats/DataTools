using System.Collections.Generic;
using System.Data;
using System.Linq;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Compare
{

    /// <summary>
    /// a set of TableComparisons bewteen to DataTableSets.
    /// </summary>
    public class TableSetDiff : Dictionary<string, ReturnValue<TableDiff>>
    {
        public DataTableSet MasterDts { get; private set; }
        public DataTableSet ReplicaDts { get; private set; }

        public TableSetDiff(DataTableSet masterDts, DataTableSet replicaDts)
        {
            MasterDts = masterDts;
            ReplicaDts = replicaDts;
           
        }

        public override string ToString()
        {
            var reports = this.Select(kv => string.Format(
                "Table {0}: {1}", kv.Key, (kv.Value.Success ? kv.Value.Value.ToString() : kv.Value.ToString())));
            return string.Join("\r\n", reports);
        }
    }

    /// <summary>
    /// structure the contains infomation about a tables differences, compared to a *master* table.
    /// Comparisons are default key based.  The TableCompareOptions enum controls how comparing works.
    /// </summary>
    public class TableDiff
    {
        public TableDiffType DiffType { get; set; } // TODO: this enum is akward, and might reduntant.  rework.
        public SchemaDiff SchemaDiff { get; set; }
        public List<RowDiff> RowDiffs { get; set; }
        public DataTable Master { get; private set; }
        public DataTable Replica { get; private set; }

        public TableDiff()
        {
            SchemaDiff = new SchemaDiff();
            RowDiffs = new List<RowDiff>();
        }

        public TableDiff(DataTable master, DataTable replica): this()
        {
            Master = master;
            Replica = replica;
        }

        public bool HasDiffs
        {
            get
            {
                return SchemaDiff.HasDiffs || RowDiffs.Any();
            }
        }

        public override string ToString()
        {
            var working = string.Format("Master Table: {0}.  Diff Type: {1}\r\n", Master.TableName, DiffType);
            if (SchemaDiff.HasDiffs)
            {
                working += string.Format("Schema Diff: {0}\r\n", SchemaDiff);
            }
            if (RowDiffs.Any())
            {
                working += string.Join("\r\n", RowDiffs);
            }

            return working;
        }
        
    }


    public class SchemaDiff
    {
        public List<ColumnDiff> ColumnDiffs { get; set; }
        public bool IsCompatible { get; set; }
        DiffType DiffType;
        public SchemaDiff()
        {
            ColumnDiffs = new List<ColumnDiff>();
            DiffType = DiffType.None;
        }
        public bool HasDiffs
        {
            get
            {
                return ColumnDiffs.Count() > 0;
            }
        }

        public override string ToString()
        {
            var template = "Diff Type: {0}\r\nIs Compatible: {1}\r\n{2}";
            return string.Format(template, DiffType, IsCompatible, string.Join("\r\n", ColumnDiffs));
        }
    }

    /// <summary>
    /// determines the delta between two rows.
    /// TODO: Consider caching current and original values.
    /// </summary>
    public class RowDiff
    {
        public DiffType DiffType { get; set; }
        public List<ColumnDiff> ColumnDiffs { get; set; }
        public DataRow Row { get; set; }
       
        public RowDiff()
        {
            DiffType = DiffType.None;
            ColumnDiffs = new List<ColumnDiff>();
        }

        public override string ToString()
        {
            var template = "Row #: {0}.  Diff Type: {1}\r\n{2}";
            return string.Format(template,RowId(),   DiffType, string.Join("\r\n", ColumnDiffs));
        }

        private string RowId()
        {
            var pk = Row.Table.PrimaryKey;
            if (!pk.Any())
                return string.Empty;
            return string.Join(", ", pk.Select(c => Row[c.ColumnName]));
        }
      
    }

    /// <summary>
    /// TODO:  Split this into ColumnDiff class, that reports on schema diffs, but doesn't hold values,
    /// and CellDiff, which refereces a [row, column] in the master table (or replica in the case of an extra column/row) and optionally stores the values being compared. 
    /// </summary>
    public class ColumnDiff
    {
        public DataColumn Column { get; set; }
        public object ReplicaValue { get; set; }
        public object MasterValue { get; set; }
        public DiffType DiffType { get; set; }
        public ColumnDiff()
        {
            DiffType = DiffType.None;
        }

        public override string ToString()
        {
            return string.Format("\t{0}\t{1}", Column.ColumnName, DiffType);
        }



       
    }

  
    public enum DiffType
    {
        None,
        Extra,
        Missing,
        TypeMismatch,
        DataMismatch
    }

    public enum TableDiffType {
        None,
        CompatibleSchema,
        IncompatibleSchema,
        Data
    }
}
