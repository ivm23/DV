using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registration.ClientInterface;
using System.ComponentModel.Design;
using Registration.Model;
using System.IO;

namespace Registration.WinForms.Forms
{
    public partial class CreateFolderForm : Form
    {
        const string DisplayMember = "Name";
        const string ValueMember = "Id";
        const string WorkersName = "workersName";

        private IClientRequests _clientRequests;

        private List<FolderType> _folderTypes;

        private Controlers.ButtonCreateCancelControl _newButtonsControl;
        private Point _baseSize;
        private readonly IServiceProvider _serviceProvider;


        public CreateFolderForm(IServiceProvider provider)
        {
            InitializeComponent();
            _newButtonsControl = new Controlers.ButtonCreateCancelControl();
            this._newButtonsControl.CreateB.Click += new EventHandler(CreateFolder);
            this.comboFolderType.DropDownClosed += new EventHandler(FolderTypeIsChange);
            _serviceProvider = provider;
        }

        private IServiceProvider ServiceProvider => _serviceProvider;

        private Point BaseSize
        {
            get { return _baseSize; }
        }

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }


        private void InitializeClientService()
        {
            _clientRequests = (IClientRequests)(ServiceProvider.GetService(typeof(IClientRequests)));
        }

        private void InitializeBaseSize()
        {
            _baseSize = new Point(this.Size.Width, this.Size.Height);
        }

        public FolderType SelectedFolderType
        {
            set {
                comboFolderType.SelectedItem = value;
            }
            get { return (FolderType)comboFolderType.SelectedItem; }
        }

        public IEnumerable<FolderType> FolderType
        {
            set
            {
                _folderTypes = new List<FolderType>();
                foreach (FolderType folderType in value)
                {
                    _folderTypes.Add(folderType);
                }

                comboFolderType.Items.Clear();
                comboFolderType.DataSource = _folderTypes;
                comboFolderType.DisplayMember = DisplayMember;
                comboFolderType.ValueMember = ValueMember;
            }
            get
            {
                return _folderTypes;
            }
        }

        public string FolderName
        {
            set
            {
                txtFolderName.Text = value;
            }
            get
            {
                return txtFolderName.Text;
            }
        }

        private void CreateFolderForm_Load(object sender, EventArgs e)
        {
            InitializeClientService();
            InitializeBaseSize();

            InitializeFolderPluginUI(comboFolderType);
        }


        private void CreateFolder()
        {
            IFolderPropertiesUIPlugin clientUIPlugin = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentFolderPropertiesPlugin;

            global::Registration.Model.FolderProperties folderProp = clientUIPlugin.FolderProperties;

            StringBuilder data = new StringBuilder();
            if (null != folderProp)
            {
                foreach (KeyValuePair<string, string> info in folderProp.Properties)
                {
                    data.Append(info);
                }
            }

            ClientRequests.CreateFolder(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id, clientUIPlugin.FolderProperties.Name, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id, SelectedFolderType.Id, data.ToString());
        }

        private void CreateFolder(object sender, EventArgs e)
        {
            CreateFolder();
        }

        private void InitializeFolderPluginUI(object sender)
        {
            FolderType selectedFolderType;
            if (sender == this.comboFolderType)
                selectedFolderType = SelectedFolderType;
            else
                selectedFolderType = ((IFolderPropertiesUIPlugin)sender).FolderType;

            IFolderPropertiesUIPlugin clientUIPlugin = ((PluginService)(ServiceProvider.GetService(typeof(PluginService)))).GetFolderPropetiesPlugin(selectedFolderType);

            var allWorkersInfo = new FolderProperties();
            var allWorkers = ClientRequests.GetAllWorkers();

            foreach (string info in allWorkers)
            {
                allWorkersInfo.Properties.Add(info, info);
            }

            clientUIPlugin.FolderProperties = allWorkersInfo;
            clientUIPlugin.OnLoad(ServiceProvider);

            clientUIPlugin.ChangedFolderTypePlugin += new EventHandler(FolderTypeIsChange);

            clientUIPlugin.FolderType.Id = selectedFolderType.Id;
            clientUIPlugin.FolderType.Name = selectedFolderType.Name;
            clientUIPlugin.FolderType.TypeClientUI = selectedFolderType.TypeClientUI;
            clientUIPlugin.FolderType.TypeFolderService = selectedFolderType.TypeFolderService;

            Control newControl = (Control)clientUIPlugin;

            newControl.Location = new Point(0, 0);

            _newButtonsControl.Location = new Point(0, newControl.Size.Height);

            int width = Math.Max(Math.Max(BaseSize.X, newControl.Width), _newButtonsControl.Size.Width);

            this.Size = new Size(width, Math.Max(BaseSize.Y, newControl.Size.Height + _newButtonsControl.Location.Y + _newButtonsControl.Size.Height));

            this.Controls.Clear();
            this.Controls.Add(newControl);
            this.Controls.Add(_newButtonsControl);

            SelectedFolderType.Id = selectedFolderType.Id;
            SelectedFolderType.Name = selectedFolderType.Name;
            SelectedFolderType.TypeFolderService = selectedFolderType.TypeFolderService;
            SelectedFolderType.TypeClientUI = selectedFolderType.TypeClientUI;

            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentFolderPropertiesPlugin = clientUIPlugin;
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Name = clientUIPlugin.FolderProperties.Name;
        }

        void FolderTypeIsChange(object sender, EventArgs e)
        {
            InitializeFolderPluginUI(sender);
        }
    }
}
