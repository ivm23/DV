using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Registration.ClientInterface;
using Registration.WinForms;

namespace Registration.WPF
{
    class MainWindowViewModel : ViewModels.Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;

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

        private ObservableCollection<Folder> _folders = new ObservableCollection<Folder>();
        public ObservableCollection<Folder> Folders
        {
            get
            {
                return _folders;
            }
            set
            {
                _folders = value;
                OnPropertyChanged("Folders");
            }
        }

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
        
        public void InitializeTreeView()
        {
            var folders = ClientRequests.GetAllWorkerFolders(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id); 
            foreach(var folder in folders)
            {
                _folders.Add(folder);
            }
        }
    }

}
