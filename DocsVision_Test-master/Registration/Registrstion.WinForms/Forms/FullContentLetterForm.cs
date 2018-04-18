using System;
using System.Windows.Forms;
using Registration.Model;
using Registration.ClientInterface;
using System.ComponentModel.Design;
using Registration.Logger;
using System.Drawing;
using System.Collections.Generic;
using System.Xml;

namespace Registration.WinForms.Forms
{
    internal partial class FullContentLetterForm : Form
    {
        private IClientRequests _clientRequests;
        private Message.IMessageService _messageService;
        private readonly IServiceProvider _serviceProvider;

        private List<Control> _baseControls;
        private Point _baseSizeHeight;

        public FullContentLetterForm(IServiceProvider provider)
        {
            _serviceProvider = provider;
            InitializeComponent();
        }

        private Point BaseSizeHeight
        {
            get { return _baseSizeHeight; }
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

        private void InitializeBaseSizeHeight()
        {
            _baseSizeHeight = new Point(this.Size.Width, this.Size.Height);
        }

        private IServiceProvider ServiceProvider => _serviceProvider;

        public IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        public Message.IMessageService MessageService
        {
            get { return _messageService; }
        }

        private void InitializeClientService()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private void InitializeMessageService()
        {
            _messageService = (Message.IMessageService)ServiceProvider.GetService(typeof(Message.IMessageService));
        }

        public void InitializeForm()
        {
            InitializeClientService();
            InitializeMessageService();
            InitializeBaseControls();

            LetterView letterView = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;

            LetterType selectedLetterType = ClientRequests.GetLetterType(letterView.Type);

            ILetterPropertiesUIPlugin newControl = ((PluginService)(ServiceProvider.GetService(typeof(PluginService)))).GetLetterPropetiesPlugin(selectedLetterType);

            int tabIndex = 0;
            newControl.OnLoad(ServiceProvider);

            ((Control)newControl).TabIndex = tabIndex;

            int heightSize = 0;
            int locationY = 0;
            foreach(Control control in BaseControls)
            {
                if (control.Location.Y + control.Size.Height > locationY)
                    locationY = control.Location.Y + control.Size.Height;

                heightSize += control.Size.Height;
            }

            this.Controls.Clear();
            ((Control)newControl).Location = new Point(0, locationY);
          
            this.Size = new Size(((Control)newControl).Size.Width, ((Control)newControl).Size.Height + heightSize);

            this.Controls.Add(((Control)newControl));

            foreach(Control control in BaseControls)
            {
                ++tabIndex;
                control.TabIndex = tabIndex;
                this.Controls.Add(control);
            }
        }

        private void DeleteLetter(LetterView letterView, Guid workerId)
        {
            ClientRequests.DeleteLetter(letterView, workerId);
        }

        private void deleteLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageService.QuestionMessage(Message.MessageResource.DeleteLetter) == DialogResult.Yes)
            {
                DeleteLetter(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
                Close();
            }
        }

        private void FullContentLetterForm_Load(object sender, EventArgs e)
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
    }
}
