using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Craftsmaneer.DataTools.Compare;

namespace Craftsmaneer.DataToolUtils.Compare
{
    public class DataTableSetListView : ListView
    {
        public DataTableSetListView()
        {
            Init();
            LoadSampleItems();
        }

        public TableSetDiff TableSetDiff { get; set; }

        private void Init()
        {
            // 
            // imagelist.
            // 
            var resources = new ComponentResourceManager(typeof(CompareDataTableSets));
            //TODO: decouple from this form.
            var ilCompareResult = new ImageList
            {
                ImageStream = ((ImageListStreamer)(resources.GetObject("ilCompareResult.ImageStream"))),
                TransparentColor = Color.Transparent
            };
            ilCompareResult.Images.SetKeyName(0, "blue_equal.png");
            ilCompareResult.Images.SetKeyName(1, "incompatible");
            ilCompareResult.Images.SetKeyName(2, "warning");
            ilCompareResult.Images.SetKeyName(3, "error");
            ilCompareResult.Images.SetKeyName(4, "equal");
            ilCompareResult.Images.SetKeyName(5, "datadiff");
            ilCompareResult.Images.SetKeyName(6, "unknown");
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


        private void LoadSampleItems()
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
            }, "unknown")
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