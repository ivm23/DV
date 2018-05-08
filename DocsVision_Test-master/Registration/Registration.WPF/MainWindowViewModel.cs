﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Registration.ClientInterface;
using Registration.WPF.ViewModels;
using System.Windows.Input;
using System.Windows;

namespace Registration.WPF
{
    class MainWindowViewModel : Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private List<Models.Node> _dirItems;
        private LetterView _selectedLetter;
        private Models.Node _selectedNode;
        private IList<LetterView> _letters;

        public MainWindowViewModel() { }

        public MainWindowViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            InitializeClientRequests();

            SelectedItemChanged = new ViewModels.Command(arg => SelectedItemChangedMethod(arg));
            OpenLetterViewWindow = new ViewModels.Command(args => OpenLetterViewWindowMethod(args));
            DeleteLetterClick = new ViewModels.Command(args => DeleteLetterClickMethod());
            MakeLetter = new ViewModels.Command(arg => MakeLetterMethod(arg));
            CreateFolder = new ViewModels.Command(arg => CreateFolderMethod());
            ShowBriefLetter = new ViewModels.Command(arg => ShowBriefLetterMethod(arg));
            EditFolder = new ViewModels.Command(arg => EditFolderMethod(arg));
            DeleteFolder = new ViewModels.Command(arg => DeleteFolderMethod(arg));
        }

        public ICommand SelectedItemChanged { get; set; }
        public ICommand OpenLetterViewWindow { set; get; }
        public ICommand DeleteLetterClick { set; get; }
        public ICommand MakeLetter { set; get; }
        public ICommand ShowBriefLetter { set; get; }
        public ICommand CreateFolder { set; get; }
        public ICommand EditFolder { set; get; }
        public ICommand DeleteFolder { set; get; }

        private void MakeLetterMethod(object arg)
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType = (LetterType)(arg);
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView = new LetterView();
            var window = new Views.MakeLetterWindow(ServiceProvider);
            window.ShowDialog();

            InitializeDataGrid(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id);
        }

        private void EditFolderMethod(object arg)
        {
            var window = new Views.RenameFolderWindow(ServiceProvider);
            window.ShowDialog();
        }

        private void DeleteFolderMethod(object arg)
        {
            ClientRequests.DeleteFolder(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id);
        }
        private void CreateFolderMethod()
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType = ClientRequests.GetFolderType(2);
            var makeFolderWindow = new Views.MakeFolderWindow(ServiceProvider);

            makeFolderWindow.ShowDialog();
        }

        private LetterView getNextLetter(LetterView letter)
        {

            int index = Letters.IndexOf(letter);

            if (0 < index && index == Letters.Count - 1)
                return Letters[index - 1];
            else
                if (index > 0)
                return Letters[index + 1];
            else
                throw new IndexOutOfRangeException();
        }


        private void DeleteLetterClickMethod()
        {

            LetterView newSelectedLetter = null;
            var cur = SelectedLetter;

            try
            {
                newSelectedLetter = getNextLetter(SelectedLetter);
            }
            catch (IndexOutOfRangeException ex)
            {

            }

            //MessageBox.Show(newSelectedLetter.Name);
            ClientRequests.DeleteLetter(SelectedLetter, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);


            InitializeDataGrid(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id);

            if (null != newSelectedLetter)
                SelectedLetter = newSelectedLetter;


        }

        bool f = false;
        private void SelectedItemChangedMethod(object arg)
        {
            if (!f)
            {
                InitializeDataGrid(((Models.DirectoryNode)arg).Folder.Id);
                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder = ((Models.DirectoryNode)arg).Folder;
                f = true;
            }
        }


        private IEnumerable<LetterType> _existLettersTypes;

        public IEnumerable<LetterType> ExistLettersTypes
        {
            set
            {
                _existLettersTypes = value;
                OnPropertyChanged(nameof(ExistLettersTypes));
            }
            get
            {
                return _existLettersTypes;
            }
        }

        public void InitializeMenu()
        {
            ExistLettersTypes = ClientRequests.GetAllLetterTypes();
        }

        private void OpenLetterViewWindowMethod(object arg)
        {
            var letterViewWindow = new Views.LetterViewWindow(ServiceProvider);
            letterViewWindow.ShowDialog();

            InitializeDataGrid(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id);
        }

        private void ShowBriefLetterMethod(object arg)
        {
            LetterPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            LetterPlugin.OnLoad(ServiceProvider);
            LetterPlugin.ReadOnly = true;
        }

        private IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private IList<Folder> _folders = new ObservableCollection<Folder>();

        public List<Models.Node> DirItems
        {
            get { return _dirItems; }
            set
            {
                _dirItems = value;
                OnPropertyChanged(nameof(DirItems));
            }
        }

        public LetterView SelectedLetter
        {
            set
            {
                _selectedLetter = value;
                if (null != value)
                {
                    ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType = ClientRequests.GetLetterType(_selectedLetter.Type);
                    ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView = _selectedLetter;
                }

                OnPropertyChanged(nameof(SelectedLetter));
            }
            get
            {
                return _selectedLetter;
            }
        }

        public Models.Node SelectedNode
        {
            set
            {
                _selectedNode = value;
                OnPropertyChanged(nameof(SelectedNode));
            }
            get
            {
                return _selectedNode;
            }
        }

        public void InitializeTreeView()
        {
            var itemProvider = new NodeProvider(ClientRequests.GetAllWorkerFolders(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id), ClientRequests.GetAllWorkerFolders(Guid.Empty));
            DirItems = itemProvider.DirItems;
        }

        public void InitializeDataGrid(Guid folderId)
        {
            try
            {
                Letters = ClientRequests.GetWorkerLettersInFolder(folderId, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id).ToList();
            }
            catch (Exception ex)
            {
                Letters = new List<LetterView>();
            }
        }

        public IList<LetterView> Letters
        {
            set
            {
                _letters = value;
                OnPropertyChanged(nameof(Letters));
            }
            get
            {
                return _letters;
            }
        }

        private ILetterPropertiesUIPlugin _letterPlugin;
        public ILetterPropertiesUIPlugin LetterPlugin
        {
            set
            {
                _letterPlugin = value;
                OnPropertyChanged(nameof(LetterPlugin));
            }
            get
            {
                return _letterPlugin;
            }
        }
    }
}
