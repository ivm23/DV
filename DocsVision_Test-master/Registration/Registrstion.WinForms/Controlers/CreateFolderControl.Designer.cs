namespace Registration.WinForms.Controlers
{
    partial class CreateFolderControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FolderTypeL = new System.Windows.Forms.Label();
            this.comboFolderType = new System.Windows.Forms.ComboBox();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FolderTypeL
            // 
            this.FolderTypeL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderTypeL.AutoSize = true;
            this.FolderTypeL.Location = new System.Drawing.Point(7, 37);
            this.FolderTypeL.Name = "FolderTypeL";
            this.FolderTypeL.Size = new System.Drawing.Size(31, 13);
            this.FolderTypeL.TabIndex = 14;
            this.FolderTypeL.Text = "&Type";
            // 
            // comboFolderType
            // 
            this.comboFolderType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboFolderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFolderType.FormattingEnabled = true;
            this.comboFolderType.Location = new System.Drawing.Point(59, 33);
            this.comboFolderType.Name = "comboFolderType";
            this.comboFolderType.Size = new System.Drawing.Size(156, 21);
            this.comboFolderType.TabIndex = 13;
            // 
            // txtFolderName
            // 
            this.txtFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderName.Location = new System.Drawing.Point(59, 7);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(156, 20);
            this.txtFolderName.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "&Name";
            // 
            // CreateFolderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FolderTypeL);
            this.Controls.Add(this.comboFolderType);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.label1);
            this.Name = "CreateFolderControl";
            this.Size = new System.Drawing.Size(230, 63);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FolderTypeL;
        private System.Windows.Forms.ComboBox comboFolderType;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Label label1;
    }
}
