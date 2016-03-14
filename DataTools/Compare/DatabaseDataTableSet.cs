using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Craftsmaneer.Data;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Compare
{
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

        

        public override ReturnValue<string[]> ImportTables(string connStr)
        {
            throw new NotImplementedException();
        }

        public override ReturnValue SaveConfig(string dtsConfigPath)
        {
            return ReturnValue.Wrap(() =>
            {
                var xdoc = new XDocument();
                xdoc.Add(new XElement("DataTableSet",
                    new XElement("Type", "Database"),
                    new XElement("ConnectionString", ConnStr),
                    new XElement("Tables",
                        TableList.Select(t => new XElement("Table", t)))
                    ));
            }, "Saving database config");

        }
    }
}