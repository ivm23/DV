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
using Registration.WinForms;
using System.Windows.Input;

namespace Registration.WPF
{
    class MainWindowViewModel : ViewModels.Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private List<Models.Node> _dirItems;
        private Letter _selectedLetter;
        private Models.Node _selectedNode;
        private IEnumerable<Letter> _letters;


        public MainWindowViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;


            InitializeClientRequests();
            ClickCommand = new ViewModels.Command(arg => ClickMethod());
        }

        public ICommand ClickCommand { get; set; }
        private void ClickMethod()
        {
            var a = SelectedValue;
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

        public Letter SelectedLetter
        {
            set
            {
                _selectedLetter = value;
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
            var allLetters = ClientRequests.GetWorkerLettersInFolder(folderId, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            Letters = allLetters;
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

        public IEnumerable<Letter> Letters
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
    }


}
