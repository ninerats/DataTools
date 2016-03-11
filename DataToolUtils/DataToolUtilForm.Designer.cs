using Craftsmaneer.DataTools.Compare.Control;
using Craftsmaneer.DataToolUtils.Compare;

namespace Craftsmaneer.DataToolUtils
{
    partial class DataToolUtilForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Table 1",
            "132"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Table 1",
            "132"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Table 1",
            "132"}, -1);
            this.tabUtils = new System.Windows.Forms.TabControl();
            this.tabExportDT = new System.Windows.Forms.TabPage();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.cmdMakeConfig = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdExport = new System.Windows.Forms.Button();
            this.lvwTables = new TableListView();
            this.mnuExportOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.tabImportDataTable = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompareToTable = new System.Windows.Forms.TextBox();
            this.cmdCompare = new System.Windows.Forms.Button();
            this.lblTableName = new System.Windows.Forms.Label();
            this.dgvImported = new System.Windows.Forms.DataGridView();
            this.cmdImportDataTable = new System.Windows.Forms.Button();
            this.txtImportDataTablePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.tabUtils.SuspendLayout();
            this.tabExportDT.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.mnuExportOptions.SuspendLayout();
            this.tabImportDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImported)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabUtils
            // 
            this.tabUtils.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabUtils.Controls.Add(this.tabExportDT);
            this.tabUtils.Controls.Add(this.tabImportDataTable);
            this.tabUtils.Location = new System.Drawing.Point(12, 67);
            this.tabUtils.Name = "tabUtils";
            this.tabUtils.SelectedIndex = 0;
            this.tabUtils.Size = new System.Drawing.Size(710, 443);
            this.tabUtils.TabIndex = 0;
            // 
            // tabExportDT
            // 
            this.tabExportDT.Controls.Add(this.pnlBottom);
            this.tabExportDT.Controls.Add(this.lvwTables);
            this.tabExportDT.Location = new System.Drawing.Point(4, 22);
            this.tabExportDT.Name = "tabExportDT";
            this.tabExportDT.Padding = new System.Windows.Forms.Padding(3);
            this.tabExportDT.Size = new System.Drawing.Size(702, 417);
            this.tabExportDT.TabIndex = 0;
            this.tabExportDT.Text = "Export Datatable";
            this.tabExportDT.UseVisualStyleBackColor = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottom.Controls.Add(this.cmdMakeConfig);
            this.pnlBottom.Controls.Add(this.button1);
            this.pnlBottom.Controls.Add(this.txtExportPath);
            this.pnlBottom.Controls.Add(this.label2);
            this.pnlBottom.Controls.Add(this.cmdExport);
            this.pnlBottom.Location = new System.Drawing.Point(18, 340);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(669, 71);
            this.pnlBottom.TabIndex = 1;
            // 
            // cmdMakeConfig
            // 
            this.cmdMakeConfig.Location = new System.Drawing.Point(173, 32);
            this.cmdMakeConfig.Name = "cmdMakeConfig";
            this.cmdMakeConfig.Size = new System.Drawing.Size(75, 23);
            this.cmdMakeConfig.TabIndex = 8;
            this.cmdMakeConfig.Text = "Make Cfg";
            this.cmdMakeConfig.UseVisualStyleBackColor = true;
            this.cmdMakeConfig.Click += new System.EventHandler(this.cmdMakeConfig_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtExportPath
            // 
            this.txtExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExportPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtExportPath_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtExportPath.Location = new System.Drawing.Point(78, 6);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(577, 20);
            this.txtExportPath.TabIndex = 6;
            this.txtExportPath.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtExportPath_Text;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Export Path:";
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(10, 32);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(75, 23);
            this.cmdExport.TabIndex = 4;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // lvwTables
            // 
            this.lvwTables .Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTables.CheckBoxes = true;
            this.lvwTables.ConnectionString = null;
            this.lvwTables.ContextMenuStrip = this.mnuExportOptions;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            this.lvwTables.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lvwTables.Location = new System.Drawing.Point(18, 7);
            this.lvwTables.Name = "lvwTables";
            this.lvwTables.Size = new System.Drawing.Size(669, 322);
            this.lvwTables.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwTables.TabIndex = 0;
            this.lvwTables.UseCompatibleStateImageBehavior = false;
            this.lvwTables.View = System.Windows.Forms.View.Details;
            // 
            // mnuExportOptions
            // 
            this.mnuExportOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSelectAll,
            this.miSelectNone});
            this.mnuExportOptions.Name = "mnuExportOptions";
            this.mnuExportOptions.Size = new System.Drawing.Size(138, 48);
            // 
            // miSelectAll
            // 
            this.miSelectAll.Name = "miSelectAll";
            this.miSelectAll.Size = new System.Drawing.Size(137, 22);
            this.miSelectAll.Text = "Select All";
            this.miSelectAll.Click += new System.EventHandler(this.miSelectAll_Click);
            // 
            // miSelectNone
            // 
            this.miSelectNone.Name = "miSelectNone";
            this.miSelectNone.Size = new System.Drawing.Size(137, 22);
            this.miSelectNone.Text = "Select None";
            this.miSelectNone.Click += new System.EventHandler(this.miSelectNone_Click);
            // 
            // tabImportDataTable
            // 
            this.tabImportDataTable.Controls.Add(this.label4);
            this.tabImportDataTable.Controls.Add(this.txtCompareToTable);
            this.tabImportDataTable.Controls.Add(this.cmdCompare);
            this.tabImportDataTable.Controls.Add(this.lblTableName);
            this.tabImportDataTable.Controls.Add(this.dgvImported);
            this.tabImportDataTable.Controls.Add(this.cmdImportDataTable);
            this.tabImportDataTable.Controls.Add(this.txtImportDataTablePath);
            this.tabImportDataTable.Controls.Add(this.label3);
            this.tabImportDataTable.Location = new System.Drawing.Point(4, 22);
            this.tabImportDataTable.Name = "tabImportDataTable";
            this.tabImportDataTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabImportDataTable.Size = new System.Drawing.Size(702, 417);
            this.tabImportDataTable.TabIndex = 1;
            this.tabImportDataTable.Text = "Import DataTable";
            this.tabImportDataTable.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Compare to table:";
            // 
            // txtCompareToTable
            // 
            this.txtCompareToTable.Location = new System.Drawing.Point(209, 386);
            this.txtCompareToTable.Name = "txtCompareToTable";
            this.txtCompareToTable.Size = new System.Drawing.Size(236, 20);
            this.txtCompareToTable.TabIndex = 6;
            // 
            // cmdCompare
            // 
            this.cmdCompare.Location = new System.Drawing.Point(24, 384);
            this.cmdCompare.Name = "cmdCompare";
            this.cmdCompare.Size = new System.Drawing.Size(75, 23);
            this.cmdCompare.TabIndex = 5;
            this.cmdCompare.Text = "Compare";
            this.cmdCompare.UseVisualStyleBackColor = true;
            this.cmdCompare.Click += new System.EventHandler(this.cmdCompare_Click);
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTableName.Location = new System.Drawing.Point(21, 42);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(272, 13);
            this.lblTableName.TabIndex = 4;
            this.lblTableName.Text = "Select an exported Datatable and click Import.";
            // 
            // dgvImported
            // 
            this.dgvImported.AllowUserToOrderColumns = true;
            this.dgvImported.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvImported.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImported.Location = new System.Drawing.Point(21, 58);
            this.dgvImported.Name = "dgvImported";
            this.dgvImported.Size = new System.Drawing.Size(656, 319);
            this.dgvImported.TabIndex = 3;
            // 
            // cmdImportDataTable
            // 
            this.cmdImportDataTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdImportDataTable.Location = new System.Drawing.Point(602, 13);
            this.cmdImportDataTable.Name = "cmdImportDataTable";
            this.cmdImportDataTable.Size = new System.Drawing.Size(75, 23);
            this.cmdImportDataTable.TabIndex = 2;
            this.cmdImportDataTable.Text = "Import";
            this.cmdImportDataTable.UseVisualStyleBackColor = true;
            this.cmdImportDataTable.Click += new System.EventHandler(this.cmdImportDataTable_Click);
            // 
            // txtImportDataTablePath
            // 
            this.txtImportDataTablePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImportDataTablePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtImportDataTablePath_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtImportDataTablePath.Location = new System.Drawing.Point(92, 15);
            this.txtImportDataTablePath.Name = "txtImportDataTablePath";
            this.txtImportDataTablePath.Size = new System.Drawing.Size(504, 20);
            this.txtImportDataTablePath.TabIndex = 1;
            this.txtImportDataTablePath.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtImportDataTablePath_Text;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Import From:";
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.Controls.Add(this.cmdConnect);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.txtConnStr);
            this.pnlTop.Location = new System.Drawing.Point(16, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(702, 58);
            this.pnlTop.TabIndex = 1;
            // 
            // cmdConnect
            // 
            this.cmdConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConnect.Location = new System.Drawing.Point(616, 27);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 23);
            this.cmdConnect.TabIndex = 2;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connection String:";
            // 
            // txtConnStr
            // 
            this.txtConnStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnStr.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Craftsmaneer.DataToolUtils.Properties.Settings.Default, "txtConnStr_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtConnStr.Location = new System.Drawing.Point(18, 26);
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.Size = new System.Drawing.Size(592, 20);
            this.txtConnStr.TabIndex = 0;
            this.txtConnStr.Text = global::Craftsmaneer.DataToolUtils.Properties.Settings.Default.txtConnStr_Text;
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // DataToolUtilForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 522);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.tabUtils);
            this.Name = "DataToolUtilForm";
            this.Text = "DataTools Utilities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataToolUtilForm_FormClosing);
            this.tabUtils.ResumeLayout(false);
            this.tabExportDT.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.mnuExportOptions.ResumeLayout(false);
            this.tabImportDataTable.ResumeLayout(false);
            this.tabImportDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImported)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabUtils;
        private System.Windows.Forms.TabPage tabExportDT;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnStr;
        private TableListView lvwTables;

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.TabPage tabImportDataTable;
        private System.Windows.Forms.TextBox txtImportDataTablePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvImported;
        private System.Windows.Forms.Button cmdImportDataTable;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCompareToTable;
        private System.Windows.Forms.Button cmdCompare;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip mnuExportOptions;
        private System.Windows.Forms.ToolStripMenuItem miSelectAll;
        private System.Windows.Forms.ToolStripMenuItem miSelectNone;
        private System.Windows.Forms.Button cmdMakeConfig;
    }
}

