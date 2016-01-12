using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Craftsmaneer.DataTools
{
    public class TableDiff
    {
        public TableDiffType DiffType { get; set; }
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
    }

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

    public class ColumnDiff
    {
        public DataColumn Column { get; set; }
        public DiffType DiffType { get; set; }
        public ColumnDiff()
        {
            DiffType = DiffType.None;
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
