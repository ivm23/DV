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

        private Dictionary<FolderType, IFolderPropertiesUIPlugin> _existClientPlugin;
        private Controlers.ButtonCreateCancelControl _newButtonsControl;
        private List<Control> _baseControls;
        private Point _baseSizeHeight;
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

        private Point BaseSizeHeight
        {
            get { return _baseSizeHeight; }
        }

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private Dictionary<FolderType, IFolderPropertiesUIPlugin> ExistClientPlugin
        {
            get
            {
                return _existClientPlugin;
            }
        }

        private List<Control> BaseControls
        {
            get { return _baseControls; }
        }

        private void InitializeClientService()
        {
            _clientRequests = (IClientRequests)(ServiceProvider.GetService(typeof(IClientRequests)));
        }

        private void InitializeBaseControls()
        {
            _baseControls = new List<Control>();
            foreach(Control control in this.Controls)
            {
                BaseControls.Add(control);
            }
        }

        private void InitializeBaseSizeHeight()
        {
            _baseSizeHeight = new Point(this.Size.Width, this.Size.Height);
        }

        public FolderType SelectedFolderType
        {
            set { comboFolderType.SelectedItem = value; }
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
        private void InitializeExistIClientUIPlugin()
        {
            _existClientPlugin = new Dictionary<FolderType, IFolderPropertiesUIPlugin>();
        }

        private void CreateFolderForm_Load(object sender, EventArgs e)
        {
            InitializeClientService();
            InitializeExistIClientUIPlugin();
            InitializeBaseControls();
            InitializeBaseSizeHeight();
        }


        private void CreateFolder(object sender, EventArgs e)
        {
            IFolderPropertiesUIPlugin clientUIPlugin = ((PluginService)(ServiceProvider.GetService(typeof(PluginService)))).GetFolderPropetiesPlugin(SelectedFolderType);
            global::Registration.Model.FolderProperties folderProp = clientUIPlugin.Info;

            StringBuilder data = new StringBuilder();
            if (null != folderProp)
            {
                foreach (KeyValuePair<string, string> info in folderProp.Properties)
                {
                    data.Append(info);
                }
            }

            ClientRequests.CreateFolder(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id, FolderName, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id, SelectedFolderType.Id, data.ToString());
        }

        void FolderTypeIsChange(object sender, EventArgs e)
        {         
            FolderType selectedFolderType = SelectedFolderType;

            IFolderPropertiesUIPlugin clientUIPlugin = ((PluginService)(ServiceProvider.GetService(typeof(PluginService)))).GetFolderPropetiesPlugin(SelectedFolderType);

            var allWorkersInfo = new FolderProperties();
            var allWorkers = ClientRequests.GetAllWorkers();

            foreach (string info in allWorkers)
            {
                allWorkersInfo.Properties.Add(info, info);
            }

            clientUIPlugin.Info = allWorkersInfo;
            Control newControl = (Control)clientUIPlugin;

            int locationNewControlY = 0;

            foreach (Control control in BaseControls)
            {
                locationNewControlY = Math.Max(control.Location.Y + control.Size.Height, locationNewControlY);
            }

            newControl.Location = new Point(0, locationNewControlY);

            _newButtonsControl.Location = new Point(0, locationNewControlY + newControl.Size.Height);

            int width = Math.Max(Math.Max(BaseSizeHeight.X, newControl.Width), _newButtonsControl.Size.Width);

            this.Size = new Size(width, BaseSizeHeight.Y + newControl.Size.Height + _newButtonsControl.Size.Height);

            this.Controls.Clear();
            this.Controls.Add(newControl);
            this.Controls.Add(_newButtonsControl);

            foreach(Control control in BaseControls)
            {
                this.Controls.Add(control);
            }

            SelectedFolderType = selectedFolderType;
        }
    }
}
