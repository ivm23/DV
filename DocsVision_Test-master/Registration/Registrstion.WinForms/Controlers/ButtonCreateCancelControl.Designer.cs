namespace Registration.WinForms.Controlers
{
    partial class ButtonCreateCancelControl
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
            this.cancelB = new System.Windows.Forms.Button();
            this.createB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelB
            // 
            this.cancelB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelB.Location = new System.Drawing.Point(92, 11);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(75, 23);
            this.cancelB.TabIndex = 7;
            this.cancelB.Text = "Cancel";
            this.cancelB.UseVisualStyleBackColor = true;
            // 
            // createB
            // 
            this.createB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.createB.Location = new System.Drawing.Point(11, 11);
            this.createB.Name = "createB";
            this.createB.Size = new System.Drawing.Size(75, 23);
            this.createB.TabIndex = 6;
            this.createB.Text = "Create";
            this.createB.UseVisualStyleBackColor = true;
            // 
            // ButtonCreateCancelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.createB);
            this.Name = "ButtonCreateCancelControl";
            this.Size = new System.Drawing.Size(182, 41);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelB;
        private System.Windows.Forms.Button createB;
    }
}
