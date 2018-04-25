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

namespace Registration.WPF
{
    class MainWindowViewModel : ViewModels.Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private List<Models.Node> _dirItems;

        public MainWindowViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;


            InitializeClientRequests();

          
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
                OnPropertyChanged("LettersViews");
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


        public void InitializeTreeView()
        {

      //       folders = ClientRequests.GetAllWorkerFolders(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            var itemProvider = new NodeProvider(ClientRequests.GetAllWorkerFolders(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id), ClientRequests.GetAllWorkerFolders(Guid.Empty));
            DirItems = itemProvider.DirItems;
        }

    }

}
