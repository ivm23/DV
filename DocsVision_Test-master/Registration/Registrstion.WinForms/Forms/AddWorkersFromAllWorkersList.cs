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

namespace Registration.WinForms.Forms
{
    public partial class AddWorkersFromAllWorkersList : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private IEnumerable<string> _receivers = new List<string>();
        private IEnumerable<string> _otherWorkers = new List<string>();

        public AddWorkersFromAllWorkersList(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            InitializeComponent();
            _serviceProvider = serviceProvider;
            this.KeyDown += new KeyEventHandler(form_KeyDown);
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }
        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void InitializeAllWorkers()
        {
            listAllWorkers.Items.AddRange(((((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).GetAllWorkers()).Where(str => ! _receivers.Contains(str.Trim()))).ToArray());
        }

        private void InitializeCurrentReceivers()
        {
            _receivers = (((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentReceivers).Select(str => str.Trim()).Where(str => _otherWorkers.Contains(str));
            listReceivers.Items.AddRange(_receivers.ToArray());
        }

        private void InitializeAddWorkersForm()
        {
            _receivers = (((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentReceivers).Select(str => str.Trim());
            _otherWorkers = (((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).GetAllWorkers()).Select(str => str.Trim());

            InitializeCurrentReceivers();
            InitializeAllWorkers();
        }

        private void AddWorkersFromAllWorkersList_Load(object sender, EventArgs e)
        {
            InitializeAddWorkersForm();
        }

        private IEnumerable<string> getSelectedNames(ListBox listWorkers)
        {
            IList<string> selectedNames = new List<string>();
            foreach (string name in listWorkers.SelectedItems)
            {
                selectedNames.Add(name);
            }
            return selectedNames;
        }

        private bool deleteName(string nameForDelete, ListBox fromWorkersList)
        {
            if (fromWorkersList.Items.Contains(nameForDelete.Trim()))
            {
                fromWorkersList.Items.Remove(nameForDelete);
                return true;
            }
            return false;
        }

        private bool addName(string nameForAdd, ListBox fromWorkersList)
        {
            if (!fromWorkersList.Items.Contains(nameForAdd.Trim()))
            {
                fromWorkersList.Items.Add(nameForAdd);
                return true;
            }
            return false;
        }

        private void modifyNamesList(IEnumerable<string> names, ListBox listForModify, Func<string, ListBox, bool> typeModify)
        {
            foreach (string name in names)
            {
                typeModify(name, listForModify);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            IEnumerable<string> workersToModify = getSelectedNames(listAllWorkers);
            modifyNamesList(workersToModify, listReceivers, addName);
            modifyNamesList(workersToModify, listAllWorkers, deleteName);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            IEnumerable<string> workersToModify = getSelectedNames(listReceivers);
            modifyNamesList(workersToModify, listAllWorkers, addName);
            modifyNamesList(workersToModify, listReceivers, deleteName);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentReceivers = listReceivers.Items.Cast<string>();
        }
    }
}
