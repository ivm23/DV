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


        private void SingUp()
        {
            IClientRequests clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
            clientRequests.DatabaseName = SelectDatabaseName;

            Guid workerId = Guid.Empty;

            if (!(string.IsNullOrEmpty(NameW) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password)))
            {
                try 
                {
                    ClientRequests.WorkerIsExist(Login);
                    MessageService.ErrorMessage(Message.MessageResource.ExistWorker);
                }
                catch (Exception ex)
                {
                    workerId = CreateWorker(NameW, Login, Password);
                    MessageService.InfoMessage(Message.MessageResource.SuccessfullRegistration);

                    ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id = workerId;
                    DialogResult = DialogResult.OK;

                }
            }
            else
            {
                if (string.IsNullOrEmpty(NameW)) MessageService.ErrorMessage(Message.MessageResource.EmptyName);
                else
                    if (string.IsNullOrEmpty(Login)) MessageService.ErrorMessage(Message.MessageResource.EmptyLogin);
                else
                    if (string.IsNullOrEmpty(Password)) MessageService.ErrorMessage(Message.MessageResource.EmptyPassword);
            }
        }

        private void singUpB_Click(object sender, EventArgs e)
        {
            try
            {
                SingUp();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void singUp_Closing(object sender, FormClosingEventArgs e)
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CloseReason = e.CloseReason;
        }
    }

}

