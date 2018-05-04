using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.Windows.Input;

namespace Registration.WPF.ViewModels
{
    class InboxFolderViewModel : Notifier
    {
        private readonly Folder _folder;
        private FolderType _folderType;
        private IEnumerable<FolderType> _folderTypes;
        private readonly IServiceProvider _serviceProvider;
        public InboxFolderViewModel(IServiceProvider provider, IEnumerable<FolderType> folderTypes)
        {

            if (null == folderTypes || null == provider)
                throw new ArgumentNullException();
                      
            _folder = new Folder();
            FoldersTypes = folderTypes;
            _serviceProvider = provider;

            DataContextChange = new ViewModels.Command(arg => DataContextChangeMethod(arg));
        }


        public ICommand DataContextChange { get; set; }
        private void DataContextChangeMethod(object arg)
        {
            ((ApplicationState)_serviceProvider.GetService(typeof(ApplicationState))).SelectedFolderType = (FolderType)arg;
        }

        public string NameFolder
        {
            set
            {
                _folder.Name = value;
                OnPropertyChanged(nameof(NameFolder));
            }
            get
            {
                return _folder.Name;
            }
        }
        public FolderType SelectedType
        {
            set
            {
                _folderType = value;
                _folder.Type = value.Id;
                OnPropertyChanged(nameof(SelectedType));
            }
            get
            {
                return _folderType;
            }
        }

        public IEnumerable<FolderType> FoldersTypes
        {
            set
            {
                _folderTypes = value;
                OnPropertyChanged(nameof(FoldersTypes));
            }
            get
            {
                return _folderTypes;
            }
        }
    }
}
