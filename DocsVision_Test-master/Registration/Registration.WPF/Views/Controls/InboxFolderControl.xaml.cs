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
    /// Interaction logic for InboxFolderControl.xaml
    /// </summary>
    public partial class InboxFolderControl : UserControl, IFolderPropertiesUIPlugin
    {
        private ViewModels.InboxFolderViewModel _inboxFolderViewModel;
        public InboxFolderControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider, Models.IMakeFolderWindow parent)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _inboxFolderViewModel = new ViewModels.InboxFolderViewModel(serviceProvider, ((IClientRequests)serviceProvider.GetService(typeof(IClientRequests))).GetAllFolderTypes(), parent);
            DataContext = _inboxFolderViewModel;
        }

        public FolderType FolderType
        {
            set
            {
                _inboxFolderViewModel.SelectedType = value;
            }
            get
            {
                return _inboxFolderViewModel.SelectedType;
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
                _inboxFolderViewModel.NameFolder = value;
            }

            get
            {
                return _inboxFolderViewModel.NameFolder;
            }
        }
    }
}
