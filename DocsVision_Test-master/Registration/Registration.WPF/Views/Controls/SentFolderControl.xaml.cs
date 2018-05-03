using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Registration.Model;
using Registration.ClientInterface;

namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for SentFolderControl.xaml
    /// </summary>
    public partial class SentFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        public SentFolderControl()
        {
            InitializeComponent();
        }

        private IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        public IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)_serviceProvider.GetService(typeof(IClientRequests));
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _serviceProvider = serviceProvider;
            InitializeClientRequests();
            InitializeFolderTypes();
            CurrentFolerPropertiesPlugin = this;
        }
        
        private string _folderName;
        public string FolderName {
        set
            {
                _folderName = value;
            }
            get
            {
                return _folderName;
            }
        }


        private void InitializeFolderTypes()
        {
            FoldersTypes = ClientRequests.GetAllFolderTypes();
        }

        public IEnumerable<FolderType> FoldersTypes { get; set; }

        public FolderType FolderType
        {
            set
            {
                // createFolderControl1.FolderType = value;
            }
            get
            {
                return null;// createFolderControl1.FolderType;
            }
        }

        private void createFolderControl_ChangedFolderType(object sender, EventArgs e)
        {
            // ChangedFolderTypePlugin(this, e);
        }

        public FolderProperties FolderProperties
        {
            set
            {
                //   _info = value;
            }
            get
            {
                //    _info.Properties.Clear();
                //    if (_info == null)
                //        _info = new global::Registration.Model.FolderProperties();
                //      _info.Name = createFolderControl1.NameF;

                return null;//_info;
            }
        }
        public IFolderPropertiesUIPlugin CurrentFolerPropertiesPlugin { get; set; }

        
    }
}
