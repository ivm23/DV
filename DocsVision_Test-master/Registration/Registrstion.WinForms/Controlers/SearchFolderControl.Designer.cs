namespace Registration.WinForms.Controlers
{
    partial class SearchFolderControl
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
            this.comboSelectSender = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.createFolderControl1 = new Controlers.CreateFolderControl();
            this.SuspendLayout();
            // 
            // comboSelectSender
            // 
            this.comboSelectSender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSelectSender.FormattingEnabled = true;
            this.comboSelectSender.Location = new System.Drawing.Point(174, 89);
            this.comboSelectSender.Name = "comboSelectSender";
            this.comboSelectSender.Size = new System.Drawing.Size(112, 21);
            this.comboSelectSender.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Which messages would you like to show in this folder?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Show in folder if Sender contains";
            // 
            // createFolderControl1
            // 
            this.createFolderControl1.FolderType = null;
            this.createFolderControl1.Location = new System.Drawing.Point(0, 0);
            this.createFolderControl1.Name = "createFolderControl1";
            this.createFolderControl1.NameF = "";
            this.createFolderControl1.Size = new System.Drawing.Size(302, 63);
            this.createFolderControl1.TabIndex = 8;
            // 
            // SearchFolderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.createFolderControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboSelectSender);
            this.Name = "SearchFolderControl";
            this.Size = new System.Drawing.Size(326, 127);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboSelectSender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CreateFolderControl createFolderControl1;
    }
}
