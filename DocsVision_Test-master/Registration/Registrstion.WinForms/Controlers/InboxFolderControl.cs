using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registration.Model;

namespace Registration.WinForms.Controlers
{
    public partial class InboxFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        
        public InboxFolderControl()
        {
            InitializeComponent();
        }

        FolderProperties _info;


        public FolderProperties Info
        {
            set
            {
                _info = value;
            }
            get
            {
                if (_info == null)
                    _info = new global::Registration.Model.FolderProperties();

                return _info;
            }
        }

        public void OnLoad() {}
    }
}
