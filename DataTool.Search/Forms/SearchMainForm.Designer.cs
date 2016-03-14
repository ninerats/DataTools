namespace DataTool.Search.Forms
{
    partial class SearchMainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.cmdSource = new System.Windows.Forms.Button();
            this.lblDsSummary = new System.Windows.Forms.Label();
            this.dataSearchListView1 = new DataTool.Search.Control.DataSearchListView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search Value:";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(311, 314);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 23);
            this.cmdSearch.TabIndex = 6;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToOrderColumns = true;
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(35, 350);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(669, 150);
            this.dgvResults.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(38, 512);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(636, 25);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "label3";
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DataTool.Search.Properties.Settings.Default, "txtSearchValue_Txt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSearchValue.Location = new System.Drawing.Point(113, 316);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(176, 20);
            this.txtSearchValue.TabIndex = 5;
            this.txtSearchValue.Text = global::DataTool.Search.Properties.Settings.Default.txtSearchValue_Txt;
            // 
            // cmdSource
            // 
            this.cmdSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSource.Location = new System.Drawing.Point(629, 27);
            this.cmdSource.Name = "cmdSource";
            this.cmdSource.Size = new System.Drawing.Size(75, 23);
            this.cmdSource.TabIndex = 10;
            this.cmdSource.Text = "Source...";
            this.cmdSource.UseVisualStyleBackColor = true;
            this.cmdSource.Click += new System.EventHandler(this.cmdSource_Click);
            // 
            // lblDsSummary
            // 
            this.lblDsSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDsSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDsSummary.Location = new System.Drawing.Point(29, 27);
            this.lblDsSummary.Name = "lblDsSummary";
            this.lblDsSummary.Size = new System.Drawing.Size(594, 23);
            this.lblDsSummary.TabIndex = 11;
            this.lblDsSummary.Text = "No DataSource selected.";
            this.lblDsSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataSearchListView1
            // 
            this.dataSearchListView1.Location = new System.Drawing.Point(35, 68);
            this.dataSearchListView1.Name = "dataSearchListView1";
            this.dataSearchListView1.Size = new System.Drawing.Size(669, 220);
            this.dataSearchListView1.TabIndex = 12;
            this.dataSearchListView1.UseCompatibleStateImageBehavior = false;
            this.dataSearchListView1.View = System.Windows.Forms.View.Details;
            // 
            // SearchMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 547);
            this.Controls.Add(this.dataSearchListView1);
            this.Controls.Add(this.lblDsSummary);
            this.Controls.Add(this.cmdSource);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.txtSearchValue);
            this.Controls.Add(this.label2);
            this.Name = "SearchMainForm";
            this.Text = "Search Main Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectTableForm_FormClosing);
            this.Load += new System.EventHandler(this.SearchMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button cmdSource;
        private System.Windows.Forms.Label lblDsSummary;
        private Control.DataSearchListView dataSearchListView1;
    }
}