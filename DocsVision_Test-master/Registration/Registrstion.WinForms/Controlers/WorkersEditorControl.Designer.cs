namespace Registration.WinForms.Controlers
{
    partial class WorkersEditorControl
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
            this.txtWorkers = new System.Windows.Forms.TextBox();
            this.comboWorkers = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtWorkers
            // 
            this.txtWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkers.Location = new System.Drawing.Point(2, 2);
            this.txtWorkers.Name = "txtWorkers";
            this.txtWorkers.Size = new System.Drawing.Size(205, 20);
            this.txtWorkers.TabIndex = 0;
            // 
            // comboWorkers
            // 
            this.comboWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboWorkers.FormattingEnabled = true;
            this.comboWorkers.Location = new System.Drawing.Point(3, 3);
            this.comboWorkers.Name = "comboWorkers";
            this.comboWorkers.Size = new System.Drawing.Size(204, 21);
            this.comboWorkers.TabIndex = 1;
            // 
            // WorkersEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboWorkers);
            this.Controls.Add(this.txtWorkers);
            this.Name = "WorkersEditorControl";
            this.Size = new System.Drawing.Size(211, 28);
            this.Load += new System.EventHandler(this.WorkersEditorControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWorkers;
        private System.Windows.Forms.ComboBox comboWorkers;
    }
}
