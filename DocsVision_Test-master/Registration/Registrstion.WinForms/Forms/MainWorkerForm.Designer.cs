namespace Registration.WinForms.Forms
{
    partial class MainWorkerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWorkerForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.compose = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLetterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foldersTV = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.briefContentLetterDGV = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.senderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFolderTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.changeFolderTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFolderTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.briefContentLetterDGV)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compose,
            this.deleteLetterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(0, 50);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1213, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // compose
            // 
            this.compose.Image = ((System.Drawing.Image)(resources.GetObject("compose.Image")));
            this.compose.Name = "compose";
            this.compose.Size = new System.Drawing.Size(94, 20);
            this.compose.Text = "Type Letter";
            // 
            // deleteLetterToolStripMenuItem
            // 
            this.deleteLetterToolStripMenuItem.Enabled = false;
            this.deleteLetterToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteLetterToolStripMenuItem.Image")));
            this.deleteLetterToolStripMenuItem.Name = "deleteLetterToolStripMenuItem";
            this.deleteLetterToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.deleteLetterToolStripMenuItem.Text = "Delete Letter";
            this.deleteLetterToolStripMenuItem.Click += new System.EventHandler(this.DeleteLetterToolStripMenuItem_Click);
            // 
            // foldersTV
            // 
            this.foldersTV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foldersTV.BackColor = System.Drawing.SystemColors.Window;
            this.foldersTV.Cursor = System.Windows.Forms.Cursors.Default;
            this.foldersTV.HideSelection = false;
            this.foldersTV.Location = new System.Drawing.Point(12, 11);
            this.foldersTV.Name = "foldersTV";
            this.foldersTV.Size = new System.Drawing.Size(154, 509);
            this.foldersTV.TabIndex = 11;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 23);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(1196, 523);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 670;
            this.splitContainer1.Panel2MinSize = 500;
            this.splitContainer1.Size = new System.Drawing.Size(1208, 523);
            this.splitContainer1.SplitterDistance = 676;
            this.splitContainer1.TabIndex = 12;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.foldersTV);
            this.splitContainer2.Panel1MinSize = 160;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.briefContentLetterDGV);
            this.splitContainer2.Panel2MinSize = 500;
            this.splitContainer2.Size = new System.Drawing.Size(673, 520);
            this.splitContainer2.SplitterDistance = 169;
            this.splitContainer2.TabIndex = 9;
            // 
            // briefContentLetterDGV
            // 
            this.briefContentLetterDGV.AllowUserToAddRows = false;
            this.briefContentLetterDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.briefContentLetterDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.briefContentLetterDGV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.briefContentLetterDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.briefContentLetterDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.latterName,
            this.senderName});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.briefContentLetterDGV.DefaultCellStyle = dataGridViewCellStyle1;
            this.briefContentLetterDGV.Location = new System.Drawing.Point(3, 11);
            this.briefContentLetterDGV.MinimumSize = new System.Drawing.Size(50, 50);
            this.briefContentLetterDGV.MultiSelect = false;
            this.briefContentLetterDGV.Name = "briefContentLetterDGV";
            this.briefContentLetterDGV.ReadOnly = true;
            this.briefContentLetterDGV.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.briefContentLetterDGV.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.briefContentLetterDGV.RowTemplate.Height = 40;
            this.briefContentLetterDGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.briefContentLetterDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.briefContentLetterDGV.Size = new System.Drawing.Size(494, 509);
            this.briefContentLetterDGV.TabIndex = 8;
            // 
            // date
            // 
            this.date.FillWeight = 187.8924F;
            this.date.HeaderText = "Date";
            this.date.MinimumWidth = 160;
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // latterName
            // 
            this.latterName.FillWeight = 58.87294F;
            this.latterName.HeaderText = "LetterName";
            this.latterName.MinimumWidth = 160;
            this.latterName.Name = "latterName";
            this.latterName.ReadOnly = true;
            this.latterName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // senderName
            // 
            this.senderName.FillWeight = 115.6712F;
            this.senderName.HeaderText = "Sender name";
            this.senderName.MinimumWidth = 170;
            this.senderName.Name = "senderName";
            this.senderName.ReadOnly = true;
            this.senderName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFolderTSMI,
            this.changeFolderTSMI,
            this.deleteFolderTSMI});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 70);
            // 
            // createFolderTSMI
            // 
            this.createFolderTSMI.Name = "createFolderTSMI";
            this.createFolderTSMI.Size = new System.Drawing.Size(149, 22);
            this.createFolderTSMI.Text = "Create folder";
            this.createFolderTSMI.Click += new System.EventHandler(this.createFolderTSMI_Click);
            // 
            // changeFolderTSMI
            // 
            this.changeFolderTSMI.Name = "changeFolderTSMI";
            this.changeFolderTSMI.Size = new System.Drawing.Size(149, 22);
            this.changeFolderTSMI.Text = "Change folder";
            this.changeFolderTSMI.Click += new System.EventHandler(this.changeFolderTSMI_Click);
            // 
            // deleteFolderTSMI
            // 
            this.deleteFolderTSMI.Name = "deleteFolderTSMI";
            this.deleteFolderTSMI.Size = new System.Drawing.Size(149, 22);
            this.deleteFolderTSMI.Text = "Delete folder";
            this.deleteFolderTSMI.Click += new System.EventHandler(this.deleteFolderTSMI_Click);
            // 
            // MainWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 558);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1229, 596);
            this.Name = "MainWorkerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainLettersForm";
            this.Load += new System.EventHandler(this.MainWorkerForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.briefContentLetterDGV)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteLetterToolStripMenuItem;
        private System.Windows.Forms.TreeView foldersTV;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView briefContentLetterDGV;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn latterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn senderName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createFolderTSMI;
        private System.Windows.Forms.ToolStripMenuItem changeFolderTSMI;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderTSMI;
        private System.Windows.Forms.ToolStripMenuItem compose;
    }
}