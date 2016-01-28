﻿using System.Collections.Generic;
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
        public TableDiff()
        {
            SchemaDiff = new SchemaDiff();
            RowDiffs = new List<RowDiff>();
        }
        public bool HasDiffs
        {
            get
            {
                return SchemaDiff.HasDiffs || RowDiffs.Count() > 0;
            }
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
