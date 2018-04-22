namespace Registration.WinForms.Forms
{
    partial class AddWorkersFromAllWorkersList
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
            this.listAllWorkers = new System.Windows.Forms.ListBox();
            this.listReceivers = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.lblAllWorkers = new System.Windows.Forms.Label();
            this.lblReceivers = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listAllWorkers
            // 
            this.listAllWorkers.FormattingEnabled = true;
            this.listAllWorkers.Location = new System.Drawing.Point(8, 26);
            this.listAllWorkers.Name = "listAllWorkers";
            this.listAllWorkers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listAllWorkers.Size = new System.Drawing.Size(225, 199);
            this.listAllWorkers.TabIndex = 0;
            // 
            // listReceivers
            // 
            this.listReceivers.FormattingEnabled = true;
            this.listReceivers.Location = new System.Drawing.Point(302, 26);
            this.listReceivers.Name = "listReceivers";
            this.listReceivers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listReceivers.Size = new System.Drawing.Size(225, 199);
            this.listReceivers.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAdd.Location = new System.Drawing.Point(239, 46);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(55, 20);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "→";
            this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRemove.Location = new System.Drawing.Point(239, 70);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(55, 20);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "←";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // lblAllWorkers
            // 
            this.lblAllWorkers.AutoSize = true;
            this.lblAllWorkers.Location = new System.Drawing.Point(5, 8);
            this.lblAllWorkers.Name = "lblAllWorkers";
            this.lblAllWorkers.Size = new System.Drawing.Size(58, 13);
            this.lblAllWorkers.TabIndex = 4;
            this.lblAllWorkers.Text = "All workers";
            // 
            // lblReceivers
            // 
            this.lblReceivers.AutoSize = true;
            this.lblReceivers.Location = new System.Drawing.Point(299, 8);
            this.lblReceivers.Name = "lblReceivers";
            this.lblReceivers.Size = new System.Drawing.Size(55, 13);
            this.lblReceivers.TabIndex = 5;
            this.lblReceivers.Text = "Receivers";
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.Location = new System.Drawing.Point(383, 231);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(69, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "&Save";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(458, 231);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(69, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // AddWorkersFromAllWorkersList
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(536, 257);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.lblReceivers);
            this.Controls.Add(this.lblAllWorkers);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listReceivers);
            this.Controls.Add(this.listAllWorkers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddWorkersFromAllWorkersList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Receivers";
            this.Load += new System.EventHandler(this.AddWorkersFromAllWorkersList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listAllWorkers;
        private System.Windows.Forms.ListBox listReceivers;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label lblAllWorkers;
        private System.Windows.Forms.Label lblReceivers;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}