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
    public partial class UnreadFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        private IServiceProvider _serviceProvider;
        public event EventHandler ChangedFolderTypePlugin;

        public UnreadFolderControl()
        {
            InitializeComponent();
            createFolderControl1.ChangedFolderType += new EventHandler(createFolderControl_ChangedFolderType);
        }

        FolderProperties info;

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
                info = value;
            }
            get
            {
                if (null == info)
                {
                    info = new global::Registration.Model.FolderProperties();
                }
                info.Properties.Clear();

                info.Name = createFolderControl1.NameF;
                return info;
            }
        }

        public void OnLoad(IServiceProvider serviceProvider) {
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
