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
        public string _selectedDatabaseName { get; set; }
        public int _selectedDatabaseIndex { get; set; } = 0;

        private ObservableCollection<string> _databasesNames = new ObservableCollection<string>();
        public ObservableCollection<string> DatabasesNames
        {
            get
            {

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

        public ICommand SingInCommand { get; set; }
        public ICommand SingUpCommand { get; set; }

        private void InitializeCommands()
        {
            SingInCommand = new ViewModels.Command(arg => SingInMethod(arg));
            SingUpCommand = new ViewModels.Command(arg => SingUpMethod(arg));
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

        private void SingInMethod(object arg)
        {

            ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).DatabaseName = _selectedDatabaseName;

            _worker.Id = ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).AcceptAuthorisation(Login, Password);
            if (Guid.Empty != _worker.Id)
            {
                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id = _worker.Id;
                MessageBox.Show("Welcome!");

                ((Window)(arg)).Close();
            }
            else
            {
                MessageBox.Show("Login or password is wrong");
            }
        }

        private void SingUpMethod(object arg)
        {
            ((Window)(arg)).Hide();

            var singUpWindow = new Views.SingUpWindow(ServiceProvider);
            if (singUpWindow.ShowDialog() == true)
            {
                ((Window)(arg)).Close();
            }
        }
    }
}

