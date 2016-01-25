using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools
{
    public abstract class DataTableCollection : ICollection
    {
        private DataSet _ds = null;
        private DataSet DS
        {
            get
            {
                if (_ds == null)
                {
                    var dsResult = GetTables();
                    if (dsResult.Success)
                    {
                        _ds = dsResult.Value;
                    }
                    else
                    {
                        _ds = null;
                    }
                }
                return _ds;
            }
        }
        public List<string> TableList { get; set; }
        public DataTable this[string name]
        {
            get
            {
               return DS.Tables[name];
            }
           
        }

        public static ReturnValue<DataTableCollection> FromConfigFile(string fileName)
        {
            try
            {
                var config = XDocument.Load(fileName);
                string type = config.Root.Element("Type").Value;
                DataTableCollection dtc;
                if (type == "Database")
                {
                    dtc = new DatabaseDataTableCollection(config.Root.Element("ConnectionString").Value);
                }
                else if (type == "Folder")
                {
                    dtc = new FolderDataTableCollection(config.Root.Element("FolderPath").Value);
                }
                else
                {

                    return ReturnValue<DataTableCollection>.FailResult(string.Format("Invalid Type in configration file: {0}", type));
                }
                var dataTablesElement = config.Root.Element("Tables");
                if (dataTablesElement != null)
                {
                    dtc.TableList = dataTablesElement.Elements("Table").Select(e => e.Value).ToList();

                }
                return ReturnValue<DataTableCollection>.SuccessResult(dtc);

            }
            catch (Exception ex)
            {
                return ReturnValue<DataTableCollection>.FailResult(string.Format("Error trying to create DataTableCollection from file: {0}", fileName), ex);
            }
        }
        public void CopyTo(Array array, int index)
        {
            DS.Tables.CopyTo((DataTable[])array, index);
        }

        public int Count
        {
            get { return DS.Tables.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return DS.Tables.GetEnumerator();
        }

        protected abstract ReturnValue<DataSet> GetTables();


    }

    public class FolderDataTableCollection : DataTableCollection
    {
        public string FolderPath { get; protected set; }
        public FolderDataTableCollection(string folderPath)
        {
            FolderPath = folderPath;
        }


        protected override ReturnValue<DataSet> GetTables()
        {
            return ReturnValue<DataSet>.Wrap(() =>
            {
                var ds = new DataSet();
                ds.EnforceConstraints = false;
               // assume each file is {schema}.{table}.xml
                foreach (var file in Directory.GetFiles(FolderPath))
                {
                    var fileName = new FileInfo(file).Name;
                    var dt = new DataTable(fileName.Substring(0, fileName.Length - 4));
                    ds.Tables.Add(dt);                  
                    dt.ReadXml(file);
                    
                }

                return ds;
            }, "Error Getting tables.");
        }
    }

    public class DatabaseDataTableCollection : DataTableCollection
    {
        public string ConnStr { get; protected set; }
        public DatabaseDataTableCollection(string connStr)
        {
            ConnStr = connStr;
        }



        protected override ReturnValue<DataSet> GetTables()
        {
            return ReturnValue<DataSet>.Wrap(() =>
            {
                var ds = new DataSet();
                ds.EnforceConstraints = false;
                using (var conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
               
                var tables = TableList;
                if (tables == null || tables.Count == 0)
                {
                    tables =
                        conn.GetTableList()
                            .Rows.Cast<DataRow>()
                            .Select(r => string.Format("{0},{1}", r["TableName"], r["SchemaName"]))
                            .ToList();
                }
                    foreach (var table in tables)
                    {
                        ds.Tables.Add(conn.GetTable(table));
                    }
                    return ds;
                }
            }, "Error Getting tables.");
        }
    }

}
