using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registrstion.WinForms;
using Registration.Model;
using Registration.Logger;

namespace Registration.Plugins
{
    public partial class SearchFolderForm : Form, IClientUIFolderPropertiesPlugin
    {
        private FolderProperties _info;
        private bool _event = false;
        public SearchFolderForm()
        {
            InitializeComponent();
        }
        

        private void InitializeFolderTupeInfo()
        {
            _info = new FolderProperties();
        }
        private FolderProperties FolderTypeInfo
        {
            get
            {
                return _info;
            }
        }
        private void InitializeSearchFolderForm()
        {
            InitializeFolderTupeInfo();
        }

        public void LoadForm()
        {
            try
            {
                InitializeSearchFolderForm();
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }

            if (ShowDialog() == DialogResult.OK)


        }

        public FolderProperties Info
        {
            set
            {
                foreach (string name in value.Info)
                {
                    selectSenderCB.Items.Add(name);
                }
            }
            get
            {
                return FolderTypeInfo;
            }
        }

        public bool GetEventStatus()
        {
            return _event;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderTypeInfo.Info.Add(selectSenderCB.SelectedItem.ToString());
            _event = true;
            Close();
        }
    }
}
