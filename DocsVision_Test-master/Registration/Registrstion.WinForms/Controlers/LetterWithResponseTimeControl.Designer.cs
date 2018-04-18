namespace Registration.WinForms.Controlers
{
    partial class LetterWithResponseTimeControl
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
            this.labelDate = new System.Windows.Forms.Label();
            this.dateTimePickerResponseRequired = new System.Windows.Forms.DateTimePicker();
            this.fullContentLetterControl1 = new Controlers.FullContentLetterControl();
            this.SuspendLayout();
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(383, 50);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(132, 13);
            this.labelDate.TabIndex = 0;
            this.labelDate.Text = "&Date of response required:";
            // 
            // dateTimePickerResponseRequired
            // 
            this.dateTimePickerResponseRequired.Location = new System.Drawing.Point(386, 66);
            this.dateTimePickerResponseRequired.Name = "dateTimePickerResponseRequired";
            this.dateTimePickerResponseRequired.Size = new System.Drawing.Size(122, 20);
            this.dateTimePickerResponseRequired.TabIndex = 1;
            // 
            // fullContentLetterControl1
            // 
            this.fullContentLetterControl1.Location = new System.Drawing.Point(-8, -4);
            this.fullContentLetterControl1.Name = "fullContentLetterControl1";
            this.fullContentLetterControl1.ReadOnly = true;
            this.fullContentLetterControl1.Size = new System.Drawing.Size(544, 457);
            this.fullContentLetterControl1.TabIndex = 2;
            // 
            // LetterWithResponseTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimePickerResponseRequired);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.fullContentLetterControl1);
            this.Name = "LetterWithResponseTimeControl";
            this.Size = new System.Drawing.Size(538, 481);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerResponseRequired;
        private FullContentLetterControl fullContentLetterControl1;
    }
}
