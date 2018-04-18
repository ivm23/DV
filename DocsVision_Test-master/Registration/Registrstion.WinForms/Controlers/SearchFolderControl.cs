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
    public partial class SearchFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        public SearchFolderControl()
        {
            InitializeComponent();
        }

        public FolderProperties Info
        {
            set {
                foreach (KeyValuePair<string, string> name in value.Properties)
                {
                    comboSelectSender.Items.Add(name);
                }
            }
            get
            {
                global::Registration.Model.FolderProperties info = new global::Registration.Model.FolderProperties();
                info.Properties.Add(comboSelectSender.Text, comboSelectSender.SelectedItem.ToString());
                return info;
            }
        }

        public void OnLoad() { }
    }
}
