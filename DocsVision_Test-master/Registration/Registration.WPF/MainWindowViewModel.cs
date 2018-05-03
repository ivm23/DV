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
using Registration.WinForms;
using System.Windows.Input;
using System.Windows;

namespace Registration.WPF
{
    class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private List<Models.Node> _dirItems;
        private LetterView _selectedLetter;
        private Models.Node _selectedNode;
        private IEnumerable<LetterView> _letters;
   //     private ILetterPropertiesUIPlugin _letterPlugin;

        public MainWindowViewModel() { }

        public MainWindowViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            InitializeClientRequests();
            ClickCommand = new ViewModels.Command(arg => ClickMethod());
            SelectedItemChanged = new ViewModels.Command(arg => SelectedItemChangedMethod(arg));
            MouseDoubleClick = new ViewModels.Command(args => MouseDoubleClickMethod(args));
            DeleteLetterClick = new ViewModels.Command(args => DeleteLetterClickMethod());

            TreeViewRightClick = new ViewModels.Command(args => TreeViewRightClickMethod(args));


            DataContextChange = new ViewModels.Command(arg => ClickMethod(arg));
        }


    public ICommand ClickCommand { get; set; }
        public ICommand SelectedItemChanged { get; set; }
        public ICommand TreeViewRightClick { get; set; }

        public ICommand MouseDoubleClick { set; get; }
        public ICommand DeleteLetterClick { set; get; }

        private void ClickMethod()
        {
        }

        private IFolderPropertiesUIPlugin InitializeFolderPlugin(Models.DirectoryNode directoryNode)
        {
            FolderType folderType = ClientRequests.GetFolderType(directoryNode.Folder.Type);

            IFolderPropertiesUIPlugin folderPlugin = ViewPluginCreater.Create(folderType, (PluginService)ServiceProvider.GetService(typeof(PluginService)));
            folderPlugin.OnLoad(ServiceProvider);

            return folderPlugin;         
        }


        private void TreeViewRightClickMethod(object arg) { 
                  
            var window = new Views.MakeFolderWindow(ServiceProvider);
            window.InitializeMakeFolderWindow(((Models.DirectoryNode)(arg)).Folder.Type);
            window.ShowDialog();
        }

       public string NameFolder { set; get; }

        public ICommand DataContextChange { get; set; }
        private void ClickMethod(object arg)
        {
            MessageBox.Show("aa");
        }


        private void DeleteLetterClickMethod()
        {
            ClientRequests.DeleteLetter(SelectedLetter, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
        }

        bool f = false;
        private void SelectedItemChangedMethod(object arg)
        {
            if (!f)
            {
                InitializeDataGrid(((Models.DirectoryNode)arg).Folder.Id);
                f = true;
            }
        }


        private IDictionary<string, LetterType> _existLettersTypes = new Dictionary<string, LetterType>();

        public IDictionary<string, LetterType> ExistLettersTypes
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
            IEnumerable<LetterType> allLettersType = ClientRequests.GetAllLetterTypes();
            IDictionary<string, LetterType> lettersType = new Dictionary<string, LetterType>();

            foreach (LetterType letterType in allLettersType)
            {
                lettersType.Add(letterType.Name, letterType);
            }
            ExistLettersTypes = lettersType;
        }

        private void MouseDoubleClickMethod(object arg)
        {   
        //    LetterPlugin.OnLoad(ServiceProvider);
      //      LetterPlugin.ReadOnly = true;

            var window = new Views.FullContentLetterWindow(ServiceProvider);
            window.InitializeFullContentLetterWindow();
     //       window.DataContext = LetterPlugin;
            window.ShowDialog();
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

        private ObservableCollection<LetterView> _lettersViews = new ObservableCollection<LetterView>();
        public ObservableCollection<LetterView> LettersViews
        {
            get
            {
                return _lettersViews;
            }
            set
            {
                _lettersViews = value;
                OnPropertyChanged(nameof(LettersViews));
            }
        }

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

                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType = ClientRequests.GetLetterType(_selectedLetter.Type);
                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView = _selectedLetter;

             //   LetterPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));

                OnPropertyChanged(nameof(SelectedLetter));

         //       LetterPlugin.OnLoad(ServiceProvider);
         //       LetterPlugin.ReadOnly = true;
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
            Letters = ClientRequests.GetWorkerLettersInFolder(folderId, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
        }

        private object _selectedValue;

        public object SelectedValue
        {
            set
            {
                _selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }

            get
            {
                return _selectedValue;
            }
        }

        public IEnumerable<LetterView> Letters
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

 /*       public ILetterPropertiesUIPlugin LetterPlugin
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
        }*/
    }
}
