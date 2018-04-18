using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration.WinForms.Controlers
{
    public partial class WorkersEditorControl : UserControl
    {

        private IServiceProvider _serviceProvider;
        private StringBuilder _existNameString = new StringBuilder();
        private int _currentEndOfNamesString = 0;

        private IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        public WorkersEditorControl()
        {
            InitializeComponent();
            comboWorkers.KeyDown += new KeyEventHandler(comboWorkers_KeyPress);
        }

        public bool ReadOnly
        {
            set
            {
                txtWorkers.ReadOnly = value;
                txtWorkers.Visible = value;
                comboWorkers.Visible = !value;
            }
            get
            {
                return txtWorkers.ReadOnly;
            }
        }

        public IEnumerable<string> GetWorkers()
        {
            IEnumerable<string> workers = new List<string>();
            workers = txtWorkers.Text.Split(';');
            return workers;
        }

        public string GetSelectedWorker()
        {
            if (null == comboWorkers.SelectedItem)
                throw new Exception("Worker isn't select");

            return comboWorkers.SelectedItem.ToString();
        }

        public void SetWorkers(IEnumerable<string> workers)
        {
            if (ReadOnly)
            {
                StringBuilder workersString = new StringBuilder();
                foreach (string worker in workers)
                {
                    workersString.Append(worker).Append("; ");
                }

                txtWorkers.Text = workersString.ToString();
            }
        }

        public void InitializeAllWorkers(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            IEnumerable<string> workers = ((ClientInterface.IClientRequests)ServiceProvider.GetService(typeof(ClientInterface.IClientRequests))).GetAllWorkers();
            comboWorkers.Items.Clear();
            foreach (string worker in workers)
            {
                comboWorkers.Items.Add(worker);
            }
        }

        private void WorkersEditorControl_Load(object sender, EventArgs e)
        {
            comboWorkers.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboWorkers.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void comboWorkers_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = comboWorkers.Text.Substring(_currentEndOfNamesString);

                comboWorkers.ValueMember = newName;

                if (!comboWorkers.Items.Contains(newName))
                {
                    MessageBox.Show(newName + "SuchWorker isn't exist");
                    comboWorkers.Text = comboWorkers.Text.Substring(0, _currentEndOfNamesString);
                }
                    _currentEndOfNamesString = comboWorkers.Text.Length;
                comboWorkers.Text += "; ";
            }
        }
    }

}

