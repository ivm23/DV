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
        private ViewModels.SentFolderViewModel _sentFolderViewModel;
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
        private IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        public void OnLoad(IServiceProvider serviceProvider, Models.IMakeFolderWindow parent)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _serviceProvider = serviceProvider;
            InitializeClientRequests();

            
            _sentFolderViewModel = new ViewModels.SentFolderViewModel(ServiceProvider, ClientRequests.GetAllFolderTypes(), parent);
            DataContext = _sentFolderViewModel;
            FolderType = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType;
            createFolderControl.DataContext = _sentFolderViewModel;

            _sentFolderViewModel.SelectedType = FolderType;
        }

        public FolderType FolderType
        {
            set
            {
                _sentFolderViewModel.SelectedType = value;
            }
            get
            {
                return _sentFolderViewModel.SelectedType;
            }
        }

        private FolderProperties _folderProperties = new FolderProperties();
        public FolderProperties FolderProperties
        {
            set
            {
                _folderProperties = value;
            }
            get
            {
                return _folderProperties;
            }
        }

        public string FolderName
        {
            set
            {
                _sentFolderViewModel.NameFolder = value;
            }

            get
            {
                return _sentFolderViewModel.NameFolder;
            }
        }

    }
}
