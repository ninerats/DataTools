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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("No Differences", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Differences (Incompatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Differences (Compatible)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Missing Tables", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Error Comparing", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareDataTableSetsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCompare = new System.Windows.Forms.Button();
            this.txtReplicaDtSet = new System.Windows.Forms.TextBox();
            this.txtMasterDtSet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.cmdViewDetails = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRecCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lvCompareResults = new Craftsmaneer.DataToolUtils.Compare.DataTableSetListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdCompare);
            this.panel1.Controls.Add(this.txtReplicaDtSet);
            this.panel1.Controls.Add(this.txtMasterDtSet);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 92);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmdCompare
            // 
            this.cmdCompare.Location = new System.Drawing.Point(22, 59);
            this.cmdCompare.Name = "cmdCompare";
            this.cmdCompare.Size = new System.Drawing.Size(75, 23);
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
            this.txtReplicaDtSet.Size = new System.Drawing.Size(480, 20);
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
            this.txtMasterDtSet.Size = new System.Drawing.Size(480, 20);
            this.txtMasterDtSet.TabIndex = 1;
            this.txtMasterDtSet.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtMasterDataTableSet_Text;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Replica DataTableSet";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Master DataTableSet";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.cmdOk);
            this.pnlBottom.Controls.Add(this.cmdSettings);
            this.pnlBottom.Controls.Add(this.cmdViewDetails);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 502);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(740, 43);
            this.pnlBottom.TabIndex = 1;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(601, 8);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(97, 23);
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
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.lblRecCount);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lvCompareResults);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(740, 410);
            this.panel2.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblStatus.Location = new System.Drawing.Point(26, 359);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(671, 37);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status label.  \r\nNothing to report.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecCount
            // 
            this.lblRecCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecCount.AutoSize = true;
            this.lblRecCount.Location = new System.Drawing.Point(638, 15);
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
            this.lvCompareResults.CheckBoxes = true;
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
            this.lvCompareResults.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5});
            this.lvCompareResults.Location = new System.Drawing.Point(22, 31);
            this.lvCompareResults.Name = "lvCompareResults";
            this.lvCompareResults.Size = new System.Drawing.Size(676, 323);
            this.lvCompareResults.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvCompareResults.TabIndex = 0;
            this.lvCompareResults.TableSetDiff = null;
            this.lvCompareResults.UseCompatibleStateImageBehavior = false;
            this.lvCompareResults.View = System.Windows.Forms.View.Details;
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
            // CompareDataTableSetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 545);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.panel1);
            this.Name = "CompareDataTableSetsForm";
            this.Text = "CompareDataTableSetsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CompareDataTableSets_FormClosing);
            this.Load += new System.EventHandler(this.CompareDataTableSetsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
    }
}