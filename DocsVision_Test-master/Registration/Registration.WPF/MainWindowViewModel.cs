using System;
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
    class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private List<Models.Node> _dirItems;
        private LetterView _selectedLetter;
        private Models.Node _selectedNode;
        private IEnumerable<LetterView> _letters;

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
            MakeLetter = new ViewModels.Command(arg => MakeLetterMethod());
            CreateFolder = new ViewModels.Command(arg => CreateFolderMethod());

            AA = new ViewModels.Command(arg => AMethod());
        }

        public ICommand SelectedItemChanged { get; set; }
        public ICommand OpenLetterViewWindow { set; get; }
        public ICommand DeleteLetterClick { set; get; }
        public ICommand MakeLetter { set; get; }

        public ICommand CreateFolder { set; get; }

        public ICommand AA { get; set; }

        private void AMethod()
        {
            MessageBox.Show("Hi");
        }

        private void MakeLetterMethod()
        {
            var window = new Views.MakeLetterWindow(ServiceProvider);          
            window.ShowDialog();
        }

        private void CreateFolderMethod()
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType = ClientRequests.GetFolderType(2);
            var makeFolderWindow = new Views.MakeFolderWindow(ServiceProvider);

            makeFolderWindow.ShowDialog();
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
