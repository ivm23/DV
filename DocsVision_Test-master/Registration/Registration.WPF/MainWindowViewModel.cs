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
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows;
using System.Security;

namespace Registration.WPF
{
    class MainWindowViewModel : Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        private IMessageService _messageService;
        private IClientRequests _clientRequests;
        private List<Models.Node> _dirItems;
        private LetterView _selectedLetter;
        private ObservableCollection<LetterView> _letters = new ObservableCollection<LetterView>();

        public MainWindowViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            InitializeClientRequests();
            InitializeMessageService();

            SelectedFolderNodeChanged = new ViewModels.Command(arg => SelectedFolderNodeChangedMethod(arg));
            OpenLetterViewWindow = new ViewModels.Command(args => OpenLetterViewWindowMethod(args));
            DeleteLetterClick = new ViewModels.Command(args => DeleteLetterClickMethod());
            MakeLetter = new ViewModels.Command(arg => MakeLetterMethod(arg));
            CreateFolder = new ViewModels.Command(arg => CreateFolderMethod());
            ShowBriefLetter = new ViewModels.Command(arg => ShowBriefLetterMethod(arg));
            EditFolder = new ViewModels.Command(arg => EditFolderMethod(arg));
            DeleteFolder = new ViewModels.Command(arg => DeleteFolderMethod(arg));
        }

        private IMessageService MessageService
        {
            get { return _messageService; }
        }
        public ICommand SelectedFolderNodeChanged { get; set; }
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
            DeleteEnable = false;
            LetterPlugin = null;
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

        private int _index;
        private void getNextLetter(LetterView letter)
        {
            int index = Letters.IndexOf(letter);

            if (0 < index && index == Letters.Count - 1)
                _index = index - 1;
            else
                if (index >= 0)
                _index = index;
            else
                throw new IndexOutOfRangeException();
        }

        private void DeleteLetterClickMethod()
        {
            if (MessageService.QuestionMessage(MessageResources.DeleteLetter) == MessageBoxResult.Yes)
            {
                getNextLetter(SelectedLetter);
                ClientRequests.DeleteLetter(SelectedLetter, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
                Letters.Remove(SelectedLetter);

                restoreSelectedLetter();
            }
        }

        private void SelectedFolderNodeChangedMethod(object arg)
        {
            if (null != arg)
            {
                DeleteEnable = false;
                InitializeDataGrid(((Models.DirectoryNode)arg).Folder.Id);
                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder = ((Models.DirectoryNode)arg).Folder;
                
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

        private void InitializeMessageService()
        {
            _messageService = (IMessageService)ServiceProvider.GetService(typeof(IMessageService));
        }

        private void restoreSelectedLetter()
        {
            if (_index != -1 && Letters.Count() > 0)
                SelectedLetter = Letters[_index];
        }

        private void OpenLetterViewWindowMethod(object arg)
        {
            getNextLetter(SelectedLetter);

            var letterViewWindow = new Views.LetterViewWindow(ServiceProvider);
            letterViewWindow.ShowDialog();
            ClientRequests.LetterIsRead(((LetterView)(arg)).Id, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            InitializeDataGrid(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id);
                
            restoreSelectedLetter();
        }

        private void ShowBriefLetterMethod(object arg)
        {
            LetterPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            LetterPlugin.OnLoad(ServiceProvider);
            LetterPlugin.ReadOnly = true;

            if (null != arg)
                DeleteEnable = true;
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

        public void InitializeTreeView()
        {
            var itemProvider = new NodeProvider(ClientRequests.GetAllWorkerFolders(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id), ClientRequests.GetAllWorkerFolders(Guid.Empty), ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder);
            DirItems = itemProvider.DirItems;
        }
                
        public void InitializeDataGrid(Guid folderId)
        {
            try
            {
                Letters.Clear();
                var letters = ClientRequests.GetWorkerLettersInFolder(folderId, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id).ToList();
                foreach (LetterView letterView in letters)
                {
                    Letters.Add(letterView);
                }
            }
            catch (Exception ex)
            {
                Letters = new ObservableCollection<LetterView>();
            }
        }

        public ObservableCollection<LetterView> Letters
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

        private Folder _selectedFolder;

        public Folder SelectedFolder
        {
            set
            {
                _selectedFolder = value;
            }
            get
            {
                return _selectedFolder;
            }
        }


        private bool _deleteEnable = false;

        public bool DeleteEnable
        {
            set
            {
                _deleteEnable = value;
                OnPropertyChanged(nameof(DeleteEnable));
            }
            get
            {
                return _deleteEnable;
            }
        }

    }
}
