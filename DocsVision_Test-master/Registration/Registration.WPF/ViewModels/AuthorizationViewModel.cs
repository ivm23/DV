using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

using Registration.Model;
using Registration.ClientInterface;
using Registration.Logger;

namespace Registration.WPF.ViewModels
{
    class AuthorizationViewModel : Notifier
    {
        private Worker _worker = new Worker();
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private IMessageService _messageService;
        public string _selectedDatabaseName { get; set; }
        public int _selectedDatabaseIndex { get; set; } = 0;

        private ObservableCollection<string> _databasesNames = new ObservableCollection<string>();
        public ObservableCollection<string> DatabasesNames
        {
            get { return _databasesNames; }
            set
            {
                _databasesNames = value;
                OnPropertyChanged("DatabasesNames");
            }
        }

        private IMessageService MessageService
        {
            get { return _messageService; }
        }

        private void InitializeMessageService()
        {
            _messageService = (IMessageService)ServiceProvider.GetService(typeof(IMessageService));
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

        public string Login
        {
            set
            {
                _worker.Login = value;
                OnPropertyChanged("_worker.Login");
            }
            get { return _worker.Login; }
        }

        public string Password
        {
            set
            {
                _worker.Password = value;
                OnPropertyChanged("_worker.Password");
            }
            get { return _worker.Password; }
        }

        private void InitializeDatabasesNames()
        {
            IEnumerable<string> _databasesNames = ClientRequests.GetDatabasesNames();

            foreach (var name in _databasesNames)
            {
                DatabasesNames.Add(name);
            }
        }

        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        private void InitializeCommands()
        {
            SignInCommand = new ViewModels.Command(arg => SignInMethod(arg));
            SignUpCommand = new ViewModels.Command(arg => SignUpMethod(arg));
        }

        private void InitializeAuthorizationViewModel()
        {
            InitializeClientRequests();
            InitializeMessageService();
            InitializeDatabasesNames();
            InitializeCommands();
        }

        public AuthorizationViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            try
            {
                InitializeAuthorizationViewModel();
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }

        }

        private void SignInMethod(object arg)
        {

            ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).DatabaseName = _selectedDatabaseName;

            _worker.Id = ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).AcceptAuthorisation(Login, Password);
            if (Guid.Empty != _worker.Id)
            {
                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id = _worker.Id;
                MessageService.InfoMessage(MessageResources.Welcome);

                ((Window)(arg)).Close();
            }
            else
            {
                MessageService.InfoMessage(MessageResources.WrongLoginOrPassword);
            }
        }

        private void SignUpMethod(object arg)
        {
            ((Window)(arg)).Hide();

            var singUpWindow = new Views.SignUpWindow(ServiceProvider);
            if (singUpWindow.ShowDialog() == true)
            {
                ((Window)(arg)).Close();
            }
        }

    }

}

