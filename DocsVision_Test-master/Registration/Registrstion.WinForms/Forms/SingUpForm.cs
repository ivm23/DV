using System;
using System.Collections.Generic;

using System.Linq;

using System.Windows.Forms;
using Registration.ClientInterface;
using System.ComponentModel.Design;
using Registration.Logger;
using Registration.Model;

namespace Registration.WinForms.Forms
{
    internal partial class SingUpForm : Form
    {
        private IClientRequests _clientRequests;
        private Message.IMessageService _messageService;
        private readonly IServiceProvider _serviceProvider;
  //      private IEnumerable<string> databasesNames;

        public SingUpForm(IServiceProvider provider)
        {
            _serviceProvider = provider;

            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(singUp_Closing);
        }

        private IServiceProvider ServiceProvider => _serviceProvider;
        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private Message.IMessageService MessageService
        {
            get { return _messageService; }
        }

        public string NameW
        {
            set { singUpControl1.NameW = value; }
            get { return singUpControl1.NameW; }
        }

        public string Login
        {
            set { singUpControl1.Login = value; }
            get { return singUpControl1.Login; }
        }

        public string Password
        {
            set { singUpControl1.Password = value; }
            get { return singUpControl1.Password; }
        }

        public IEnumerable<string> DatabaseNames
        {
            set { singUpControl1.DatabaseNames = value; }
            get { return singUpControl1.DatabaseNames;  }
        }

        public string SelectDatabaseName
        {
            set { singUpControl1.SelectDatabaseName = value; }
            get { return singUpControl1.SelectDatabaseName; }
        }

        public void InitializeClientService()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        public void InitializeMessageService()
        {
            _messageService = (Message.IMessageService)ServiceProvider.GetService(typeof(Message.IMessageService));
        }

        private void InitializeDatabaseNames()
        {
            DatabaseNames = ClientRequests.GetDatabasesNames();
            SelectDatabaseName = DatabaseNames.First();
        }

        private void InitializeForm()
        {
            InitializeClientService();
            InitializeMessageService();
            InitializeDatabaseNames();
        }

        private void singUpForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeForm();
            }
            catch (Exception ex)
            {                
                NLogger.Logger.Error(ex.ToString());
            }
        }

        private Guid CreateWorker(string workerName, string workerLogin, string workerPassword)
        {
            return ClientRequests.CreateWorker(workerName, workerLogin, workerPassword);
        }

        private Guid SingUp(string workerName, string workerLogin, string workerPassword)
        {
            Guid workerId = Guid.Empty;

            if (!(string.IsNullOrEmpty(workerName) || string.IsNullOrEmpty(workerLogin) || string.IsNullOrEmpty(workerPassword)))
            {
                try 
                {
                    ClientRequests.WorkerIsExist(workerLogin);
                    MessageService.ErrorMessage(Message.MessageResource.ExistWorker);
                }
                catch (Exception ex)
                {
                    workerId = CreateWorker(workerName, workerLogin, workerPassword);
                    MessageService.InfoMessage(Message.MessageResource.SuccessfullRegistration);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(workerName)) MessageService.ErrorMessage(Message.MessageResource.EmptyName);
                else
                    if (string.IsNullOrEmpty(workerLogin)) MessageService.ErrorMessage(Message.MessageResource.EmptyLogin);
                else
                    if (string.IsNullOrEmpty(workerPassword)) MessageService.ErrorMessage(Message.MessageResource.EmptyPassword);
            }

            return workerId;
        }

        private void singUpB_Click(object sender, EventArgs e)
        {
            IClientRequests clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
            clientRequests.DatabaseName = SelectDatabaseName;

            Guid workerId = SingUp(NameW, Login, Password);

            if (Guid.Empty != workerId)
            {
                ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id = workerId;
            }
        }

        private void singUp_Closing(object sender, FormClosingEventArgs e)
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CloseReason = e.CloseReason;
        }
    }

}

