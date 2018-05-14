﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Registration.ClientInterface;
using System.Windows;
using Registration.Model;

namespace Registration.WPF.ViewModels
{
    class SignUpViewModel : Notifier
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

        public string NameW
        {
            set
            {
                _worker.Name = value;
                OnPropertyChanged(nameof(NameW));
            }
            get { return _worker.Name; }
        }

        public string Login
        {
            set
            {
                _worker.Login = value;
                OnPropertyChanged(nameof(Login));
            }
            get { return _worker.Login; }
        }


        public string Password
        {
            set
            {
                _worker.Password = value;
                OnPropertyChanged(nameof(Password));
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

        public ICommand CancelCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        private void InitializeCommands()
        {
            CancelCommand = new ViewModels.Command(arg => CancelMethod(arg));
            SignUpCommand = new ViewModels.Command(arg => SignUpMethod(arg));
        }

        private void InitializeMessageService()
        {
            _messageService = (IMessageService)ServiceProvider.GetService(typeof(IMessageService));
        }

        public SignUpViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            InitializeClientRequests();
            InitializeMessageService();
            InitializeDatabasesNames();
            InitializeCommands();
        }

        private void SignUpMethod(object arg)
        {
            var window = (Window)arg;

            Guid workerId = Guid.Empty;

            ClientRequests.DatabaseName = _selectedDatabaseName;

            if (!(string.IsNullOrEmpty(NameW) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password)))
            {
                try
                {
                    ClientRequests.WorkerIsExist(Login);
                        MessageService.InfoMessage(MessageResources.ExistWorker);
                        window.DialogResult = null;
                }
                catch (Exception ex)
                {
                    workerId = ClientRequests.CreateWorker(NameW, Login, Password);
                    ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id = workerId;


                    if (window != null)
                    {
                        window.DialogResult = true;
                        window.Close();
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(NameW))
                {
                    MessageService.ErrorMessage(MessageResources.EmptyName);
                    window.DialogResult = null;
                }

                else
                    if (string.IsNullOrEmpty(Login))
                {
                    MessageService.ErrorMessage(MessageResources.EmptyLogin);
                    window.DialogResult = null;
                }
                else
                    if (string.IsNullOrEmpty(Password))
                {
                    MessageService.ErrorMessage(MessageResources.EmptyPassword);
                    window.DialogResult = null;
                }

            }
        }

        private void CancelMethod(object arg)
        {
            ((Window)arg).DialogResult = null;
            ((Window)arg).Close();
            
        }
    }
}
