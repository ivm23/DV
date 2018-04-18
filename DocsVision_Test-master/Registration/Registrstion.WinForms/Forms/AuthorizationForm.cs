using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Registration.ClientInterface;
using System.ComponentModel.Design;
using Registration.Logger;
using Registration.Model;

namespace Registration.WinForms
{
    internal partial class Registration : Form
    {
        private IClientRequests _clientRequests;
        private Message.IMessageService _messageService;

        private readonly IServiceProvider _serviceProvider;

        IEnumerable<string> databasesNames;

        public Registration(IServiceProvider provider)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(singUp_Closing);
            _serviceProvider = provider;
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

        private void InitializeForm()
        {
            InitializeClientService();
            InitializeMessage();

            databasesNames = ClientRequests.GetDatabasesNames();

            foreach (string databaseName in databasesNames)
            {
                databaseNamesCB.Items.Add(databaseName);
            }
            databaseNamesCB.SelectedItem = databasesNames.First();
        }

        private void InitializeClientService()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private void InitializeMessage()
        {
            _messageService = (Message.IMessageService)ServiceProvider.GetService(typeof(Message.IMessageService));
        }

        private void Registration_Load(object sender, EventArgs e)
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

        private Guid AcceptAuthorisation(string workerLogin, string workerPassword)
        {
            return ClientRequests.AcceptAuthorisation(workerLogin, workerPassword);
        }

        private Guid SingIn(string workerLogin, string workerPassword)
        {
            Guid workerId = Guid.Empty;
            if (string.IsNullOrEmpty(workerLogin) || string.IsNullOrEmpty(workerPassword))
            {
                MessageService.ErrorMessage(Message.MessageResource.EmptyLoginOrPassword);
            }
            else
            {
                workerId = AcceptAuthorisation(workerLogin, workerPassword);

                if (Guid.Empty.Equals(workerId))
                {
                    MessageService.ErrorMessage(Message.MessageResource.WrongLoginOrPassword);
                }
                else
                {
                    MessageService.SingleMessage(Message.MessageResource.Welcome);
                }
            }
            return workerId;
        }

        private void singIn_Click(object sender, EventArgs e)
        {
            ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).DatabaseName = databaseNamesCB.SelectedItem.ToString();

            Guid workerId = ((ApplicationState)(ServiceProvider.GetService(typeof(ApplicationState)))).Worker.Id = SingIn(loginTB.Text, passwordTB.Text);

            if (!workerId.Equals(Guid.Empty))
            {
                Hide();
            }
        }

        private void singUp_Click(object sender, EventArgs e)
        {
            Hide();
            using (var singUpForm = new Forms.SingUpForm(ServiceProvider))
            {
                singUpForm.ShowDialog();
            }
            Show();
        }

        private void singUp_Closing(object sender, FormClosingEventArgs e)
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CloseReason = e.CloseReason;
        }
    }

}
