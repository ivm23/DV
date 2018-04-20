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
        private IServiceProvider _serviceProvider;

        public SearchFolderControl()
        {
            InitializeComponent();
            createFolderControl1.ChangedFolderType += new EventHandler(createFolderControl_ChangedFolderType);
        }

        public event EventHandler ChangedFolderTypePlugin;

        private IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        public FolderProperties FolderProperties
        {
            set
            {
                foreach (KeyValuePair<string, string> name in value.Properties)
                {
                    comboSelectSender.Items.Add(name);
                }
            }
            get
            {
                FolderProperties info = new FolderProperties();
                if (null != comboSelectSender.SelectedItem)
                    info.Properties.Add(comboSelectSender.Text, comboSelectSender.SelectedItem.ToString());
                return info;
            }
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _serviceProvider = serviceProvider;
            createFolderControl1.InitializeFolderTypes(ServiceProvider);
        }

        private void createFolderControl_ChangedFolderType(object sender, EventArgs e)
        {
                ChangedFolderTypePlugin(this, e);
        }

        public FolderType FolderType
        {
            set
            {
                createFolderControl1.FolderType = value;
            }
            get
            {
                return createFolderControl1.FolderType;
            }
        }


    }
}
