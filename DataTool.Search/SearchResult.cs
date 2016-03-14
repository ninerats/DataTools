using System.Data;
using System.Linq;

namespace DataTool.Search
{
    internal class SearchResult
    {
        public string TableName { get; set; }

        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string Sql { get; set; }

        public object Value { get; set; }

        public object[] KeyValue { get; set; }
        public DataColumn[] KeyFields { get; set; }

        public string KeyList
        {
            get
            {
                var kv = new string[KeyFields.Count()];
                for (int i = 0; i < KeyFields.Count(); i++)
                {
                    kv[i] = string.Format("{0}={1}", KeyFields[i], KeyValue[i]);
                }
                return string.Join(", ", kv);

            }
        }

        
    }
}