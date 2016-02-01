using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Craftsmaneer.Data;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.IO
{
    /// <summary>
    ///     transforms dataTables between database and files
    ///     handles referential integrity.
    /// </summary>
    public class DataTableSerializer : IDisposable
    {
        public DataTableSerializer()
        {
        }

        public DataTableSerializer(string connStr)
            : this()
        {
            Connection = new SqlConnection(connStr);
            Connection.Open();
        }

        public SqlConnection Connection { get; private set; }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
            }
        }

        /// <summary>
        ///     reads a datatable from XML
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ReturnValue<DataTable> ReadTable(string path)
        {
            return ReturnValue<DataTable>.Wrap(() =>
            {
                var dt = new DataTable();
                dt.ReadXml(path);
                return dt;
            }, string.Format("Reading datatable from {0}.", path));
        }

        /// <summary>
        ///     imports a table from an xml file.  the table must exist.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ReturnValue ImportTable(string path)
        {
            return ReturnValue.Wrap(() =>
            {
                var dt = new DataTable();
                dt.ReadXml(path);

                using (SqlTransaction tran = Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    Connection.ExecSql(string.Format("DELETE  {0}", dt.TableName), tran);
                    string sql = string.Format("SELECT * FROM {0}", dt.TableName);
                    var cmd = new SqlCommand(sql, Connection, tran);
                    var ds = new DataSet();
                    ds.EnforceConstraints = false;
                    ds.Tables.Add(dt);
                    var dataAdapter = new SqlDataAdapter(cmd);
                    // Automatically generates DeleteCommand, UpdateCommand and InsertCommand for DataAdapter object 
                    var builder = new SqlCommandBuilder(dataAdapter);
                    dataAdapter.Update(dt);
                    tran.Commit();
                    ds.Tables.Remove(dt);
                }
            }, string.Format("Importing table from {0}.", path));
        }

        public ReturnValue ExportTable(string tableName, string path)
        {
            return ReturnValue.Wrap(() =>
            {
                string sql = string.Format("SELECT * FROM {0}", tableName);
                var cmd = new SqlCommand(sql, Connection);
                var adapter = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(dt);
                dt.TableName = tableName;
                dt.WriteXml(path, XmlWriteMode.WriteSchema);
            }, string.Format("Exporting table '{0}' to '{1}'.", tableName, path));
        }

        //    https://social.msdn.microsoft.com/Forums/en-US/98cf1c68-f11b-4cd7-955c-152b7645ec08/readxml-to-datatable-and-update-database?forum=csharpgeneral
        public ReturnValue ImportTableWithBulkCopy(string path)
        {
            string tableName = "";
            return ReturnValue.Wrap(() =>
            {
                // TODO: consider getting a table lock in the whole table.
                using (SqlTransaction tran = Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    var dt = new DataTable();
                    dt.ReadXml(path);
                    tableName = dt.TableName;
                    // delete table first.  must fake deferable constrain.
                    //Connection.ExecSql(string.Format("ALTER TABLE {0} NOCHECK CONSTRAINT ALL", dt.TableName),tran);
                    Connection.ExecSql(string.Format("sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'"), tran);
                    Connection.ExecSql(string.Format("DELETE FROM {0}", dt.TableName), tran);
                    // Creat the SqlBulkCopy object using a connection string
                    using (var bulkCopy = new SqlBulkCopy(Connection, SqlBulkCopyOptions.TableLock, tran))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(dt);
                    }
                    // turn constraint back on. any error will happen here.
                    // TODO: return specific error code for a failed constraint.

                    Connection.ExecSql("sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'", tran);
                }
            }, string.Format("Importing table '{0}' fron '{1}'.", tableName, path));
        }

        /// <summary>
        ///     imports the tables as a single transaction.
        /// // TODO: return specific error code for a failed constraint.
        /// </summary>

        public ReturnValue ImportTables(IEnumerable<string> paths)
        {
            return ReturnValue.Wrap(() =>
            {
                // TODO: consider getting a table lock in the whole table.
                using (SqlTransaction tran = Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    // turn off constraints for all tables.  
                    //TODO: see if only turning off constraints on specific tables affects performance.
                    Connection.ExecSql(string.Format("sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'"), tran);
                    foreach (string path in paths)
                    {
                        var dt = new DataTable();
                        dt.ReadXml(path);
                        string tableName = dt.TableName;
                        Connection.ExecSql(string.Format("DELETE FROM {0}", dt.TableName), tran);
                        // Creat the SqlBulkCopy object using a connection string
                        using (var bulkCopy = new SqlBulkCopy(Connection, SqlBulkCopyOptions.TableLock, tran))
                        {
                            bulkCopy.DestinationTableName = tableName;
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(dt);
                        }
                    }
                    // turn constraint back on. any DRI error will happen here, rolling back the transaction.
                    // ("WITH CHECK CHECK..." is not a typo, that means "enable CHECK CONSTRAINT, using the WITH CHECK option)                    
                    Connection.ExecSql("sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'", tran);
                }
            }, string.Format("Importing tables {0}", string.Join(", ", paths)));
        }
    }
}