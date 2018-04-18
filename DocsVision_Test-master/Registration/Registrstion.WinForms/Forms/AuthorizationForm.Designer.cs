namespace Registration.WinForms
{
    partial class Registration
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration));
            this.singIn = new System.Windows.Forms.Button();
            this.singUp = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.labelDataBase = new System.Windows.Forms.Label();
            this.databaseNamesCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // singIn
            // 
            resources.ApplyResources(this.singIn, "singIn");
            this.singIn.Name = "singIn";
            this.singIn.UseVisualStyleBackColor = true;
            this.singIn.Click += new System.EventHandler(this.singIn_Click);
            // 
            // singUp
            // 
            resources.ApplyResources(this.singUp, "singUp");
            this.singUp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.singUp.Name = "singUp";
            this.singUp.UseVisualStyleBackColor = true;
            this.singUp.Click += new System.EventHandler(this.singUp_Click);
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
            // loginTB
            // 
            resources.ApplyResources(this.loginTB, "loginTB");
            this.loginTB.Name = "loginTB";
            // 
            // passwordTB
            // 
            resources.ApplyResources(this.passwordTB, "passwordTB");
            this.passwordTB.Name = "passwordTB";
            // 
            // labelDataBase
            // 
            resources.ApplyResources(this.labelDataBase, "labelDataBase");
            this.labelDataBase.Name = "labelDataBase";
            // 
            // databaseNamesCB
            // 
            resources.ApplyResources(this.databaseNamesCB, "databaseNamesCB");
            this.databaseNamesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseNamesCB.FormattingEnabled = true;
            this.databaseNamesCB.Name = "databaseNamesCB";
            // 
            // Registration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.databaseNamesCB);
            this.Controls.Add(this.labelDataBase);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.loginTB);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.singUp);
            this.Controls.Add(this.singIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Registration";
            this.Load += new System.EventHandler(this.Registration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button singIn;
        private System.Windows.Forms.Button singUp;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Label labelDataBase;
        private System.Windows.Forms.ComboBox databaseNamesCB;
    }
}

