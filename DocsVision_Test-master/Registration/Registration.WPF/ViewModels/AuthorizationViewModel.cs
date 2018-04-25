using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using Registration.Model;
using Registration.ClientInterface;
using System.Windows;
using Registration.WinForms;

namespace Registration.WPF.ViewModels
{
    class AuthorizationViewModel : Notifier
    {
        private Worker _worker = new Worker();
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private WinForms.Message.IMessageService _messageService;
        public string _selectedDatabaseNames { get; set; }

        private ObservableCollection<string> _databasesNames = new ObservableCollection<string>();
        public ObservableCollection<string> DatabasesNames
        {
            get {
               
                return _databasesNames;
            }
            set
            {
                _databasesNames = value;
                OnPropertyChanged("DatabasesNames");
            }
        }

        private WinForms.Message.IMessageService MessageService
        {
            get
            {
                return _messageService;
            }
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }
        public string Login
        {
            set
            {
                _worker.Login = value;
                OnPropertyChanged("_worker.Login");
            }
            get
            {
                return _worker.Login;
            }
        }

        public string Password
        {
            set
            {
                _worker.Password = value;
                OnPropertyChanged("_worker.Password");
            }
            get
            {
                return _worker.Password;
            }
        }

        private void InitializeDatabasesNames()
        {
            IEnumerable<string> _databasesNames = ClientRequests.GetDatabasesNames();

            foreach (var name in _databasesNames)
            {

                DatabasesNames.Add(name);
            }
        }

        private void InitializeCommands()
        {
            SingInCommand = new ViewModels.Command(arg => SingInMethod());
            SingUpCommand = new ViewModels.Command(arg => SingUpMethod());
        }

        private void InitializeMessageService()
        {
             _messageService = (WinForms.Message.IMessageService)ServiceProvider.GetService(typeof(WinForms.Message.IMessageService));
        }

        public AuthorizationViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            InitializeClientRequests();
            InitializeMessageService();            
            InitializeDatabasesNames();
            InitializeCommands();
        }


        public ICommand SingInCommand { get; set; }
        public ICommand SingUpCommand { get; set; }

        private void SingInMethod()
        {

            ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).DatabaseName = _selectedDatabaseNames;

            _worker.Id = ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).AcceptAuthorisation(_worker.Login, _worker.Password);
            if (Guid.Empty != _worker.Id)
            {
                //MessageService.InfoMessage();                
                MessageBox.Show("Welcome!");
                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id = _worker.Id;
            }
            else
            {
                MessageService.InfoMessage("Login or password is wrong");
            }

            MessageBox.Show(_worker.Id.ToString());
            
        }
        private void SingUpMethod()
        {
            var singUpWindow = new Views.SingUpWindow();
               singUpWindow.ShowDialog();

        }

    }
}

