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
    /// Interaction logic for SearchFolderControl.xaml
    /// </summary>
    public partial class SearchFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        private ViewModels.SearchFolderViewModel _searchFolderViewModel;
        public SearchFolderControl()
        {
            InitializeComponent();
         //   createFolderControl.DataContext = _searchFolderViewModel;
        }

        public void OnLoad(IServiceProvider serviceProvider, Models.IMakeFolderWindow parent)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _searchFolderViewModel = new ViewModels.SearchFolderViewModel(serviceProvider, ((IClientRequests)serviceProvider.GetService(typeof(IClientRequests))).GetAllFolderTypes(), parent);

            DataContext = _searchFolderViewModel;

            FolderType = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedFolderType;
            createFolderControl.DataContext = _searchFolderViewModel;

            _searchFolderViewModel.SelectedType = FolderType;
        }


        public FolderType FolderType
        {
            set
            {
                _searchFolderViewModel.SelectedType = value;
            }
            get
            {
                return _searchFolderViewModel.SelectedType;
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
                _searchFolderViewModel.NameFolder = value;
            }

            get
            {
                return _searchFolderViewModel.NameFolder;
            }
        }
    }
}
