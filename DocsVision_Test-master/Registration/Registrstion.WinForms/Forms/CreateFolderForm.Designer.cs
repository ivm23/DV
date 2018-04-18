namespace Registration.WinForms.Forms
{
    partial class CreateFolderForm
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
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.comboFolderType = new System.Windows.Forms.ComboBox();
            this.FolderTypeL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(60, 12);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(156, 20);
            this.txtFolderName.TabIndex = 8;
            // 
            // comboFolderType
            // 
            this.comboFolderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFolderType.FormattingEnabled = true;
            this.comboFolderType.Location = new System.Drawing.Point(60, 38);
            this.comboFolderType.Name = "comboFolderType";
            this.comboFolderType.Size = new System.Drawing.Size(156, 21);
            this.comboFolderType.TabIndex = 9;
            // 
            // FolderTypeL
            // 
            this.FolderTypeL.AutoSize = true;
            this.FolderTypeL.Location = new System.Drawing.Point(8, 46);
            this.FolderTypeL.Name = "FolderTypeL";
            this.FolderTypeL.Size = new System.Drawing.Size(31, 13);
            this.FolderTypeL.TabIndex = 10;
            this.FolderTypeL.Text = "Type";
            // 
            // CreateFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 76);
            this.Controls.Add(this.FolderTypeL);
            this.Controls.Add(this.comboFolderType);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateFolderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FolderProperties";
            this.Load += new System.EventHandler(this.CreateFolderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.ComboBox comboFolderType;
        private System.Windows.Forms.Label FolderTypeL;
    }
}