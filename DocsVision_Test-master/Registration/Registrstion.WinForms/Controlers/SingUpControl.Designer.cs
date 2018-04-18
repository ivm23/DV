namespace Registration.WinForms.Controlers
{
    partial class SingUpControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingUpControl));
            this.loginWorkerTB = new System.Windows.Forms.TextBox();
            this.passwordWorkerTB = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.databaseNamesCB = new System.Windows.Forms.ComboBox();
            this.labelDataBase = new System.Windows.Forms.Label();
            this.nameWorkerTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // loginWorkerTB
            // 
            resources.ApplyResources(this.loginWorkerTB, "loginWorkerTB");
            this.loginWorkerTB.Name = "loginWorkerTB";
            // 
            // passwordWorkerTB
            // 
            resources.ApplyResources(this.passwordWorkerTB, "passwordWorkerTB");
            this.passwordWorkerTB.Name = "passwordWorkerTB";
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // labelLogin
            // 
            resources.ApplyResources(this.labelLogin, "labelLogin");
            this.labelLogin.Name = "labelLogin";
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // databaseNamesCB
            // 
            this.databaseNamesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseNamesCB.FormattingEnabled = true;
            resources.ApplyResources(this.databaseNamesCB, "databaseNamesCB");
            this.databaseNamesCB.Name = "databaseNamesCB";
            // 
            // labelDataBase
            // 
            resources.ApplyResources(this.labelDataBase, "labelDataBase");
            this.labelDataBase.Name = "labelDataBase";
            // 
            // nameWorkerTB
            // 
            resources.ApplyResources(this.nameWorkerTB, "nameWorkerTB");
            this.nameWorkerTB.Name = "nameWorkerTB";
            // 
            // SingUpControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameWorkerTB);
            this.Controls.Add(this.databaseNamesCB);
            this.Controls.Add(this.labelDataBase);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.passwordWorkerTB);
            this.Controls.Add(this.loginWorkerTB);
            this.Name = "SingUpControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox loginWorkerTB;
        private System.Windows.Forms.TextBox passwordWorkerTB;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.ComboBox databaseNamesCB;
        private System.Windows.Forms.Label labelDataBase;
        private System.Windows.Forms.TextBox nameWorkerTB;
    }
}
