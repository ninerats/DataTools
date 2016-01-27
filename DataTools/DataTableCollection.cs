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
        private DataSet _ds;

        public ReturnValue Load()
        {
            if (_ds == null)
            {
                var dsResult = GetTables();
                if (dsResult.Success)
                {
                    _ds = dsResult.Value;
                    return ReturnValue.SuccessResult();
                }
                return ReturnValue.Cascade(dsResult, "Couldn't loan Datatable collection");
            }
            return ReturnValue.SuccessResult();
        }

        public List<string> TableList { get; set; }
        public string Id { get; set; }
        public DataTable this[string name]
        {
            get
            {
                PrepForEnumeration();
                return _ds.Tables[name];
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
                    string connStr = config.Root.Element("ConnectionString").Value;
                    dtc = new DatabaseDataTableCollection(connStr);
                    dtc.Id = connStr;
                }
                else if (type == "Folder")
                {
                    string folderPath = config.Root.Element("FolderPath").Value;
                    dtc = new FolderDataTableCollection(folderPath);
                    dtc.Id = folderPath;
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

        #region enumeration implementation
        public void CopyTo(Array array, int index)
        {
            PrepForEnumeration();
            _ds.Tables.CopyTo((DataTable[])array, index);
        }

        public int Count
        {

            get
            {
                PrepForEnumeration();
                return _ds.Tables.Count;
            }
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
            PrepForEnumeration();
            return _ds.Tables.GetEnumerator();
        }

        /// <summary>
        /// does pre-reqs to ensure this is ready to be enumerated.
        /// </summary>
        private void PrepForEnumeration()
        {
            var result = Load();
            if (_ds == null || !result.Success)
            {
                throw new InvalidOperationException("The underlying dataset for this Enumerator is not available.", result.Error);
            }
        }

        protected abstract ReturnValue<DataSet> GetTables();


    }
        #endregion

    public class FolderDataTableCollection : DataTableCollection
    {
        public string FolderPath { get; protected set; }
        public FolderDataTableCollection(string folderPath)
        {
            FolderPath = folderPath;
        }


        protected override ReturnValue<DataSet> GetTables()
        {
            try
            {
                var ds = new DataSet();
                ds.EnforceConstraints = false;
                // assume each file is {schema}.{table}.xml
                var availableFiles = Directory.GetFiles(FolderPath);
                if (TableList != null && TableList.Count > 0)
                {
                    var expandedNames = TableList.Select(tn => string.Format(@"{0}\{1}.xml", FolderPath, tn));
                    availableFiles = availableFiles.Intersect(expandedNames).ToArray();
                }
                foreach (var file in availableFiles)
                {
                    var fileName = new FileInfo(file).Name;
                    var dt = new DataTable(fileName.Substring(0, fileName.Length - 4));
                    ds.Tables.Add(dt);
                    var thisFile = file;
                    var result = ReturnValue.Wrap(() => dt.ReadXml(thisFile));
                    if (!result.Success)
                        ReturnValue<DataSet>.Cascade(result,
                            string.Format("Error reading table {0} from {1}.", dt.TableName, thisFile));

                }

                return ReturnValue<DataSet>.SuccessResult(ds);
            }
            catch (Exception ex)
            {
                return ReturnValue<DataSet>.FailResult("Unhandled error getting tables", ex);
            }
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
            try
            {
                var ds = new DataSet();
                ds.EnforceConstraints = false;
                using (var conn = new SqlConnection(ConnStr))
                {
                    conn.Open();

                    var tableList = TableList;
                    if (tableList == null || tableList.Count == 0)
                    {
                        tableList =
                            conn.GetTableList()
                                .Rows.Cast<DataRow>()
                                .Select(r => string.Format("{0},{1}", r["TableName"], r["SchemaName"]))
                                .ToList();
                    }
                    foreach (var tableName in tableList)
                    {
                        var thisConn = conn;
                        var thisTableName = tableName;
                        var result = ReturnValue.Wrap(() => ds.Tables.Add(thisConn.GetTable(thisTableName)));
                        if (!result.Success)
                        {
                            return ReturnValue<DataSet>.Cascade(result,
                                string.Format("Couldn't get DataTable for {0}.", tableName));
                        }
                    }
                }
                return ReturnValue<DataSet>.SuccessResult(ds);
            }
            catch (Exception ex)
            {
                return ReturnValue<DataSet>.FailResult("Unhanlded error while trying to tables.", ex);
            }
        }

    }
}


