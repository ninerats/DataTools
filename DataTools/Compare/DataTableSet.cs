using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Craftsmaneer.Data;
using Craftsmaneer.DataTools.IO;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Compare
{
    public abstract class DataTableSet : ICollection
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

        protected abstract ReturnValue<DataSet> GetTables();
        public abstract ReturnValue<string[]> ImportTables(string connStr);

        // HACK: This really belongs in the FolderDataTableSet class.
        public static ReturnValue<FolderDataTableSet> FromRelativeFolderConfigFile(string configFile, string rootFolder,
            bool loadOnOpen = true)
        {
            var createResult = FromConfigFile(configFile, false);
            if (!createResult.Success)
                ReturnValue<DataTableSet>.Cascade(createResult, string.Format("Error loading config file at {0}",
                    configFile));
            var dtSet = createResult.Value as FolderDataTableSet;
            if (dtSet == null)
                return ReturnValue<FolderDataTableSet>.FailResult(
                    string.Format("This DataTableSet specified in the config file at {0} is not a Folder DataTableSet.",configFile));

            var setPathResult = ReturnValue.Wrap(() =>
            {
                var originalRelativePath = dtSet.FolderPath;
                dtSet.FolderPath = Path.Combine(rootFolder, originalRelativePath ?? "");
              
            }, string.Format("adding root apth {0} to relative path in config file",rootFolder));
            if (!setPathResult.Success)
            {
                return ReturnValue<FolderDataTableSet>.Cascade(setPathResult);
            }
            if (loadOnOpen)
            {
                var loadResult = dtSet.Load();
                if (!loadResult.Success)
                    return ReturnValue<FolderDataTableSet>.Cascade(loadResult, "Failed to load tables.");
            }

            return ReturnValue<FolderDataTableSet>.SuccessResult(dtSet);

        }

        public static ReturnValue<DataTableSet> FromConfigFile(string fileName, bool loadOnOpen = true)
        {
            try
            {
                var config = XDocument.Load(fileName);
                string type = config.Root.Element("Type").Value;
                DataTableSet dtSet;
                if (type == "Database")
                {
                    string connStr = config.Root.Element("ConnectionString").Value;
                    dtSet = new DatabaseDataTableSet(connStr);
                    dtSet.Id = connStr;
                }
                else if (type == "Folder")
                {
                    string folderPath = config.Root.Element("FolderPath").Value;
                    dtSet = new FolderDataTableSet(folderPath);
                    dtSet.Id = folderPath;
                }
                else
                {

                    return ReturnValue<DataTableSet>.FailResult(string.Format("Invalid Type in configration file: {0}", type));
                }
                var dataTablesElement = config.Root.Element("Tables");
                if (dataTablesElement != null)
                {
                    dtSet.TableList = dataTablesElement.Elements("Table").Select(e => e.Value).ToList();

                }

                if (loadOnOpen)
                {
                    var loadResult = dtSet.Load();
                    if (!loadResult.Success)
                        return ReturnValue<DataTableSet>.Cascade(loadResult, "Failed to load tables.");
                }

                return ReturnValue<DataTableSet>.SuccessResult(dtSet);

            }
            catch (Exception ex)
            {
                return ReturnValue<DataTableSet>.FailResult(string.Format("Error trying to create DataTableSet from file: {0}", fileName), ex);
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


        public abstract ReturnValue ExportTables(string exportFolder);
    }
        #endregion

    public class FolderDataTableSet : DataTableSet
    {
        public string FolderPath { get;  set; }
        public FolderDataTableSet(string folderPath)
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
                foreach (var file in AvailableFiles)
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

        private string[] AvailableFiles
        {
            get
            {
                var availableFiles = Directory.GetFiles(FolderPath);
                if (TableList != null && TableList.Count > 0)
                {
                    var expandedNames = TableList.Select(tn => string.Format(@"{0}\{1}.xml", FolderPath, tn));
                    availableFiles = availableFiles.Intersect(expandedNames).ToArray();
                }
                return availableFiles;
            }
        }

        public override ReturnValue<string[]> ImportTables(string connStr)
        {
            return ReturnValue<string[]>.Wrap(() =>
            {
                var dataSer = new DataTableSerializer(connStr);
                dataSer.ImportTables(AvailableFiles).AbortOnFail();
                return AvailableFiles;
            }, "Importing tables");
        }

        public override ReturnValue ExportTables(string exportFolder)
        {
            // this is effecively a file copy.
            throw new NotImplementedException();
        }
    }

    public class DatabaseDataTableSet : DataTableSet
    {
        public string ConnStr { get; protected set; }
        public DatabaseDataTableSet(string connStr)
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
                                .Select(r => string.Format("{0}.{1}", r["SchemaName"],r["TableName"] ))
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

        public override ReturnValue ExportTables(string exportFolder)
        {
            if (!Directory.Exists(exportFolder))
            {
                return ReturnValue.FailResult(string.Format("Exporting tables"), new DirectoryNotFoundException(
                    string.Format("The export folder '{0}' does not exist.", exportFolder)));
            }
            return ReturnValue.Wrap(() =>
            {
                var dataSer = new DataTableSerializer(ConnStr);
                foreach (string tableName in TableList)
                {

                    var path = Path.Combine(exportFolder, string.Format("{0}.xml", tableName));
                    dataSer.ExportTable(tableName, path).AbortOnFail();
                 
                    
                }
            }, string.Format("Exporting to folder '{0}'.", exportFolder));
        }

        //TODO: standardize this.
        private string GetDataTablePath(DataTable table)
        {
            return string.Format("{0}.xml", table.TableName);
        }

        public override ReturnValue<string[]> ImportTables(string connStr)
        {
            throw new NotImplementedException();
        }
    }
}


