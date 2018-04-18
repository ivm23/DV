using System;
using System.Collections.Generic;
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


namespace Registration.WinForms.Forms
{
    public partial class RenameFolderForm : Form
    {
        private IClientRequests _clientRequests;
        private readonly IServiceProvider _serviceProvider;

        public RenameFolderForm(IServiceProvider provider)
        {
            InitializeComponent();
            _serviceProvider = provider;
        }

        private IServiceProvider ServiceProvider => _serviceProvider;

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private void InitializeClientService()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private void RenameFolderForm_Load(object sender, EventArgs e)
        {
            InitializeClientService();
            newNameTB.Text = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Name; 
        }

        private void saveB_Click(object sender, EventArgs e)
        {
            Folder folder = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder;
            ClientRequests.UpdateFolder(folder.Id, newNameTB.Text);
            Close();
        }
    }
}
