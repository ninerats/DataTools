namespace Craftsmaneer.DataTools.Forms
{
    partial class ReturnValueForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSuccess = new System.Windows.Forms.TextBox();
            this.tabReturnValue = new System.Windows.Forms.TabControl();
            this.pagValue = new System.Windows.Forms.TabPage();
            this.pagError = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValueType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdDump = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDumpPath = new System.Windows.Forms.TextBox();
            this.txtValueAsString = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContext = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtErrorCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtErrorDetails = new System.Windows.Forms.TextBox();
            this.tabReturnValue.SuspendLayout();
            this.pagValue.SuspendLayout();
            this.pagError.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Success: ";
            // 
            // txtSuccess
            // 
            this.txtSuccess.Location = new System.Drawing.Point(73, 21);
            this.txtSuccess.Name = "txtSuccess";
            this.txtSuccess.ReadOnly = true;
            this.txtSuccess.Size = new System.Drawing.Size(84, 20);
            this.txtSuccess.TabIndex = 1;
            // 
            // tabReturnValue
            // 
            this.tabReturnValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabReturnValue.Controls.Add(this.pagValue);
            this.tabReturnValue.Controls.Add(this.pagError);
            this.tabReturnValue.Location = new System.Drawing.Point(16, 69);
            this.tabReturnValue.Name = "tabReturnValue";
            this.tabReturnValue.SelectedIndex = 0;
            this.tabReturnValue.Size = new System.Drawing.Size(712, 489);
            this.tabReturnValue.TabIndex = 2;
            // 
            // pagValue
            // 
            this.pagValue.Controls.Add(this.txtValueAsString);
            this.pagValue.Controls.Add(this.label3);
            this.pagValue.Controls.Add(this.txtValueType);
            this.pagValue.Controls.Add(this.label2);
            this.pagValue.Location = new System.Drawing.Point(4, 22);
            this.pagValue.Name = "pagValue";
            this.pagValue.Padding = new System.Windows.Forms.Padding(3);
            this.pagValue.Size = new System.Drawing.Size(704, 463);
            this.pagValue.TabIndex = 0;
            this.pagValue.Text = "Value";
            this.pagValue.UseVisualStyleBackColor = true;
            // 
            // pagError
            // 
            this.pagError.Controls.Add(this.txtErrorDetails);
            this.pagError.Controls.Add(this.label7);
            this.pagError.Controls.Add(this.txtErrorCode);
            this.pagError.Controls.Add(this.label6);
            this.pagError.Controls.Add(this.txtContext);
            this.pagError.Controls.Add(this.label5);
            this.pagError.Location = new System.Drawing.Point(4, 22);
            this.pagError.Name = "pagError";
            this.pagError.Padding = new System.Windows.Forms.Padding(3);
            this.pagError.Size = new System.Drawing.Size(704, 463);
            this.pagError.TabIndex = 1;
            this.pagError.Text = "Error Information";
            this.pagError.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Value Type:";
            // 
            // txtValueType
            // 
            this.txtValueType.Location = new System.Drawing.Point(87, 18);
            this.txtValueType.Name = "txtValueType";
            this.txtValueType.ReadOnly = true;
            this.txtValueType.Size = new System.Drawing.Size(275, 20);
            this.txtValueType.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Text Representation";
            // 
            // cmdDump
            // 
            this.cmdDump.Location = new System.Drawing.Point(189, 19);
            this.cmdDump.Name = "cmdDump";
            this.cmdDump.Size = new System.Drawing.Size(75, 23);
            this.cmdDump.TabIndex = 3;
            this.cmdDump.Text = "Dump";
            this.cmdDump.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Path:";
            // 
            // txtDumpPath
            // 
            this.txtDumpPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDumpPath.Location = new System.Drawing.Point(305, 19);
            this.txtDumpPath.Name = "txtDumpPath";
            this.txtDumpPath.Size = new System.Drawing.Size(423, 20);
            this.txtDumpPath.TabIndex = 5;
            // 
            // txtValueAsString
            // 
            this.txtValueAsString.AcceptsReturn = true;
            this.txtValueAsString.AcceptsTab = true;
            this.txtValueAsString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValueAsString.Location = new System.Drawing.Point(21, 81);
            this.txtValueAsString.Multiline = true;
            this.txtValueAsString.Name = "txtValueAsString";
            this.txtValueAsString.ReadOnly = true;
            this.txtValueAsString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtValueAsString.Size = new System.Drawing.Size(660, 358);
            this.txtValueAsString.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Context:";
            // 
            // txtContext
            // 
            this.txtContext.Location = new System.Drawing.Point(65, 17);
            this.txtContext.Name = "txtContext";
            this.txtContext.ReadOnly = true;
            this.txtContext.Size = new System.Drawing.Size(616, 20);
            this.txtContext.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Code:";
            // 
            // txtErrorCode
            // 
            this.txtErrorCode.Location = new System.Drawing.Point(65, 52);
            this.txtErrorCode.Name = "txtErrorCode";
            this.txtErrorCode.ReadOnly = true;
            this.txtErrorCode.Size = new System.Drawing.Size(193, 20);
            this.txtErrorCode.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Details:";
            // 
            // txtErrorDetails
            // 
            this.txtErrorDetails.AcceptsReturn = true;
            this.txtErrorDetails.AcceptsTab = true;
            this.txtErrorDetails.Location = new System.Drawing.Point(16, 105);
            this.txtErrorDetails.Multiline = true;
            this.txtErrorDetails.Name = "txtErrorDetails";
            this.txtErrorDetails.ReadOnly = true;
            this.txtErrorDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrorDetails.Size = new System.Drawing.Size(665, 337);
            this.txtErrorDetails.TabIndex = 5;
            // 
            // ReturnValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 570);
            this.Controls.Add(this.txtDumpPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdDump);
            this.Controls.Add(this.tabReturnValue);
            this.Controls.Add(this.txtSuccess);
            this.Controls.Add(this.label1);
            this.Name = "ReturnValueForm";
            this.Text = "ReturnValueForm";
            this.tabReturnValue.ResumeLayout(false);
            this.pagValue.ResumeLayout(false);
            this.pagValue.PerformLayout();
            this.pagError.ResumeLayout(false);
            this.pagError.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSuccess;
        private System.Windows.Forms.TabControl tabReturnValue;
        private System.Windows.Forms.TabPage pagValue;
        private System.Windows.Forms.TextBox txtValueAsString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValueType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage pagError;
        private System.Windows.Forms.TextBox txtErrorDetails;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtErrorCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtContext;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdDump;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDumpPath;
    }
}