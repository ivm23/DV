namespace Registration.WinForms.Controlers
{
    partial class ImportanceDegreeEditorControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboImportanceDegree = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboImportanceDegree
            // 
            this.comboImportanceDegree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboImportanceDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboImportanceDegree.FormattingEnabled = true;
            this.comboImportanceDegree.Location = new System.Drawing.Point(3, 3);
            this.comboImportanceDegree.Name = "comboImportanceDegree";
            this.comboImportanceDegree.Size = new System.Drawing.Size(121, 21);
            this.comboImportanceDegree.TabIndex = 0;
            // 
            // ImportanceDegreeEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboImportanceDegree);
            this.Name = "ImportanceDegreeEditorControl";
            this.Size = new System.Drawing.Size(128, 30);
            this.Load += new System.EventHandler(this.ImportanceDegreeEditorControl_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboImportanceDegree;
    }
}
