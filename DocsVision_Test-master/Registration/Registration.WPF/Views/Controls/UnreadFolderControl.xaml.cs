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
    /// Interaction logic for UnreadFolderControl.xaml
    /// </summary>
    public partial class UnreadFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        private ViewModels.UnreadFolderViewModel _unreadFolderViewModel;

        public UnreadFolderControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider, Models.IMakeFolderWindow parent)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _unreadFolderViewModel = new ViewModels.UnreadFolderViewModel(serviceProvider, ((IClientRequests)serviceProvider.GetService(typeof(IClientRequests))).GetAllFolderTypes(), parent);
            DataContext = _unreadFolderViewModel;
            FolderType = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedFolderType;

            createFolderControl.DataContext = _unreadFolderViewModel;
        }

        public FolderType FolderType
        {
            set
            {
                _unreadFolderViewModel.SelectedType = value;
            }
            get
            {
                return _unreadFolderViewModel.SelectedType;
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
                _unreadFolderViewModel.NameFolder = value;
            }

            get
            {
                return _unreadFolderViewModel.NameFolder;
            }
        }
    }
}
