namespace Registration.WinForms.Forms
{
    partial class SingUpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingUpForm));
            this.singUpB = new System.Windows.Forms.Button();
            this.cancelB = new System.Windows.Forms.Button();
            this.singUpControl1 = new  WinForms.Controlers.SingUpControl();
            this.SuspendLayout();
            // 
            // singUpB
            // 
            this.singUpB.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.singUpB, "singUpB");
            this.singUpB.Name = "singUpB";
            this.singUpB.UseVisualStyleBackColor = true;
            this.singUpB.Click += new System.EventHandler(this.singUpB_Click);
            // 
            // cancelB
            // 
            this.cancelB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelB, "cancelB");
            this.cancelB.Name = "cancelB";
            this.cancelB.UseVisualStyleBackColor = true;
            // 
            // singUpControl1
            // 
            this.singUpControl1.DatabaseNames = ((System.Collections.Generic.IEnumerable<string>)(resources.GetObject("singUpControl1.DatabaseNames")));
            resources.ApplyResources(this.singUpControl1, "singUpControl1");
            this.singUpControl1.Login = "";
            this.singUpControl1.Name = "singUpControl1";
            this.singUpControl1.NameW = "";
            this.singUpControl1.Password = "";
            // 
            // SingUpForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.singUpControl1);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.singUpB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SingUpForm";
            this.Load += new System.EventHandler(this.singUpForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button singUpB;
        private System.Windows.Forms.Button cancelB;
        private Controlers.SingUpControl singUpControl1;
    }
}