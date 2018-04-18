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

namespace NotSearchFolder
{
    public partial class NonSearchFolder : Form, IClientUIFolderPropertiesPlugin
    {
        private bool _event = true;
        private FolderProperties _info = new FolderProperties();
        public NonSearchFolder()
        {
            InitializeComponent();
        }

        public void LoadForm()
        {
        }

        public FolderProperties Info
        {
            set
            {
            }
            get
            {
                return _info;
            }
        }

        public bool GetEventStatus()
        {
            return _event;
        }
    }
}
