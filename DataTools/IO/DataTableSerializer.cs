using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml;
using Craftsmaneer.Data;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.IO
{
    /// <summary>
    ///     transforms dataTables between database and files
    ///     handles referential integrity.
    /// TODO: split into importer and exporter clasess.
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
        [Obsolete("no real reason to use this, just keeping it around for testing.")]
        public ReturnValue ImportTableWithoutBulkCopy(string path)
        {
            return ReturnValue.Wrap(() =>
            {
                PreprareConnection();
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


        private void PreprareConnection()
        {
           Contract.Assert(Connection != null);
            if (!Connection.State.Equals(ConnectionState.Open))
            {
                Connection.Open();
            }
        }


        [Obsolete()]
        public ReturnValue ExportTableWithDataTable(string tableName, string path)
        {
            return ReturnValue.Wrap(() =>
            {
                PreprareConnection();
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

        public ReturnValue ExportTable(string tableName, string path)
        {
            //TODO: sanitize
            return ReturnValue.Wrap(() =>
            {
                PreprareConnection();
                string sql = string.Format("SELECT * FROM {0}", tableName);
                var cmd = new SqlCommand(sql, Connection);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SequentialAccess | CommandBehavior.SingleResult))
                {
                    var dt = new DataTable();
                     
                  //  WriteXmlMyself(reader, path);
                    dt.Load(reader);
                    dt.TableName = tableName;
                    dt.WriteXml(path, XmlWriteMode.WriteSchema);
                }
            }, string.Format("Exporting table '{0}' to '{1}'.", tableName, path));
        }

        private void WriteXmlMyself(SqlDataReader reader, string path)
        {
            using (var xmlWriter = XmlWriter.Create(path, new XmlWriterSettings()
            {
                ConformanceLevel = ConformanceLevel.Auto,
                Indent = true

            }))
            {
                
            foreach (IDataRecord something in reader)
            {xmlWriter.WriteStartElement("Row");
               
                    for (int i = 0; i < something.FieldCount; i++)
                    {
                       xmlWriter.WriteElementString(something.GetName(i), string.Format("{0}",something.GetValue(i))); 
                        
                    }
                xmlWriter.WriteEndElement();
                }

               
            }
        }

        public ReturnValue ImportTable(string path)
        {
            return ImportTables(new[] { path });
        }


        /// <summary>
        ///     imports the tables as a single transaction.
        /// // TODO: return specific error code for a failed constraint.
        /// </summary>
        public ReturnValue ImportTables(IEnumerable<string> paths)
        {
            return ReturnValue.Wrap(() =>
            {
                PreprareConnection();
                // transactions automatically roll back if they go out of scope without a commit.
                using (SqlTransaction tran = Connection.BeginTransaction())
                {
                    // turn off constraints for all tables.  
                    //TODO: see if only turning off constraints on specific tables affects performance.
                    Connection.ExecSql(string.Format("sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'"), tran);
                    foreach (string path in paths)
                    {
                        try
                        {
                            var dt = new DataTable();
                            dt.ReadXml(path);
                            string tableName = dt.TableName;
                            Connection.ExecSql(string.Format("DELETE FROM {0}", dt.TableName), tran);
                            using (var bulkCopy = new SqlBulkCopy(Connection, SqlBulkCopyOptions.TableLock|
                                                                              SqlBulkCopyOptions.KeepNulls |
                                                                              SqlBulkCopyOptions.KeepIdentity, tran))
                            {
                               // bulkCopy.BatchSize = 1000;
                                var tableColumns = GetTableColumnNames(dt.TableName, tran);
                                MapColumns(bulkCopy.ColumnMappings, tableColumns, dt.Columns);
                                bulkCopy.DestinationTableName = tableName;
                                // Write from the source to the destination.
                                bulkCopy.WriteToServer(dt);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new AbortException(string.Format("Exception with importing table {0}",path),ex);
                        }
                    }
                    // turn constraint back on. any DRI error will happen here, rolling back the transaction.
                    // ("WITH CHECK CHECK..." is not a typo, that means "enable CHECK CONSTRAINT, using the WITH CHECK option)                    
                    Connection.ExecSql("sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'", tran);
               
                    tran.Commit();
                }
            }, string.Format("Importing tables {0}", string.Join(", ", paths)));
        }

        // TODO: cache table schema for performance.
        private List<string> GetTableColumnNames(string tableName, SqlTransaction tran = null)
        {
            var cmd = new SqlCommand(string.Format("SELECT * FROM {0}", tableName), Connection, tran);
            using (var dr = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
            {
                var dt = dr.GetSchemaTable();
                if (dt == null)
                {
                    return new List<string>();
                }
                return dt.Rows.Cast<DataRow>().Select(r => r.Field<string>("ColumnName")).ToList();

            }
        }

        /// <summary>
        /// Maps columns by name.  Required by SqlBulkCopy if schema changes or columns are out of order.
        /// </summary>
        private void MapColumns(SqlBulkCopyColumnMappingCollection columnMappings, IEnumerable<string> tableColumnNames,
            DataColumnCollection importFileColumns)
        {
            var importColumnNames = importFileColumns.Cast<DataColumn>().Select(c => c.ColumnName);
            var columnsInCommon = tableColumnNames.Intersect(importColumnNames);
            foreach (var column in columnsInCommon)
            {
                columnMappings.Add(column, column);
            }

        }
    }
}