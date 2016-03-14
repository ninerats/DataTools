using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Craftsmaneer.Data;
using Craftsmaneer.DataTools;
using Craftsmaneer.DataTools.Common.Data;
using Craftsmaneer.DataTools.Compare;

namespace DataTool.Search
{
    internal class DataValueSearcher
    {
        private readonly DatabaseDataTableSet _databaseDataTableSet;

        public DataValueSearcher(DatabaseDataTableSet databaseDataTableSet)
        {
            _databaseDataTableSet = databaseDataTableSet;

        }

        public List<SearchResult> Search(string textToSearchFor)
        {
            var searchResults = new List<SearchResult>();
            using (var conn = new SqlConnection(_databaseDataTableSet.ConnStr))
            {
                conn.Open();
                foreach (var table in _databaseDataTableSet.TableList)
                {
                    var fields = GetFieldsOfType(conn, table, SqlDbTypeGroup.String);
                    DataColumn[] keys;
                    using (var adapter = new SqlDataAdapter("select * from " + table, conn))
                    using (var dt = new DataTable(table))
                    {
                        adapter.FillSchema(dt, SchemaType.Mapped);
                        keys = dt.PrimaryKey;
                    }

                    foreach (DataRow fieldInfoRow in fields.Rows)
                    {
                        var fieldName = fieldInfoRow.Field<string>("COLUMN_NAME");
                        string fieldSearchSql = string.Format("select * from {0} where ([{1}] like '%{2}%')",
                            table, fieldName, textToSearchFor);
                        Log.Debug(string.Format("fieldSearchSql: {0}", fieldSearchSql));
                        var dataSearchResults = conn.ExecQuery(fieldSearchSql);
                        
                            searchResults.AddRange(
                                dataSearchResults.Rows.Cast<DataRow>().Select(dataRow => new SearchResult()
                                {
                                    TableName = table,
                                    FieldName = fieldName,
                                    FieldType = fieldInfoRow.Field<string>("DATA_TYPE"),
                                    Sql = string.Format("{0} AND {1}", fieldSearchSql, MakeKeyFilter(keys,dataRow.KeyValues())),
                                    Value = dataRow[fieldName],
                                    KeyFields = keys,
                                    KeyValue = dataRow.KeyValues()
                                }));
                       
                    }

                }

            }
            return searchResults;
        }

        private string MakeKeyFilter(DataColumn[] keyFields, object[] keyValues)
        {
            var subexpr = new string[keyFields.Count()];
            for (int i = 0; i < keyFields.Count(); i++)
            {
                subexpr[i] = string.Format("({0}={1})", keyFields[i], SqlTypeUtil.GetFormattedValue  (keyValues[i]));
            }
            return string.Format(subexpr.Count() > 1?  "({0})": "{0}", string.Join(" and ", subexpr));
        }

        


        private DataTable GetFieldsOfType(SqlConnection conn, string table, SqlDbTypeGroup dbTypeGroup)
        {
            var matchingTypes = SqlTypeUtil.SqlDbTypeGroupMap.Where(kv => kv.Value == dbTypeGroup).Select(kv => kv.Key);
            var inClause = string.Join(",", matchingTypes.Select(t => string.Format("'{0}'", t)));

            var sqlDbTypeSql =
                string.Format(
                    "select * from INFORMATION_SCHEMA.COLUMNS where (TABLE_SCHEMA + '.' + TABLE_NAME = '{0}') " +
                    "AND (DATA_TYPE IN ({1}))", table, inClause);
            Log.Debug(sqlDbTypeSql);
            var result = conn.ExecQuery(sqlDbTypeSql);
            return result;

        }
    }
}