using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Registration.ClientInterface;
using System.ComponentModel.Design;
using Registration.Logger;
using Registration.Model;
using System.Drawing;

namespace Registration.WinForms.Forms
{
    internal partial class MakeLetterForm : Form
    {
        private IClientRequests _clientRequests;
        private Message.IMessageService _messageService;
        private readonly IServiceProvider _serviceProvider;

        private List<Control> _baseControls;
        private Point _baseSizeHeight;

        public MakeLetterForm(IServiceProvider provider)
        {
            InitializeComponent();
            _serviceProvider = provider;
            this.KeyDown += new KeyEventHandler(form_KeyDown);
        }

        private IServiceProvider ServiceProvider => _serviceProvider;
        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }
        private Point BaseSizeHeight
        {
            get { return _baseSizeHeight; }
        }


        private Message.IMessageService MessageService
        {
            get { return _messageService; }
        }

        private List<Control> BaseControls
        {
            get { return _baseControls; }
        }

        private void InitializeBaseControls()
        {
            _baseControls = new List<Control>();
            foreach (Control control in this.Controls)
            {
                BaseControls.Add(control);
            }
            InitializeBaseSizeHeight();
        }

        private void CreateLetter(string letterName, Guid workerId, IEnumerable<string> workerNameAndLogin, string letterText, string extendedData, int type)
        {
            ClientRequests.CreateLetter(letterName, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id, workerNameAndLogin, letterText, extendedData, type);
        }

        private bool SendLetter(Guid workerId)
        {
            LetterType selectedLetterType = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType;
            ILetterPropertiesUIPlugin clientUIPlugin = ((ApplicationState)(ServiceProvider.GetService(typeof(ApplicationState)))).CurrentLetterPropertiesPlugin;

            LetterView letterView = clientUIPlugin.LetterView;


            if (string.IsNullOrEmpty(letterView.Name))
            {
               MessageService.ErrorMessage(Message.MessageResource.EmptyNameInLetter);
                return false;
            }
            if (letterView.ReceiversName.Count() == 0)
            {
                MessageService.ErrorMessage(Message.MessageResource.EmptyListRecipient);
                return false;
            }

            CreateLetter(letterView.Name, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id, letterView.ReceiversName, letterView.Text, letterView.ExtendedData, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType.Id);
            return true;
        }

        private void InitializeClientService()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private void InitializeMessageService()
        {
            _messageService = (Message.IMessageService)ServiceProvider.GetService(typeof(Message.IMessageService));
        }

        private void InitializeBaseSizeHeight()
        {
            _baseSizeHeight = new Point(this.Size.Width, this.Size.Height);
        }

        private void InitializeForm()
        {
            InitializeClientService();
            InitializeMessageService();
            int tabIndex = 0;

            var selectedLetterType = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType;


            ILetterPropertiesUIPlugin newControl = ((PluginService)(ServiceProvider.GetService(typeof(PluginService)))).GetLetterPropetiesPlugin(selectedLetterType);
            ((Control)newControl).TabIndex = tabIndex;

            newControl.OnLoad(ServiceProvider);
            newControl.ReadOnly = false;

            var clientService = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
            var workers = clientService.GetAllWorkers();
            this.Size = new Size(((Control)newControl).Size.Width, ((Control)newControl).Height);
            this.Controls.Add(((Control)newControl));
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentLetterPropertiesPlugin = newControl;
        }

        private void MakeLetterForm_Load(object sender, EventArgs e)
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



        private void sendLetterB_Click_1(object sender, EventArgs e)
        {
            if (SendLetter( ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id))
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)            
                this.Close();
        }
    }
}