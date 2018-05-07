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
            createFolderControl.DataContext = _sentFolderViewModel;
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

        public void OnLoad(IServiceProvider serviceProvider, Models.IMakeFolderWindow parent)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _sentFolderViewModel = new ViewModels.SentFolderViewModel(serviceProvider, ((IClientRequests)serviceProvider.GetService(typeof(IClientRequests))).GetAllFolderTypes(), parent);
            DataContext = _sentFolderViewModel;
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

        public FolderProperties FolderProperties
        {
            set
            {
            }
            get
            {
                return null;
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
