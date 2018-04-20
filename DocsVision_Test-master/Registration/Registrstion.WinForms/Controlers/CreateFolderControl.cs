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
using Registration.ClientInterface;

namespace Registration.WinForms.Controlers
{
    public partial class CreateFolderControl : UserControl
    {
        public event EventHandler ChangedFolderType;

        private IServiceProvider _servieProvider;
        private IEnumerable<FolderType> _folderTypes;

        const string DisplayMember = "Name";
        const string ValueMember = "Id";
        const string WorkersName = "workersName";

        public CreateFolderControl()
        {
            InitializeComponent();
            this.comboFolderType.DropDownClosed += new EventHandler(comboFolderType_DropDownClosed);

        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return _servieProvider;
            }
        }

        public void InitializeFolderTypes(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _servieProvider = serviceProvider;

            _folderTypes = ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).GetAllFolderTypes();

            comboFolderType.Items.Clear();
            comboFolderType.DataSource = _folderTypes;
            comboFolderType.DisplayMember = DisplayMember;
            comboFolderType.ValueMember = ValueMember;
        }

        public FolderType FolderType
        {
            set
            {
                comboFolderType.SelectedItem = value;
            }
            get
            {
                return (FolderType)comboFolderType.SelectedItem;
            }
        }

        public string NameF
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

        private void comboFolderType_DropDownClosed(object sender, EventArgs e)
        {
                ChangedFolderType(sender, e);
        }

    }
}
