using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registration.Model;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Registration.WinForms.Controlers
{
    internal partial class FullContentLetterControl : UserControl, ILetterPropertiesUIPlugin
    {
        public event EventHandler AddedReceiver;
        private LetterProperties _letterProperties = new LetterProperties();
        private LetterView _letterView = new LetterView();
        private IServiceProvider _serviceProvider;

        private IList<string> _namesAndLoginsReceivers = new List<string>();

        private IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        public FullContentLetterControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            InitializeReceivers();
            InitializeSender();
        }

        public LetterProperties LetterExtendedProperties
        {
            set
            {
                _letterProperties = value;
            }
            get
            {
                return _letterProperties;
            }
        }

        public LetterView StandartLetter
        {
            set
            {
                nameLetterTB.Text = value.Name;
                nameSenderTB.Text = value.SenderName;
                dateLetterTB.Text = value.Date.ToString();
                workersEditorControl2.SetWorkers(value.ReceiversName);
            }
            get
            {
                _letterView.Name = nameLetterTB.Text;
                _letterView.SenderName = nameSenderTB.Text;
                _letterView.Text = textLetterTB.Text;
                _letterView.ReceiversName.AddRange(ReadOnly ? workersEditorControl1.GetWorkers() : _namesAndLoginsReceivers);

                return _letterView;
            }
        }

        public LetterView LetterView
        {
            set
            {
                StandartLetter = value;
            }
            get
            {
                _letterView = StandartLetter;
                return _letterView;
            }
        }

        public bool ReadOnly
        {
            set
            {
                nameLetterTB.ReadOnly = value;
                textLetterTB.ReadOnly = value;
                dateLetterTB.Visible = value;
                labelDate.Visible = value;
           //     nameReceiversCB.Visible = !value;
                workersEditorControl2.ReadOnly = value;
                addReceiversB.Visible = !value;
          //      nameReceiversCB.DropDownStyle = (value ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown);
            }
            get
            {
                return textLetterTB.ReadOnly;
            }
        }

        public void InitializeReceivers()
        {
            workersEditorControl2.InitializeAllWorkers(ServiceProvider);
        }

        public void InitializeSender()
        {
            Guid workerId = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id;
            nameSenderTB.Text = ((ClientInterface.IClientRequests)ServiceProvider.GetService(typeof(ClientInterface.IClientRequests))).GetWorkerName(workerId);
        } 

        private void addReceiversB_Click_1(object sender, EventArgs e)
        {
            try
            {
                _namesAndLoginsReceivers.Add(workersEditorControl2.GetSelectedWorker());
            }
            catch(Exception ex)
            {
                ((Message.IMessageService)ServiceProvider.GetService(typeof(Message.IMessageService))).ErrorMessage(Message.MessageResource.EmptyListRecipient);
            }
        }

        private void FullContentLetterControl_Load(object sender, EventArgs e)
        {
            Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top);
        }

        private void workersEditorControl2_Load(object sender, EventArgs e)
        {

        }
    }
}

