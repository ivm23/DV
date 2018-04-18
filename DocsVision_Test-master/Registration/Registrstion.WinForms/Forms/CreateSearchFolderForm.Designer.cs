namespace Registration.WinForms.Forms
{
    partial class CreateSearchFolderForm
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
            this.QuestionL = new System.Windows.Forms.Label();
            this.ifL = new System.Windows.Forms.Label();
            this.senderContentL = new System.Windows.Forms.Label();
            this.senderContent = new System.Windows.Forms.TextBox();
            this.saveB = new System.Windows.Forms.Button();
            this.cancelB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // QuestionL
            // 
            this.QuestionL.AutoSize = true;
            this.QuestionL.Location = new System.Drawing.Point(2, 9);
            this.QuestionL.Name = "QuestionL";
            this.QuestionL.Size = new System.Drawing.Size(265, 13);
            this.QuestionL.TabIndex = 0;
            this.QuestionL.Text = "Which messages would you like to move to this folder?";
            // 
            // ifL
            // 
            this.ifL.AutoSize = true;
            this.ifL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ifL.Location = new System.Drawing.Point(2, 39);
            this.ifL.Name = "ifL";
            this.ifL.Size = new System.Drawing.Size(100, 13);
            this.ifL.TabIndex = 1;
            this.ifL.Text = "Move to folder if";
            // 
            // senderContentL
            // 
            this.senderContentL.AutoSize = true;
            this.senderContentL.Location = new System.Drawing.Point(2, 69);
            this.senderContentL.Name = "senderContentL";
            this.senderContentL.Size = new System.Drawing.Size(92, 13);
            this.senderContentL.TabIndex = 2;
            this.senderContentL.Text = "“Sender” contains";
            // 
            // senderContent
            // 
            this.senderContent.Location = new System.Drawing.Point(107, 65);
            this.senderContent.Name = "senderContent";
            this.senderContent.Size = new System.Drawing.Size(159, 20);
            this.senderContent.TabIndex = 3;
            // 
            // saveB
            // 
            this.saveB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveB.Location = new System.Drawing.Point(107, 95);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(75, 23);
            this.saveB.TabIndex = 4;
            this.saveB.Text = "Save";
            this.saveB.UseVisualStyleBackColor = true;
            // 
            // cancelB
            // 
            this.cancelB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelB.Location = new System.Drawing.Point(191, 95);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(75, 23);
            this.cancelB.TabIndex = 5;
            this.cancelB.Text = "Cancel";
            this.cancelB.UseVisualStyleBackColor = true;
            // 
            // CreateSearchFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 126);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.saveB);
            this.Controls.Add(this.senderContent);
            this.Controls.Add(this.senderContentL);
            this.Controls.Add(this.ifL);
            this.Controls.Add(this.QuestionL);
            this.Name = "CreateSearchFolderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateSearchFolderForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label QuestionL;
        private System.Windows.Forms.Label ifL;
        private System.Windows.Forms.Label senderContentL;
        private System.Windows.Forms.TextBox senderContent;
        private System.Windows.Forms.Button saveB;
        private System.Windows.Forms.Button cancelB;
    }
}