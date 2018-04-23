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
    public partial class SentFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        private IServiceProvider _serviceProvider;
        public event EventHandler ChangedFolderTypePlugin;
        private FolderProperties _info;

        public SentFolderControl()
        {
            InitializeComponent();
            createFolderControl1.ChangedFolderType += new EventHandler(createFolderControl_ChangedFolderType);
        }

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
                _info = value;
            }
            get
            {

                if (null == _info)
                {
                    _info = new global::Registration.Model.FolderProperties();
                }
                _info.Properties.Clear();

                _info.Name = createFolderControl1.NameF;

                return _info;
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
