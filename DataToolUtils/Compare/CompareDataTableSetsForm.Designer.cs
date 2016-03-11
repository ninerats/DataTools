using Craftsmaneer.DataTools.Compare.Control;
using Craftsmaneer.DataToolUtils;
using Craftsmaneer.DataToolUtils.Compare;


namespace Craftsmaneer.DataToolUtils
{
    partial class CompareDataTableSetsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareDataTableSetsForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup13 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup14 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup15 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup16 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup17 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup18 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup19 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup20 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup21 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup22 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup23 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup24 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup25 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup26 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup27 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup28 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup29 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup30 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup31 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup32 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup33 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup34 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup35 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup36 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup37 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup38 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup39 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup40 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup41 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup42 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup43 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup44 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup45 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup46 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup47 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup48 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup49 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup50 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup51 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup52 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup53 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup54 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup55 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup56 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup57 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup58 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup59 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup60 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup61 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup62 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup63 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup64 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup65 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup66 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup67 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup68 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup69 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup70 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRecCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lvCompareResults = new Craftsmaneer.DataTools.Compare.Control.DataTableSetListView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.chkShowIdentical = new System.Windows.Forms.CheckBox();
            this.cmdDumpDiff = new System.Windows.Forms.Button();
            this.cmdTools = new System.Windows.Forms.Button();
            this.chkGroup = new System.Windows.Forms.CheckBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.cmdViewDetails = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cmdExecuteMigration = new System.Windows.Forms.Button();
            this.txtWorkingFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTargetDb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSourceDb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdMigrate = new System.Windows.Forms.Button();
            this.chkIgnoreWhitespace = new System.Windows.Forms.CheckBox();
            this.cmdCompare = new System.Windows.Forms.Button();
            this.txtReplicaDtSet = new System.Windows.Forms.TextBox();
            this.txtMasterDtSet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdExport = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dataschemadiff");
            this.imageList1.Images.SetKeyName(1, "warning");
            this.imageList1.Images.SetKeyName(2, "error");
            this.imageList1.Images.SetKeyName(3, "equal");
            this.imageList1.Images.SetKeyName(4, "incompatible");
            this.imageList1.Images.SetKeyName(5, "datadiff");
            this.imageList1.Images.SetKeyName(6, "missing");
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.lblRecCount);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lvCompareResults);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(917, 474);
            this.panel2.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblStatus.Location = new System.Drawing.Point(26, 423);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(848, 37);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status label.  \r\nNothing to report.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecCount
            // 
            this.lblRecCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecCount.AutoSize = true;
            this.lblRecCount.Location = new System.Drawing.Point(815, 15);
            this.lblRecCount.Name = "lblRecCount";
            this.lblRecCount.Size = new System.Drawing.Size(60, 13);
            this.lblRecCount.TabIndex = 2;
            this.lblRecCount.Text = "no records.";
            this.lblRecCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Comparison Results";
            // 
            // lvCompareResults
            // 
            this.lvCompareResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            listViewGroup1.Header = "No Differences";
            listViewGroup1.Name = "equal";
            listViewGroup2.Header = "Differences (Incompatible)";
            listViewGroup2.Name = "incompatible";
            listViewGroup3.Header = "Differences (Compatible)";
            listViewGroup3.Name = "datadiff";
            listViewGroup4.Header = "Missing Tables";
            listViewGroup4.Name = "missing";
            listViewGroup5.Header = "Error Comparing";
            listViewGroup5.Name = "error";
            listViewGroup6.Header = "No Differences";
            listViewGroup6.Name = "equal";
            listViewGroup7.Header = "Differences (Incompatible)";
            listViewGroup7.Name = "incompatible";
            listViewGroup8.Header = "Differences (Compatible)";
            listViewGroup8.Name = "datadiff";
            listViewGroup9.Header = "Missing Tables";
            listViewGroup9.Name = "missing";
            listViewGroup10.Header = "Error Comparing";
            listViewGroup10.Name = "error";
            listViewGroup11.Header = "No Differences";
            listViewGroup11.Name = "equal";
            listViewGroup12.Header = "Differences (Incompatible)";
            listViewGroup12.Name = "incompatible";
            listViewGroup13.Header = "Differences (Compatible)";
            listViewGroup13.Name = "datadiff";
            listViewGroup14.Header = "Missing Tables";
            listViewGroup14.Name = "missing";
            listViewGroup15.Header = "Error Comparing";
            listViewGroup15.Name = "error";
            listViewGroup16.Header = "No Differences";
            listViewGroup16.Name = "equal";
            listViewGroup17.Header = "Differences (Incompatible)";
            listViewGroup17.Name = "incompatible";
            listViewGroup18.Header = "Differences (Compatible)";
            listViewGroup18.Name = "datadiff";
            listViewGroup19.Header = "Missing Tables";
            listViewGroup19.Name = "missing";
            listViewGroup20.Header = "Error Comparing";
            listViewGroup20.Name = "error";
            listViewGroup21.Header = "No Differences";
            listViewGroup21.Name = "equal";
            listViewGroup22.Header = "Differences (Incompatible)";
            listViewGroup22.Name = "incompatible";
            listViewGroup23.Header = "Differences (Compatible)";
            listViewGroup23.Name = "datadiff";
            listViewGroup24.Header = "Missing Tables";
            listViewGroup24.Name = "missing";
            listViewGroup25.Header = "Error Comparing";
            listViewGroup25.Name = "error";
            listViewGroup26.Header = "No Differences";
            listViewGroup26.Name = "equal";
            listViewGroup27.Header = "Differences (Incompatible)";
            listViewGroup27.Name = "incompatible";
            listViewGroup28.Header = "Differences (Compatible)";
            listViewGroup28.Name = "datadiff";
            listViewGroup29.Header = "Missing Tables";
            listViewGroup29.Name = "missing";
            listViewGroup30.Header = "Error Comparing";
            listViewGroup30.Name = "error";
            listViewGroup31.Header = "No Differences";
            listViewGroup31.Name = "equal";
            listViewGroup32.Header = "Differences (Incompatible)";
            listViewGroup32.Name = "incompatible";
            listViewGroup33.Header = "Differences (Compatible)";
            listViewGroup33.Name = "datadiff";
            listViewGroup34.Header = "Missing Tables";
            listViewGroup34.Name = "missing";
            listViewGroup35.Header = "Error Comparing";
            listViewGroup35.Name = "error";
            listViewGroup36.Header = "No Differences";
            listViewGroup36.Name = "equal";
            listViewGroup37.Header = "Differences (Incompatible)";
            listViewGroup37.Name = "incompatible";
            listViewGroup38.Header = "Differences (Compatible)";
            listViewGroup38.Name = "datadiff";
            listViewGroup39.Header = "Missing Tables";
            listViewGroup39.Name = "missing";
            listViewGroup40.Header = "Error Comparing";
            listViewGroup40.Name = "error";
            listViewGroup41.Header = "No Differences";
            listViewGroup41.Name = "equal";
            listViewGroup42.Header = "Differences (Incompatible)";
            listViewGroup42.Name = "incompatible";
            listViewGroup43.Header = "Differences (Compatible)";
            listViewGroup43.Name = "datadiff";
            listViewGroup44.Header = "Missing Tables";
            listViewGroup44.Name = "missing";
            listViewGroup45.Header = "Error Comparing";
            listViewGroup45.Name = "error";
            listViewGroup46.Header = "No Differences";
            listViewGroup46.Name = "equal";
            listViewGroup47.Header = "Differences (Incompatible)";
            listViewGroup47.Name = "incompatible";
            listViewGroup48.Header = "Differences (Compatible)";
            listViewGroup48.Name = "datadiff";
            listViewGroup49.Header = "Missing Tables";
            listViewGroup49.Name = "missing";
            listViewGroup50.Header = "Error Comparing";
            listViewGroup50.Name = "error";
            listViewGroup51.Header = "No Differences";
            listViewGroup51.Name = "equal";
            listViewGroup52.Header = "Differences (Incompatible)";
            listViewGroup52.Name = "incompatible";
            listViewGroup53.Header = "Differences (Compatible)";
            listViewGroup53.Name = "datadiff";
            listViewGroup54.Header = "Missing Tables";
            listViewGroup54.Name = "missing";
            listViewGroup55.Header = "Error Comparing";
            listViewGroup55.Name = "error";
            listViewGroup56.Header = "No Differences";
            listViewGroup56.Name = "equal";
            listViewGroup57.Header = "Differences (Incompatible)";
            listViewGroup57.Name = "incompatible";
            listViewGroup58.Header = "Differences (Compatible)";
            listViewGroup58.Name = "datadiff";
            listViewGroup59.Header = "Missing Tables";
            listViewGroup59.Name = "missing";
            listViewGroup60.Header = "Error Comparing";
            listViewGroup60.Name = "error";
            listViewGroup61.Header = "No Differences";
            listViewGroup61.Name = "equal";
            listViewGroup62.Header = "Differences (Incompatible)";
            listViewGroup62.Name = "incompatible";
            listViewGroup63.Header = "Differences (Compatible)";
            listViewGroup63.Name = "datadiff";
            listViewGroup64.Header = "Missing Tables";
            listViewGroup64.Name = "missing";
            listViewGroup65.Header = "Error Comparing";
            listViewGroup65.Name = "error";
            listViewGroup66.Header = "No Differences";
            listViewGroup66.Name = "equal";
            listViewGroup67.Header = "Differences (Incompatible)";
            listViewGroup67.Name = "incompatible";
            listViewGroup68.Header = "Differences (Compatible)";
            listViewGroup68.Name = "datadiff";
            listViewGroup69.Header = "Missing Tables";
            listViewGroup69.Name = "missing";
            listViewGroup70.Header = "Error Comparing";
            listViewGroup70.Name = "error";
            this.lvCompareResults.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8,
            listViewGroup9,
            listViewGroup10,
            listViewGroup11,
            listViewGroup12,
            listViewGroup13,
            listViewGroup14,
            listViewGroup15,
            listViewGroup16,
            listViewGroup17,
            listViewGroup18,
            listViewGroup19,
            listViewGroup20,
            listViewGroup21,
            listViewGroup22,
            listViewGroup23,
            listViewGroup24,
            listViewGroup25,
            listViewGroup26,
            listViewGroup27,
            listViewGroup28,
            listViewGroup29,
            listViewGroup30,
            listViewGroup31,
            listViewGroup32,
            listViewGroup33,
            listViewGroup34,
            listViewGroup35,
            listViewGroup36,
            listViewGroup37,
            listViewGroup38,
            listViewGroup39,
            listViewGroup40,
            listViewGroup41,
            listViewGroup42,
            listViewGroup43,
            listViewGroup44,
            listViewGroup45,
            listViewGroup46,
            listViewGroup47,
            listViewGroup48,
            listViewGroup49,
            listViewGroup50,
            listViewGroup51,
            listViewGroup52,
            listViewGroup53,
            listViewGroup54,
            listViewGroup55,
            listViewGroup56,
            listViewGroup57,
            listViewGroup58,
            listViewGroup59,
            listViewGroup60,
            listViewGroup61,
            listViewGroup62,
            listViewGroup63,
            listViewGroup64,
            listViewGroup65,
            listViewGroup66,
            listViewGroup67,
            listViewGroup68,
            listViewGroup69,
            listViewGroup70});
            this.lvCompareResults.Location = new System.Drawing.Point(22, 31);
            this.lvCompareResults.Name = "lvCompareResults";
            this.lvCompareResults.ShowIdentical = false;
            this.lvCompareResults.Size = new System.Drawing.Size(853, 387);
            this.lvCompareResults.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvCompareResults.TabIndex = 0;
            this.lvCompareResults.TableList = null;
            this.lvCompareResults.TableSetDiff = null;
            this.lvCompareResults.UseCompatibleStateImageBehavior = false;
            this.lvCompareResults.View = System.Windows.Forms.View.Details;
            this.lvCompareResults.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvCompareResults_ItemChecked);
            this.lvCompareResults.DoubleClick += new System.EventHandler(this.lvCompareResults_DoubleClick);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.chkShowIdentical);
            this.pnlBottom.Controls.Add(this.cmdDumpDiff);
            this.pnlBottom.Controls.Add(this.cmdTools);
            this.pnlBottom.Controls.Add(this.chkGroup);
            this.pnlBottom.Controls.Add(this.cmdOk);
            this.pnlBottom.Controls.Add(this.cmdSettings);
            this.pnlBottom.Controls.Add(this.cmdViewDetails);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 568);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(917, 43);
            this.pnlBottom.TabIndex = 1;
            // 
            // chkShowIdentical
            // 
            this.chkShowIdentical.AutoSize = true;
            this.chkShowIdentical.Location = new System.Drawing.Point(333, 11);
            this.chkShowIdentical.Name = "chkShowIdentical";
            this.chkShowIdentical.Size = new System.Drawing.Size(96, 17);
            this.chkShowIdentical.TabIndex = 6;
            this.chkShowIdentical.Text = "Show Identical";
            this.chkShowIdentical.UseVisualStyleBackColor = true;
            // 
            // cmdDumpDiff
            // 
            this.cmdDumpDiff.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDumpDiff.Location = new System.Drawing.Point(501, 6);
            this.cmdDumpDiff.Name = "cmdDumpDiff";
            this.cmdDumpDiff.Size = new System.Drawing.Size(75, 23);
            this.cmdDumpDiff.TabIndex = 5;
            this.cmdDumpDiff.Text = "Dump Diff";
            this.cmdDumpDiff.UseVisualStyleBackColor = true;
            this.cmdDumpDiff.Click += new System.EventHandler(this.cmdDumpDiff_Click);
            // 
            // cmdTools
            // 
            this.cmdTools.Location = new System.Drawing.Point(588, 7);
            this.cmdTools.Name = "cmdTools";
            this.cmdTools.Size = new System.Drawing.Size(75, 23);
            this.cmdTools.TabIndex = 4;
            this.cmdTools.Text = "Tools";
            this.cmdTools.UseVisualStyleBackColor = true;
            this.cmdTools.Click += new System.EventHandler(this.cmdTools_Click);
            // 
            // chkGroup
            // 
            this.chkGroup.AutoSize = true;
            this.chkGroup.Checked = true;
            this.chkGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGroup.Location = new System.Drawing.Point(233, 12);
            this.chkGroup.Name = "chkGroup";
            this.chkGroup.Size = new System.Drawing.Size(93, 17);
            this.chkGroup.TabIndex = 3;
            this.chkGroup.Text = "Group Results";
            this.chkGroup.UseVisualStyleBackColor = true;
            this.chkGroup.CheckedChanged += new System.EventHandler(this.chkGroup_CheckedChanged);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(778, 8);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(96, 23);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdSettings
            // 
            this.cmdSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSettings.Location = new System.Drawing.Point(126, 8);
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Size = new System.Drawing.Size(92, 23);
            this.cmdSettings.TabIndex = 1;
            this.cmdSettings.Text = "Settings...";
            this.cmdSettings.UseVisualStyleBackColor = true;
            // 
            // cmdViewDetails
            // 
            this.cmdViewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdViewDetails.Location = new System.Drawing.Point(21, 8);
            this.cmdViewDetails.Name = "cmdViewDetails";
            this.cmdViewDetails.Size = new System.Drawing.Size(99, 23);
            this.cmdViewDetails.TabIndex = 0;
            this.cmdViewDetails.Text = "View Details...";
            this.cmdViewDetails.UseVisualStyleBackColor = true;
            this.cmdViewDetails.Click += new System.EventHandler(this.cmdViewDetails_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmdExport);
            this.pnlTop.Controls.Add(this.cmdExecuteMigration);
            this.pnlTop.Controls.Add(this.txtWorkingFolder);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Controls.Add(this.txtTargetDb);
            this.pnlTop.Controls.Add(this.label5);
            this.pnlTop.Controls.Add(this.txtSourceDb);
            this.pnlTop.Controls.Add(this.label4);
            this.pnlTop.Controls.Add(this.cmdMigrate);
            this.pnlTop.Controls.Add(this.chkIgnoreWhitespace);
            this.pnlTop.Controls.Add(this.cmdCompare);
            this.pnlTop.Controls.Add(this.txtReplicaDtSet);
            this.pnlTop.Controls.Add(this.txtMasterDtSet);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(917, 94);
            this.pnlTop.TabIndex = 0;
            this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmdExecuteMigration
            // 
            this.cmdExecuteMigration.Location = new System.Drawing.Point(724, 145);
            this.cmdExecuteMigration.Name = "cmdExecuteMigration";
            this.cmdExecuteMigration.Size = new System.Drawing.Size(75, 23);
            this.cmdExecuteMigration.TabIndex = 10;
            this.cmdExecuteMigration.Text = "Execute";
            this.cmdExecuteMigration.UseVisualStyleBackColor = true;
            this.cmdExecuteMigration.Click += new System.EventHandler(this.cmdExecuteMigration_Click);
            // 
            // txtWorkingFolder
            // 
            this.txtWorkingFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtWorkingFolder_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtWorkingFolder.Location = new System.Drawing.Point(218, 147);
            this.txtWorkingFolder.Name = "txtWorkingFolder";
            this.txtWorkingFolder.Size = new System.Drawing.Size(500, 20);
            this.txtWorkingFolder.TabIndex = 9;
            this.txtWorkingFolder.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtWorkingFolder_Text;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Working Path";
            // 
            // txtTargetDb
            // 
            this.txtTargetDb.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtTargetDatabase_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTargetDb.Location = new System.Drawing.Point(218, 121);
            this.txtTargetDb.Name = "txtTargetDb";
            this.txtTargetDb.Size = new System.Drawing.Size(500, 20);
            this.txtTargetDb.TabIndex = 9;
            this.txtTargetDb.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtTargetDatabase_Text;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Target Database";
            // 
            // txtSourceDb
            // 
            this.txtSourceDb.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtSourceDatabase_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSourceDb.Location = new System.Drawing.Point(218, 95);
            this.txtSourceDb.Name = "txtSourceDb";
            this.txtSourceDb.Size = new System.Drawing.Size(500, 20);
            this.txtSourceDb.TabIndex = 9;
            this.txtSourceDb.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtSourceDatabase_Text;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Source Database";
            // 
            // cmdMigrate
            // 
            this.cmdMigrate.Location = new System.Drawing.Point(120, 63);
            this.cmdMigrate.Name = "cmdMigrate";
            this.cmdMigrate.Size = new System.Drawing.Size(86, 23);
            this.cmdMigrate.TabIndex = 7;
            this.cmdMigrate.Text = "Migrate...";
            this.cmdMigrate.UseVisualStyleBackColor = true;
            this.cmdMigrate.Visible = false;
            this.cmdMigrate.Click += new System.EventHandler(this.cmdMigrate_Click);
            // 
            // chkIgnoreWhitespace
            // 
            this.chkIgnoreWhitespace.AutoSize = true;
            this.chkIgnoreWhitespace.Checked = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.chkIgnoreWhitespace_Checked;
            this.chkIgnoreWhitespace.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "chkIgnoreWhitespace_Checked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIgnoreWhitespace.Location = new System.Drawing.Point(758, 39);
            this.chkIgnoreWhitespace.Name = "chkIgnoreWhitespace";
            this.chkIgnoreWhitespace.Size = new System.Drawing.Size(116, 17);
            this.chkIgnoreWhitespace.TabIndex = 3;
            this.chkIgnoreWhitespace.Text = "Ignore Whitespace";
            this.chkIgnoreWhitespace.UseVisualStyleBackColor = true;
            // 
            // cmdCompare
            // 
            this.cmdCompare.Location = new System.Drawing.Point(22, 63);
            this.cmdCompare.Name = "cmdCompare";
            this.cmdCompare.Size = new System.Drawing.Size(92, 23);
            this.cmdCompare.TabIndex = 2;
            this.cmdCompare.Text = "Compare";
            this.cmdCompare.UseVisualStyleBackColor = true;
            this.cmdCompare.Click += new System.EventHandler(this.cmdCompare_Click);
            // 
            // txtReplicaDtSet
            // 
            this.txtReplicaDtSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplicaDtSet.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtReplicaDtSet_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtReplicaDtSet.Location = new System.Drawing.Point(139, 36);
            this.txtReplicaDtSet.Name = "txtReplicaDtSet";
            this.txtReplicaDtSet.Size = new System.Drawing.Size(589, 20);
            this.txtReplicaDtSet.TabIndex = 1;
            this.txtReplicaDtSet.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtReplicaDtSet_Text;
            // 
            // txtMasterDtSet
            // 
            this.txtMasterDtSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterDtSet.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtMasterDataTableSet_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtMasterDtSet.Location = new System.Drawing.Point(139, 10);
            this.txtMasterDtSet.Name = "txtMasterDtSet";
            this.txtMasterDtSet.Size = new System.Drawing.Size(589, 20);
            this.txtMasterDtSet.TabIndex = 1;
            this.txtMasterDtSet.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtMasterDataTableSet_Text;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Modified DataTableSet";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original DataTableSet";
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(724, 95);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(75, 23);
            this.cmdExport.TabIndex = 11;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // CompareDataTableSetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "CompareDataTableSetsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CompareDataTableSetsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CompareDataTableSets_FormClosing);
            this.Load += new System.EventHandler(this.CompareDataTableSetsForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button cmdCompare;
        private System.Windows.Forms.TextBox txtReplicaDtSet;
        private System.Windows.Forms.TextBox txtMasterDtSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRecCount;
        private System.Windows.Forms.Label label3;
        private DataTableSetListView lvCompareResults;
      
        private System.Windows.Forms.Button cmdSettings;
        private System.Windows.Forms.Button cmdViewDetails;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkGroup;
        private System.Windows.Forms.Button cmdTools;
        private System.Windows.Forms.CheckBox chkIgnoreWhitespace;
        private System.Windows.Forms.Button cmdDumpDiff;
        private System.Windows.Forms.CheckBox chkShowIdentical;
        private System.Windows.Forms.Button cmdMigrate;
        private System.Windows.Forms.Button cmdExecuteMigration;
        private System.Windows.Forms.TextBox txtWorkingFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTargetDb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSourceDb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdExport;
    }
}