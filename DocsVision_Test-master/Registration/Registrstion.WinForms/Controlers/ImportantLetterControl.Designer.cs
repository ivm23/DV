namespace Registration.WinForms.Controlers
{
    partial class ImportantLetterControl
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
            Model.LetterProperties letterProperties1 = new Model.LetterProperties();
            Model.LetterView letterView1 = new Model.LetterView();
            this.labelImportanceDegree = new System.Windows.Forms.Label();
            this.importanceDegreeEditorControl1 = new ImportanceDegreeEditorControl();
            this.fullContentLetterControl1 = new FullContentLetterControl();
            this.SuspendLayout();
            // 
            // labelImportanceDegree
            // 
            this.labelImportanceDegree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelImportanceDegree.AutoSize = true;
            this.labelImportanceDegree.Location = new System.Drawing.Point(375, 58);
            this.labelImportanceDegree.Name = "labelImportanceDegree";
            this.labelImportanceDegree.Size = new System.Drawing.Size(109, 13);
            this.labelImportanceDegree.TabIndex = 23;
            this.labelImportanceDegree.Text = "Degree of importance";
            // 
            // importanceDegreeEditorControl1
            // 
            this.importanceDegreeEditorControl1.Location = new System.Drawing.Point(376, 71);
            this.importanceDegreeEditorControl1.Name = "importanceDegreeEditorControl1";
            this.importanceDegreeEditorControl1.SelectedImportanceDegree = Model.ImportanceDegree.Low;
            this.importanceDegreeEditorControl1.Size = new System.Drawing.Size(125, 30);
            this.importanceDegreeEditorControl1.TabIndex = 38;
            this.importanceDegreeEditorControl1.Load += new System.EventHandler(this.importanceDegreeEditorControl1_Load);
            // 
            // fullContentLetterControl1
            // 
            letterProperties1.ExtendedProperty = "";
            this.fullContentLetterControl1.LetterExtendedProperties = letterProperties1;
            letterView1.Date = new System.DateTime(((long)(0)));
            letterView1.ExtendedData = null;
            letterView1.IdFolder = new System.Guid("00000000-0000-0000-0000-000000000000");
            letterView1.IdSender = new System.Guid("00000000-0000-0000-0000-000000000000");
            letterView1.IsRead = false;
            letterView1.Name = "";
            letterView1.SenderName = "";
            letterView1.Text = "";
            letterView1.Type = 0;
            this.fullContentLetterControl1.LetterView = letterView1;
            this.fullContentLetterControl1.Location = new System.Drawing.Point(-15, 3);
            this.fullContentLetterControl1.Name = "fullContentLetterControl1";
            this.fullContentLetterControl1.ReadOnly = true;
            this.fullContentLetterControl1.Size = new System.Drawing.Size(544, 457);
            this.fullContentLetterControl1.StandartLetter = letterView1;
            this.fullContentLetterControl1.TabIndex = 39;
            this.fullContentLetterControl1.Load += new System.EventHandler(this.fullContentLetterControl1_Load);
            // 
            // ImportantLetterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.importanceDegreeEditorControl1);
            this.Controls.Add(this.labelImportanceDegree);
            this.Controls.Add(this.fullContentLetterControl1);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "ImportantLetterControl";
            this.Size = new System.Drawing.Size(513, 516);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox nameReceiversCB;
        private System.Windows.Forms.Label labelImportanceDegree;
        private ImportanceDegreeEditorControl importanceDegreeEditorControl1;
        private FullContentLetterControl fullContentLetterControl1;
    }
}
