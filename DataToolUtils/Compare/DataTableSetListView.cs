﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataToolUtils.Compare
{
    public class DataTableSetListView : ListView
    {
        public DataTableSetListView()
        {
            
            Init();
           // LoadSampleItems();
        }

        private TableSetDiff _tableSetDiff;

        [Browsable(false)]
        public TableSetDiff TableSetDiff
        {
            get { return _tableSetDiff; }
            set
            {
                _tableSetDiff = value;
               if (! ReturnValue.Wrap(RefreshList).Success)
                {
                    // TODO: log it here.
                };
            }
        }

        private void RefreshList()
        {
            Items.Clear();
            foreach (var kv in TableSetDiff)
            {
                string schema;
                string desc;
                string imageKey;
                if (kv.Value.Success)
                {
                    var tdiff = kv.Value.Value;
                    if (tdiff.SchemaDiff.IsCompatible)
                    {
                        schema = tdiff.SchemaDiff.HasDiffs ? "Compatible" : "Identical";
                        
                    }
                    else
                    {
                        schema = "Incompatible";
                    }
                    if (tdiff.RowDiffs.Any())
                    {
                        desc = string.Format("{0} rows don't match.", tdiff.RowDiffs.Count());
                    }
                    else
                    {
                        desc = tdiff.HasDiffs ? "The data in the tables match." :
                        "The tables are identical.";
                    }
                    switch (tdiff.DiffType)
                    {
                        case TableDiffType.None: imageKey = "equal";
                            break;
                        case TableDiffType.CompatibleSchema: imageKey =
                            "dataschemadiff";
                            break;
                        case TableDiffType.IncompatibleSchema:
                            imageKey = "incompatible";
                            break;
                        case TableDiffType.Data:
                            imageKey = "datadiff";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    

                }
                else
                {
                    schema = "Error";
                    desc = kv.Value.ToString();
                    imageKey = "error";
                }



                AddResult(imageKey,kv.Key, schema, desc, kv.Value);
            }
        }

        private void AddResult(string imageKey, string key, string schema, string desc, ReturnValue<TableDiff> tdiffResult )
        {
            var groupKey = imageKey;
            if (imageKey == "dataschemadiff")
                groupKey = "datadiff";
            var li = new ListViewItem(new[] {key, schema, desc}, imageKey, Groups[groupKey])
            {
                Tag = tdiffResult
            };
            if (li.Group.Name != "error" && li.Group.Name != "equal")
            {
                li.Checked = true;
            }
            Items.Add(li);
        }

        private void Init()
        {
           
           
            // 
            // imagelist.
            // 
            var resources = new ComponentResourceManager(typeof(DataTableSetListView));
            //TODO: decouple from this form.
            var ilCompareResult = new ImageList
            {
                 ImageStream = ((ImageListStreamer)(resources.GetObject("ilImageStream"))),
                TransparentColor = Color.Transparent
            };
            ilCompareResult.Images.SetKeyName(0, "dataschemadiff");
            ilCompareResult.Images.SetKeyName(1, "warning");
            ilCompareResult.Images.SetKeyName(2, "error");
            ilCompareResult.Images.SetKeyName(3, "equal");
            ilCompareResult.Images.SetKeyName(4, "incompatible");
            ilCompareResult.Images.SetKeyName(5, "datadiff");
            ilCompareResult.Images.SetKeyName(6, "missing");
            SmallImageList = ilCompareResult;

            var groupEqual = new ListViewGroup("No Differences", HorizontalAlignment.Left)
            {
                Header = "No Differences",
                Name = "equal"
            };
            var groupIncompatible = new ListViewGroup("Differences (Incompatible)", HorizontalAlignment.Left)
            {
                Header = "Differences (Incompatible)",
                Name = "incompatible"
            };
            var groupDataDiff = new ListViewGroup("Differences (Compatible)", HorizontalAlignment.Left)
            {
                Header = "Differences (Compatible)",
                Name = "datadiff"
            };
            var groupMissing = new ListViewGroup("Missing Tables", HorizontalAlignment.Left)
            {
                Header = "Missing Tables",
                Name = "missing"
            };
            var groupError = new ListViewGroup("Error Comparing", HorizontalAlignment.Left)
            {
                Header = "Error Comparing",
                Name = "error"
            };
            Groups.AddRange(new[]
            {
                groupEqual,
                groupIncompatible,
                groupDataDiff,
                groupMissing,
                groupError
            });

            // Add columns.
            var colTableName = new ColumnHeader { Text = "Table", Width = 215 };
            var colSchema = new ColumnHeader { Text = "Schema", Width = 86 };
            var colResults = new ColumnHeader { Text = "ResultDetails", Width = 483 };
            Columns.AddRange(new[]
            {
                colTableName,
                colSchema,
                colResults
            });


            //settings 
            CheckBoxes = true;
            View = View.Details;
        }


        internal void LoadSampleItems()
        {
            var listViewItem1 = new ListViewItem(new[]
            {
                "HumanResources.Department",
                "Compatible",
                "45 rows have been added."
            }, "datadiff")
            {
                Group = Groups["datadiff"]
            };
            var listViewItem2 = new ListViewItem(new[]
            {
                "Production.Product",
                "Identical",
                "423 rows scanned, No Differences detected"
            }, "equal")
            {
                Group = Groups["equal"]
            };
            var listViewItem3 = new ListViewItem(new[]
            {
                "Production.ScrapReason",
                "--",
                "Table \'Prodcution.ScrapReason\' could not be found in the Replica set."
            }, "missing")
            {
                Group = Groups["missing"]
            };
            var listViewItem4 = new ListViewItem(new[]
            {
                "Production.UnitMeasure",
                "Incompatble",
                "The \"MetricAmount\" field is missing."
            }, "incompatible")
            {
                Group = Groups["incompatible"]
            };
            var listViewItem5 = new ListViewItem(new[]
            {
                "Ye Olde Table",
                "--",
                "Error trying to read \'Ye Olde Table\'"
            }, "error")
            {
                Group = Groups["error"]
            };



            Items.AddRange(new[]
            {
                listViewItem1,
                listViewItem2,
                listViewItem3,
                listViewItem4,
                listViewItem5
            });
        }
    }
}