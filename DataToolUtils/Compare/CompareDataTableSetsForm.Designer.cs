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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCompare = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.chkGroup = new System.Windows.Forms.CheckBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.cmdViewDetails = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRecCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmdTools = new System.Windows.Forms.Button();
            this.txtReplicaDtSet = new System.Windows.Forms.TextBox();
            this.txtMasterDtSet = new System.Windows.Forms.TextBox();
            this.lvCompareResults = new Craftsmaneer.DataToolUtils.Compare.DataTableSetListView();
            this.chkIgnoreWhitespace = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkIgnoreWhitespace);
            this.panel1.Controls.Add(this.cmdCompare);
            this.panel1.Controls.Add(this.txtReplicaDtSet);
            this.panel1.Controls.Add(this.txtMasterDtSet);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 92);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmdCompare
            // 
            this.cmdCompare.Location = new System.Drawing.Point(22, 63);
            this.cmdCompare.Name = "cmdCompare";
            this.cmdCompare.Size = new System.Drawing.Size(75, 23);
            this.cmdCompare.TabIndex = 2;
            this.cmdCompare.Text = "Compare";
            this.cmdCompare.UseVisualStyleBackColor = true;
            this.cmdCompare.Click += new System.EventHandler(this.cmdCompare_Click);
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
            this.pnlBottom.Controls.Add(this.cmdTools);
            this.pnlBottom.Controls.Add(this.chkGroup);
            this.pnlBottom.Controls.Add(this.cmdSave);
            this.pnlBottom.Controls.Add(this.cmdOk);
            this.pnlBottom.Controls.Add(this.cmdSettings);
            this.pnlBottom.Controls.Add(this.cmdViewDetails);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 508);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(797, 43);
            this.pnlBottom.TabIndex = 1;
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
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(555, 8);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(97, 23);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(658, 8);
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
            this.cmdViewDetails.Click += new System.EventHandler(this.cmdViewDetails_Click);
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
            this.panel2.Size = new System.Drawing.Size(797, 416);
            this.panel2.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblStatus.Location = new System.Drawing.Point(26, 365);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(728, 37);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status label.  \r\nNothing to report.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecCount
            // 
            this.lblRecCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecCount.AutoSize = true;
            this.lblRecCount.Location = new System.Drawing.Point(695, 15);
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
            // cmdTools
            // 
            this.cmdTools.Location = new System.Drawing.Point(474, 8);
            this.cmdTools.Name = "cmdTools";
            this.cmdTools.Size = new System.Drawing.Size(75, 23);
            this.cmdTools.TabIndex = 4;
            this.cmdTools.Text = "Tools";
            this.cmdTools.UseVisualStyleBackColor = true;
            this.cmdTools.Click += new System.EventHandler(this.cmdTools_Click);
            // 
            // txtReplicaDtSet
            // 
            this.txtReplicaDtSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplicaDtSet.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtReplicaDtSet_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtReplicaDtSet.Location = new System.Drawing.Point(139, 36);
            this.txtReplicaDtSet.Name = "txtReplicaDtSet";
            this.txtReplicaDtSet.Size = new System.Drawing.Size(537, 20);
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
            this.txtMasterDtSet.Size = new System.Drawing.Size(537, 20);
            this.txtMasterDtSet.TabIndex = 1;
            this.txtMasterDtSet.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtMasterDataTableSet_Text;
            // 
            // lvCompareResults
            // 
            this.lvCompareResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCompareResults.CheckBoxes = true;
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
            this.lvCompareResults.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
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
            listViewGroup40});
            this.lvCompareResults.Location = new System.Drawing.Point(22, 31);
            this.lvCompareResults.Name = "lvCompareResults";
            this.lvCompareResults.Size = new System.Drawing.Size(733, 329);
            this.lvCompareResults.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvCompareResults.TabIndex = 0;
            this.lvCompareResults.TableSetDiff = null;
            this.lvCompareResults.UseCompatibleStateImageBehavior = false;
            this.lvCompareResults.View = System.Windows.Forms.View.Details;
            // 
            // chkIgnoreWhitespace
            // 
            this.chkIgnoreWhitespace.AutoSize = true;
            this.chkIgnoreWhitespace.Location = new System.Drawing.Point(124, 67);
            this.chkIgnoreWhitespace.Name = "chkIgnoreWhitespace";
            this.chkIgnoreWhitespace.Size = new System.Drawing.Size(116, 17);
            this.chkIgnoreWhitespace.TabIndex = 3;
            this.chkIgnoreWhitespace.Text = "Ignore Whitespace";
            this.chkIgnoreWhitespace.UseVisualStyleBackColor = true;
            // 
            // CompareDataTableSetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 551);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.panel1);
            this.Name = "CompareDataTableSetsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CompareDataTableSetsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CompareDataTableSets_FormClosing);
            this.Load += new System.EventHandler(this.CompareDataTableSetsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
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
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.CheckBox chkGroup;
        private System.Windows.Forms.Button cmdTools;
        private System.Windows.Forms.CheckBox chkIgnoreWhitespace;
    }
}