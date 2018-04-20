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
        private IServiceProvider _serviceProvider;

        public event EventHandler ChangedFolderTypePlugin;



        public InboxFolderControl()
        {
            InitializeComponent();
            createFolderControl1.ChangedFolderType += new EventHandler(createFolderControl_ChangedFolderType);
        }

        FolderProperties _info;
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
                if (_info == null)
                    _info = new global::Registration.Model.FolderProperties();


                return _info;
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
