namespace Registration.WinForms.Controlers
{
    partial class InboxFolderControl
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
            this.createFolderControl1 = new CreateFolderControl();
            this.SuspendLayout();
            // 
            // createFolderControl1
            // 
            this.createFolderControl1.Location = new System.Drawing.Point(0, 0);
            this.createFolderControl1.Name = "createFolderControl1";
            this.createFolderControl1.Size = new System.Drawing.Size(230, 63);
            this.createFolderControl1.TabIndex = 0;
            // 
            // InboxFolderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.createFolderControl1);
            this.Name = "InboxFolderControl";
            this.Size = new System.Drawing.Size(232, 64);
            this.ResumeLayout(false);

        }

        #endregion

        private CreateFolderControl createFolderControl1;
    }
}
