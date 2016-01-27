using System;

namespace Craftsmaneer.DataTools.Compare
{
    /// <summary>
    /// List of options available for making comparisons between schema and data.
    /// </summary>
    [Flags]
    public enum TableCompareOptions
    {
        None,
        AllowIncompatibleSchema, // will do the comparison even if the schemas are not compatible.
        CaptureValues, // will populate the MasterValue & ReplicaValue fields when a Column diff is detected.
        KeysOptional, // If the master datatable does not have a primary key, use the all the columns to join.
        TreatDefaultsAsNull, // if the value of the column is equal to the configured default, it will be treated as null.
    }
}