﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test
{
    class TestHelper
    {
        public static DataTable BasicDataTable()
        {
            var dt = GetBlankDataTable();
            dt.Rows.Add(new object[] { 1, "Bob", 55, true, new DateTime(2000, 1, 1) });
            dt.Rows.Add(new object[] { 2, "Carol", 33, false, new DateTime(2011, 1, 1) });
            dt.Rows.Add(new object[] { 3, "Alice", 67, false, new DateTime(2005, 1, 3) });
            return dt;
        }

        public static DataTable BasicDataTableExtraRow()
        {
            var dt = GetBlankDataTable();
            dt.Rows.Add(new object[] { 1, "Bob", 55, true, new DateTime(2000, 1, 1) });
            dt.Rows.Add(new object[] { 2, "Carol", 33, false, new DateTime(2011, 1, 1) });
            dt.Rows.Add(new object[] {3, "Alice", 67, false, new DateTime(2005, 1, 3) });
            dt.Rows.Add(new object[] { 99, "Ted", 14, true, new DateTime(2014, 4, 3) });
            return dt;
        }

        public static DataTable BasicDataTableMissingRow()
        {
            var dt = GetBlankDataTable();
            dt.Rows.Add(new object[] { 1, "Bob", 55, true, new DateTime(2000, 1, 1) });
            dt.Rows.Add(new object[] { 2, "Carol", 33, false, new DateTime(2011, 1, 1) });
            return dt;
        }

        public static DataTable BasicDataTableModdedRow()
        {
            var dt = GetBlankDataTable();
            dt.Rows.Add(new object[] { 1, "Bob", 55, true, new DateTime(2000, 1, 1) });
            dt.Rows.Add(new object[] { 2, "Carol", 33, false, new DateTime(2011, 1, 1) });
            dt.Rows.Add(new object[] { 3, "Alice", 68, false, new DateTime(2005, 7, 7) });
            return dt;
        }

        public static DataTable BasicDataTableMissingValue()
        {
            var dt = GetBlankDataTable();
            dt.Rows.Add(new object[] { 1, "Bob", 55, true, new DateTime(2000, 1, 1) });
            dt.Rows.Add(new object[] { 2, "Carol", 33, false, new DateTime(2011, 1, 1) });
            dt.Rows.Add(new object[] { 3,  DBNull.Value, 67, false, new DateTime(2005, 1, 3) });
           
            return dt;
        }
        
        public static DataTable BasicDataTableAddAndDelete()
        {
            var dt = GetBlankDataTable();
            dt.Rows.Add(new object[] { 1, "Bob", 55, true, new DateTime(2000, 1, 1) });
            dt.Rows.Add(new object[] { 2, "Carol", 33, false, new DateTime(2011, 1, 1) });
            dt.Rows.Add(new object[] { 4, "Joe", 123, true, new DateTime(2011, 6, 3) });
            return dt;
        }

        public static DataTable BasicDataTableCompatibleSchemaChange()
        {
            var dt = GetBlankDataTable();
            dt.Columns.Add("newCol", typeof(String));
            dt.Rows.Add(new object[] { 1, "Bob", 55, true, new DateTime(2000, 1, 1) ,"newColR1"});
            dt.Rows.Add(new object[] { 2, "Carol", 33, false, new DateTime(2011, 1, 1), "newColR2" });
            dt.Rows.Add(new object[] { 3, "Alice", 67, false, new DateTime(2005, 1, 3), "newColR3" });
            return dt;
        }

        public static DataTable BasicDataTableIncompatibleSchemaChange()
        {
            var dt = GetBlankDataTable();
            dt.Columns.Remove(dt.Columns["int1"]);
            dt.Rows.Add(new object[] { 1, "Bob", true, new DateTime(2000, 1, 1) });
            dt.Rows.Add(new object[] { 2, "Carol", false, new DateTime(2011, 1, 1) });
            dt.Rows.Add(new object[] { 3, "Alice", false, new DateTime(2005, 1, 3) });
            return dt;
        }


        public static DataTable GetBlankDataTable()
        {
            var dt = new DataTable("DataTable1");
            var pk = new DataColumn("pk", typeof(int));
            pk.Unique = true;
            pk.AllowDBNull = false;
            dt.Columns.Add(pk);
            dt.PrimaryKey = new DataColumn[] { pk };
            dt.Columns.Add("varchar1", typeof(String));
            dt.Columns.Add("int1", typeof(Boolean));
            dt.Columns.Add("bool1", typeof(Boolean));
            dt.Columns.Add("dateTime1", typeof(DateTime));
            return dt;
        }

        public static DataTable BobTableV1()
        {
            var dt = new DataTable("Bob");
            var pk = new DataColumn("pk", typeof(string));
            pk.Unique = true;
            pk.AllowDBNull = false;
            dt.Columns.Add(pk);
            dt.PrimaryKey = new DataColumn[] { pk };
            dt.Columns.Add("field1", typeof(string));
            dt.Columns.Add("field2", typeof(string));
            dt.Columns.Add("field3", typeof(string));
            dt.Columns.Add("field4", typeof(int));
            return dt;
        }

        // + field5
        public static DataTable BobTableAddField()
        {
            var dt = new DataTable("Bob");
            var pk = new DataColumn("pk", typeof(string));
            pk.Unique = true;
            pk.AllowDBNull = false;
            dt.Columns.Add(pk);
            dt.PrimaryKey = new DataColumn[] { pk };
            dt.Columns.Add("field1", typeof(string));
            dt.Columns.Add("field2", typeof(string));
            dt.Columns.Add("field3", typeof(string));
            dt.Columns.Add("field4", typeof(int));
            dt.Columns.Add("field5", typeof(string));

            return dt;
        }

        // - field3
        public static DataTable BobTableRemoveField()
        {
            var dt = new DataTable("Bob");
            var pk = new DataColumn("pk", typeof(string));
            pk.Unique = true;
            pk.AllowDBNull = false;
            dt.Columns.Add(pk);
            dt.PrimaryKey = new DataColumn[] { pk };
            dt.Columns.Add("field1", typeof(string));
            dt.Columns.Add("field2", typeof(string));
            // dt.Columns.Add("field3", typeof(string));
            dt.Columns.Add("field4", typeof(int));
            return dt;
        }

        // - field1 data type changed
        public static DataTable BobTableChangeDT()
        {
            var dt = new DataTable("Bob");
            var pk = new DataColumn("pk", typeof(string));
            pk.Unique = true;
            pk.AllowDBNull = false;
            dt.Columns.Add(pk);
            dt.PrimaryKey = new DataColumn[] { pk };
            dt.Columns.Add("field1", typeof(int));
            dt.Columns.Add("field2", typeof(string));
            dt.Columns.Add("field3", typeof(string));
            dt.Columns.Add("field4", typeof(int));

            return dt;
        }

        // - add and remove a field
        public static DataTable BobTableAddAndRemove()
        {
            var dt = new DataTable("BobV2");
            var pk = new DataColumn("pk", typeof(string));
            pk.Unique = true;
            pk.AllowDBNull = false;
            dt.Columns.Add(pk);
            dt.PrimaryKey = new DataColumn[] { pk };
            dt.Columns.Add("field1", typeof(string));
            dt.Columns.Add("field2", typeof(string));
            //dt.Columns.Add("field3", typeof(string));
            dt.Columns.Add("field4", typeof(int));
            dt.Columns.Add("field5", typeof(string));
            return dt;
        }
    }
}
