﻿namespace Registration.WinForms.Controlers
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
            this.listBoxWorkers = new System.Windows.Forms.ListBox();
            this.buttonAllWorkers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtWorkers
            // 
            this.txtWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkers.Location = new System.Drawing.Point(0, 3);
            this.txtWorkers.Name = "txtWorkers";
            this.txtWorkers.Size = new System.Drawing.Size(205, 20);
            this.txtWorkers.TabIndex = 0;
            // 
            // listBoxWorkers
            // 
            this.listBoxWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxWorkers.FormattingEnabled = true;
            this.listBoxWorkers.Location = new System.Drawing.Point(0, 22);
            this.listBoxWorkers.Name = "listBoxWorkers";
            this.listBoxWorkers.Size = new System.Drawing.Size(205, 30);
            this.listBoxWorkers.TabIndex = 3;
            this.listBoxWorkers.Visible = false;
            // 
            // buttonAllWorkers
            // 
            this.buttonAllWorkers.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonAllWorkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAllWorkers.Location = new System.Drawing.Point(185, 1);
            this.buttonAllWorkers.Name = "buttonAllWorkers";
            this.buttonAllWorkers.Size = new System.Drawing.Size(22, 20);
            this.buttonAllWorkers.TabIndex = 4;
            this.buttonAllWorkers.Text = "...";
            this.buttonAllWorkers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAllWorkers.UseVisualStyleBackColor = true;
            this.buttonAllWorkers.Click += new System.EventHandler(this.buttonAllWorkers_Click);
            // 
            // WorkersEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAllWorkers);
            this.Controls.Add(this.listBoxWorkers);
            this.Controls.Add(this.txtWorkers);
            this.Name = "WorkersEditorControl";
            this.Size = new System.Drawing.Size(211, 52);
            this.Load += new System.EventHandler(this.WorkersEditorControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWorkers;
        private System.Windows.Forms.ListBox listBoxWorkers;
        private System.Windows.Forms.Button buttonAllWorkers;
    }
}
