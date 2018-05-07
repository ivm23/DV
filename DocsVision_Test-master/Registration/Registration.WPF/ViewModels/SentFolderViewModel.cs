﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.Windows.Input;
using System.Windows.Controls;


namespace Registration.WPF.ViewModels
{
    class SentFolderViewModel : Notifier
    {
        private readonly Folder _folder;
        private FolderType _folderType;
        private IEnumerable<FolderType> _folderTypes;
        private readonly IServiceProvider _serviceProvider;
        private readonly Models.IMakeFolderWindow _parentWindow;

        public SentFolderViewModel(IServiceProvider provider, IEnumerable<FolderType> folderTypes, Models.IMakeFolderWindow parent)
        {

            if (null == folderTypes || null == provider)
                throw new ArgumentNullException();

            _folder = new Folder();
            FoldersTypes = folderTypes;
            _serviceProvider = provider;

            FolderTypeChanged = new ViewModels.Command(arg => FolderTypeChangedMethod(arg));
            _parentWindow = parent;
        }


        public ICommand FolderTypeChanged { get; set; }
        private void FolderTypeChangedMethod(object arg)
        {
            ((ApplicationState)_serviceProvider.GetService(typeof(ApplicationState))).SelectedFolderType = (FolderType)arg;

            var f = ViewModels.ViewPluginCreater.Create(((ApplicationState)_serviceProvider.GetService(typeof(ApplicationState))).SelectedFolderType, ((PluginService)_serviceProvider.GetService(typeof(PluginService))));
            f.OnLoad(_serviceProvider, _parentWindow);

            _parentWindow.ChangeFolderPlugin((Control)(f));
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