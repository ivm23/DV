using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.Windows.Input;
using System.Windows.Controls;

namespace Registration.WPF.ViewModels
{
    class InboxFolderViewModel : Notifier
    {
        private readonly Folder _folder;
        private FolderType _folderType;
        private IEnumerable<FolderType> _folderTypes;
        private readonly IServiceProvider _serviceProvider;
        private readonly Models.IMakeFolderWindow _parentWindow;

        public InboxFolderViewModel(IServiceProvider provider, IEnumerable<FolderType> folderTypes, Models.IMakeFolderWindow parent)
        {
            if (null == folderTypes || null == provider)
                throw new ArgumentNullException();
                      
            _folder = new Folder();
            FoldersTypes = folderTypes;
            _serviceProvider = provider;

            FolderTypeChanged = new ViewModels.Command(arg => FolderTypeChangedMethod(arg));
            _parentWindow = parent;

            SelectedType = ((ApplicationState)provider.GetService(typeof(ApplicationState))).SelectedFolderType;
            SelectedFolderName = SelectedType.Name;
        }

        private string _selectedFolderName;
        public string SelectedFolderName
        {
            set
            {
                _selectedFolderName = value;
                OnPropertyChanged(nameof(SelectedFolderName));
            }
            get { return _selectedFolderName;  }
        }

        public ICommand FolderTypeChanged { get; set; }
        private void FolderTypeChangedMethod(object arg)
        {
            ((ApplicationState)_serviceProvider.GetService(typeof(ApplicationState))).SelectedFolderType = (FolderType)arg;
            
            IFolderPropertiesUIPlugin folderPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)_serviceProvider.GetService(typeof(ApplicationState))).SelectedFolderType, ((PluginService)_serviceProvider.GetService(typeof(PluginService))));
            folderPlugin.OnLoad(_serviceProvider, _parentWindow);
            folderPlugin.FolderType = SelectedType;
            folderPlugin.FolderName = NameFolder;

            _parentWindow.ChangeFolderPlugin((Control)(folderPlugin));
            ((ApplicationState)_serviceProvider.GetService(typeof(ApplicationState))).CurrentFolderPropertiesPlugin = folderPlugin;
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
            get  { return _folderType; }
        }

        public IEnumerable<FolderType> FoldersTypes
        {
            set
            {
                _folderTypes = value;
                OnPropertyChanged(nameof(FoldersTypes));
            }
            get {  return _folderTypes;  }
        }
    }
}
