using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.Windows.Input;
using System.Windows.Controls;
using Registration.ClientInterface;

namespace Registration.WPF.ViewModels
{
    class SearchFolderViewModel : Notifier
    {
        private readonly Folder _folder;
        private FolderType _folderType;
        private IEnumerable<FolderType> _folderTypes;
        private readonly IServiceProvider _serviceProvider;
        private readonly Models.IMakeFolderWindow _parentWindow;

        public SearchFolderViewModel(IServiceProvider provider, IEnumerable<FolderType> folderTypes, Models.IMakeFolderWindow parent)
        {
            if (null == folderTypes || null == provider)
                throw new ArgumentNullException();

            _folder = new Folder();
            FoldersTypes = folderTypes;
            _serviceProvider = provider;

            FolderTypeChanged = new ViewModels.Command(arg => FolderTypeChangedMethod(arg));
            _parentWindow = parent;

            WorkersNames = ((IClientRequests)provider.GetService(typeof(IClientRequests))).GetAllWorkers();

            SelectedType = ((ApplicationState)provider.GetService(typeof(ApplicationState))).SelectedFolderType;
            SelectedFolderName = SelectedType.Name;
        }

        private Models.IMakeFolderWindow ParentWindow
        {
            get { return _parentWindow; }
        }

        private IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        public ICommand FolderTypeChanged { get; set; }

        private void FolderTypeChangedMethod(object arg)
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType = (FolderType)arg;

            IFolderPropertiesUIPlugin folderPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            folderPlugin.OnLoad(ServiceProvider, ParentWindow);

            ParentWindow.ChangeFolderPlugin((Control)(folderPlugin));
            folderPlugin.FolderType = SelectedType;
            folderPlugin.FolderName = NameFolder;
        }

        public string NameFolder
        {
            set
            {
                _folder.Name = value;
                OnPropertyChanged(nameof(NameFolder));
            }
            get { return _folder.Name; }
        }
        public FolderType SelectedType
        {
            set
            {
                _folderType = value;
                _folder.Type = value.Id;
                OnPropertyChanged(nameof(SelectedType));
            }
            get { return _folderType; }
        }

        private string _selectedFolderName;
        public string SelectedFolderName
        {
            set
            {
                _selectedFolderName = value;
                OnPropertyChanged(nameof(SelectedFolderName));
            }
            get { return _selectedFolderName; }
        }

        public IEnumerable<FolderType> FoldersTypes
        {
            set
            {
                _folderTypes = value;
                OnPropertyChanged(nameof(FoldersTypes));
            }
            get { return _folderTypes; }
        }

        private FolderProperties _folderProperties = new FolderProperties();
        public FolderProperties FolderProperties
        {
            set { _folderProperties = value; }
            get
            {
                _folderProperties.Name = NameFolder;
                _folderProperties.ExtendedProperty = SelectedWorkerName;
                return _folderProperties;
            }
        }

        private string _selectedWorkerName;
        public string SelectedWorkerName
        {
            set
            {
                _selectedWorkerName = value;
                OnPropertyChanged(nameof(SelectedWorkerName));
            }
            get { return _selectedWorkerName; }
        }

        private IEnumerable<string> _workersNames;
        public IEnumerable<string> WorkersNames
        {
            set
            {
                _workersNames = value;
                OnPropertyChanged(nameof(WorkersNames));
            }
            get { return _workersNames; }
        }
    }

}
