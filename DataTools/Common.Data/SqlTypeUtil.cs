using System;
using System.Collections.Generic;
using System.Data;

namespace Craftsmaneer.DataTools.Common.Data
{
    public class SqlTypeUtil
    {
        public static string GetFormattedValue(object value)
        {
            if (ClrTypeGroupMap.ContainsKey(value.GetType()))
            {
                var typeGroup = ClrTypeGroupMap[value.GetType()];
                if (TypeGroupFormatterMap.ContainsKey(typeGroup))
                {
                    return string.Format(TypeGroupFormatterMap[typeGroup], value);
                }
                else
                {
                    Log.Debug(string.Format("No TypeGroupFormatter was found for: '{0}'", typeGroup));
                    return string.Format("{0}", value);
                }
            }
            else
            {
                Log.Debug(string.Format("No ClrTypeGroup was found for: '{0}'", value.GetType()));
                return string.Format("{0}", value);
            }
                         
        }

        private static readonly Dictionary<SqlDbTypeGroup, string> TypeGroupFormatterMap =
            new Dictionary<SqlDbTypeGroup, string>()
            {
                {SqlDbTypeGroup.Binary, "0x{0}"},
                {SqlDbTypeGroup.Boolean, "{0}"},
                {SqlDbTypeGroup.Date, "'{0:s}'"},
                {SqlDbTypeGroup.Floating, "{0}"},
                {SqlDbTypeGroup.Integer, "{0}"},
                {SqlDbTypeGroup.String, "'{0}'"},
                {SqlDbTypeGroup.Unknown, "{0}"},
                {SqlDbTypeGroup.Xml, "'{0}'"},
            };


        public static readonly Dictionary<SqlDbType, SqlDbTypeGroup> SqlDbTypeGroupMap = new Dictionary<SqlDbType, SqlDbTypeGroup>()
        {
            {SqlDbType.BigInt, SqlDbTypeGroup.Integer},
            {SqlDbType.Binary, SqlDbTypeGroup.Binary},
            {SqlDbType.Bit, SqlDbTypeGroup.Boolean},
            {SqlDbType.Char, SqlDbTypeGroup.String},
            {SqlDbType.Date, SqlDbTypeGroup.Date},
            {SqlDbType.DateTime, SqlDbTypeGroup.Date},
            {SqlDbType.DateTimeOffset, SqlDbTypeGroup.Date},
            {SqlDbType.Decimal, SqlDbTypeGroup.Floating},
            {SqlDbType.Float, SqlDbTypeGroup.Floating},
            {SqlDbType.Image, SqlDbTypeGroup.Binary},
            {SqlDbType.Int, SqlDbTypeGroup.Integer},
            {SqlDbType.Money, SqlDbTypeGroup.Floating},
            {SqlDbType.NChar, SqlDbTypeGroup.String},
            {SqlDbType.NText, SqlDbTypeGroup.String},
            {SqlDbType.NVarChar, SqlDbTypeGroup.String},
            {SqlDbType.Real, SqlDbTypeGroup.Floating},
            {SqlDbType.SmallDateTime, SqlDbTypeGroup.Date},
            {SqlDbType.SmallInt, SqlDbTypeGroup.Integer},
            {SqlDbType.SmallMoney, SqlDbTypeGroup.Floating},
            {SqlDbType.Structured, SqlDbTypeGroup.Unknown},
            {SqlDbType.Text, SqlDbTypeGroup.String},
            {SqlDbType.Time, SqlDbTypeGroup.Date},
            {SqlDbType.Timestamp, SqlDbTypeGroup.Binary},
            {SqlDbType.TinyInt, SqlDbTypeGroup.Integer},
            {SqlDbType.Udt, SqlDbTypeGroup.Unknown},
            {SqlDbType.UniqueIdentifier, SqlDbTypeGroup.Binary},
            {SqlDbType.VarBinary, SqlDbTypeGroup.Binary},
            {SqlDbType.VarChar, SqlDbTypeGroup.String},
            {SqlDbType.Variant, SqlDbTypeGroup.Unknown},
            {SqlDbType.Xml, SqlDbTypeGroup.Xml}
        };
       
        public static readonly Dictionary<Type, SqlDbTypeGroup> ClrTypeGroupMap = new Dictionary<Type, SqlDbTypeGroup>()
        {
            {typeof(Byte), SqlDbTypeGroup.Integer},
            {typeof(Int32), SqlDbTypeGroup.Integer},
            {typeof(Int16), SqlDbTypeGroup.Integer},
            {typeof(Int64), SqlDbTypeGroup.Integer},
            {typeof(Char), SqlDbTypeGroup.String},
            {typeof(String), SqlDbTypeGroup.String},
            {typeof(DateTime),SqlDbTypeGroup.Date},
            {typeof(DateTimeOffset),SqlDbTypeGroup.Date},
            {typeof(TimeSpan),SqlDbTypeGroup.Date},
            {typeof(Decimal), SqlDbTypeGroup.Floating},
            {typeof(Single), SqlDbTypeGroup.Floating},
            {typeof(Double), SqlDbTypeGroup.Floating},
            {typeof(Boolean), SqlDbTypeGroup.Boolean},
            {typeof(Guid), SqlDbTypeGroup.String},
            {typeof(Byte[]), SqlDbTypeGroup.Binary},
          
        };
         
    }
}