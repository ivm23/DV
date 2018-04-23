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
            Model.LetterView letterView1 = new Model.LetterView();
            this.dateTimePickerResponseRequired = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.fullContentLetterControl1 = new FullContentLetterControl();
            this.SuspendLayout();
            // 
            // dateTimePickerResponseRequired
            // 
            this.dateTimePickerResponseRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerResponseRequired.Location = new System.Drawing.Point(394, 70);
            this.dateTimePickerResponseRequired.Name = "dateTimePickerResponseRequired";
            this.dateTimePickerResponseRequired.Size = new System.Drawing.Size(122, 20);
            this.dateTimePickerResponseRequired.TabIndex = 2;
            // 
            // labelDate
            // 
            this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(391, 54);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(132, 13);
            this.labelDate.TabIndex = 3;
            this.labelDate.Text = "&Date of response required:";
            // 
            // fullContentLetterControl1
            // 
            this.fullContentLetterControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fullContentLetterControl1.AutoScroll = true;
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
            this.fullContentLetterControl1.Location = new System.Drawing.Point(0, 0);
            this.fullContentLetterControl1.MinimumSize = new System.Drawing.Size(544, 500);
            this.fullContentLetterControl1.Name = "fullContentLetterControl1";
            this.fullContentLetterControl1.ReadOnly = true;
            this.fullContentLetterControl1.Size = new System.Drawing.Size(544, 500);
            this.fullContentLetterControl1.TabIndex = 1;
            // 
            // LetterWithResponseTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.dateTimePickerResponseRequired);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.fullContentLetterControl1);
            this.Name = "LetterWithResponseTimeControl";
            this.Size = new System.Drawing.Size(544, 500);
            this.Load += new System.EventHandler(this.LetterWithResponseTimeControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerResponseRequired;
        private System.Windows.Forms.Label labelDate;
        private FullContentLetterControl fullContentLetterControl1;
    }
}
