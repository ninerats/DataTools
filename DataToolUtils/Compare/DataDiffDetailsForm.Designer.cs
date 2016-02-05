using Craftsmaneer.DataTools.Compare.Control;

namespace Craftsmaneer.DataToolUtils.Compare
{
    partial class DataDiffDetailsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTableName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdDiffsOnly = new System.Windows.Forms.Button();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabResults = new System.Windows.Forms.TabControl();
            this.pagDelta = new System.Windows.Forms.TabPage();
            this.tdgvDelta = new Craftsmaneer.DataTools.Compare.Control.DataTableDiffGridView();
            this.pagOriginal = new System.Windows.Forms.TabPage();
            this.dgvOriginal = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.pagDelta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdgvDelta)).BeginInit();
            this.pagOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTableName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 48);
            this.panel1.TabIndex = 0;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableName.Location = new System.Drawing.Point(16, 9);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(222, 25);
            this.lblTableName.TabIndex = 0;
            this.lblTableName.Text = "Schema.TableName";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdDiffsOnly);
            this.panel2.Controls.Add(this.cmdPrevious);
            this.panel2.Controls.Add(this.cmdNext);
            this.panel2.Controls.Add(this.cmdOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 586);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(851, 48);
            this.panel2.TabIndex = 1;
            // 
            // cmdDiffsOnly
            // 
            this.cmdDiffsOnly.Location = new System.Drawing.Point(197, 13);
            this.cmdDiffsOnly.Name = "cmdDiffsOnly";
            this.cmdDiffsOnly.Size = new System.Drawing.Size(75, 23);
            this.cmdDiffsOnly.TabIndex = 3;
            this.cmdDiffsOnly.Text = "Diffs Only";
            this.cmdDiffsOnly.UseVisualStyleBackColor = true;
            this.cmdDiffsOnly.Click += new System.EventHandler(this.cmdDiffsOnly_Click);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Location = new System.Drawing.Point(21, 13);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(75, 23);
            this.cmdPrevious.TabIndex = 2;
            this.cmdPrevious.Text = "Previous";
            this.cmdPrevious.UseVisualStyleBackColor = true;
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(102, 13);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(75, 23);
            this.cmdNext.TabIndex = 1;
            this.cmdNext.Text = "Next";
            this.cmdNext.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(760, 13);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabResults);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(851, 538);
            this.panel3.TabIndex = 2;
            // 
            // tabResults
            // 
            this.tabResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabResults.Controls.Add(this.pagDelta);
            this.tabResults.Controls.Add(this.pagOriginal);
            this.tabResults.Location = new System.Drawing.Point(4, 7);
            this.tabResults.Name = "tabResults";
            this.tabResults.SelectedIndex = 0;
            this.tabResults.Size = new System.Drawing.Size(835, 514);
            this.tabResults.TabIndex = 0;
            // 
            // pagDelta
            // 
            this.pagDelta.Controls.Add(this.tdgvDelta);
            this.pagDelta.Location = new System.Drawing.Point(4, 22);
            this.pagDelta.Name = "pagDelta";
            this.pagDelta.Padding = new System.Windows.Forms.Padding(3);
            this.pagDelta.Size = new System.Drawing.Size(827, 488);
            this.pagDelta.TabIndex = 0;
            this.pagDelta.Text = "Current Values";
            this.pagDelta.UseVisualStyleBackColor = true;
            // 
            // tdgvDelta
            // 
            this.tdgvDelta.AllowUserToAddRows = false;
            this.tdgvDelta.AllowUserToDeleteRows = false;
            this.tdgvDelta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tdgvDelta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tdgvDelta.Location = new System.Drawing.Point(3, 3);
            this.tdgvDelta.Name = "tdgvDelta";
            this.tdgvDelta.ReadOnly = true;
            this.tdgvDelta.Size = new System.Drawing.Size(821, 482);
            this.tdgvDelta.TabIndex = 1;
            // 
            // pagOriginal
            // 
            this.pagOriginal.Controls.Add(this.dgvOriginal);
            this.pagOriginal.Location = new System.Drawing.Point(4, 22);
            this.pagOriginal.Name = "pagOriginal";
            this.pagOriginal.Padding = new System.Windows.Forms.Padding(3);
            this.pagOriginal.Size = new System.Drawing.Size(827, 488);
            this.pagOriginal.TabIndex = 1;
            this.pagOriginal.Text = "Original Values";
            this.pagOriginal.UseVisualStyleBackColor = true;
            // 
            // dgvOriginal
            // 
            this.dgvOriginal.AllowUserToAddRows = false;
            this.dgvOriginal.AllowUserToDeleteRows = false;
            this.dgvOriginal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOriginal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOriginal.Location = new System.Drawing.Point(3, 3);
            this.dgvOriginal.Name = "dgvOriginal";
            this.dgvOriginal.ReadOnly = true;
            this.dgvOriginal.Size = new System.Drawing.Size(821, 482);
            this.dgvOriginal.TabIndex = 0;
            // 
            // DataDiffDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 634);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DataDiffDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataDiffDetails";
            this.Shown += new System.EventHandler(this.DataDiffDetailsForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabResults.ResumeLayout(false);
            this.pagDelta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tdgvDelta)).EndInit();
            this.pagOriginal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdDiffsOnly;
        private System.Windows.Forms.Button cmdPrevious;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabResults;
        private System.Windows.Forms.TabPage pagDelta;
        private DataTableDiffGridView tdgvDelta;
        private System.Windows.Forms.TabPage pagOriginal;
        private System.Windows.Forms.DataGridView dgvOriginal;
    }
}