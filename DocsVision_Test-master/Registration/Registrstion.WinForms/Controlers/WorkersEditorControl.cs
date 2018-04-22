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
        private const char SplitMarker = ';';

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
            txtWorkers.PreviewKeyDown += new PreviewKeyDownEventHandler(txtWorkers_KeyPress);
            listBoxWorkers.MouseDown += new MouseEventHandler(listBoxWorkers_MouseDown);
            txtWorkers.TextChanged += new EventHandler(txtWorkers_TextChanged);
            listBoxWorkers.LostFocus += new EventHandler(listBoxWorkers_LostFocus);

            listBoxWorkers.PreviewKeyDown += new PreviewKeyDownEventHandler(listBoxWorkers_PreviewKeyDown);
        }

        public bool ReadOnly
        {
            set
            {
                txtWorkers.ReadOnly = value;
                buttonAllWorkers.Visible = !value;
            }
            get
            {
                return txtWorkers.ReadOnly;
            }
        }

        public IEnumerable<string> NamesWorkers
        {
            set
            {
                    StringBuilder workersString = new StringBuilder();
                    foreach (string worker in value)
                    {
                        workersString.Append(worker).Append(SplitMarker).Append(" ");
                    }
                    txtWorkers.Text = workersString.ToString();
            }
            get
            {
                IEnumerable<string> workers = new List<string>();
                workers = txtWorkers.Text.Trim().Split(SplitMarker);
                return workers.AsQueryable().Where(str => !string.IsNullOrEmpty(str));
            }
        }

        public void InitializeAllWorkers(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _serviceProvider = serviceProvider;

            var acsc = new AutoCompleteStringCollection();
            IEnumerable<string> workers = ((ClientInterface.IClientRequests)ServiceProvider.GetService(typeof(ClientInterface.IClientRequests))).GetAllWorkers();

            if (null != workers)
                acsc.AddRange(workers.ToArray());

            txtWorkers.AutoCompleteCustomSource = acsc;
        }

        private void WorkersEditorControl_Load(object sender, EventArgs e)
        {
            txtWorkers.Visible = true;
            txtWorkers.AutoCompleteMode = AutoCompleteMode.None;
            txtWorkers.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void fillSuitableWorkersList()
        {
            hideResults();

            listBoxWorkers.Items.Clear();
            if (txtWorkers.Text.Length == 0)
            {
                hideResults();
                return;
            }

            int _currentEndOfNamesString = findPrevEndOfNameString(txtWorkers.Text);
            bool existWorker = false;
            foreach (string s in txtWorkers.AutoCompleteCustomSource)
            {
                if (s.Contains(txtWorkers.Text.Substring(_currentEndOfNamesString).Trim()))
                {
                    listBoxWorkers.Items.Add(s);
                    existWorker = true;
                    
                }
            }
            if (existWorker)
            {
                listBoxWorkers.Visible = true;
                this.BringToFront();
            }
        }

        void txtWorkers_TextChanged(object sender, EventArgs e)
        {
            fillSuitableWorkersList();
        }

        private void addWorkerNameFromProposedList()
        {
            int _currentEndOfNamesString = findPrevEndOfNameString(txtWorkers.Text);

            if (_currentEndOfNamesString != 0)
                ++_currentEndOfNamesString;

            string newName = listBoxWorkers.Items[listBoxWorkers.SelectedIndex].ToString();

            if (!txtWorkers.Text.Contains(newName.Trim()))
            {
                txtWorkers.Text = txtWorkers.Text.Substring(0, _currentEndOfNamesString) + listBoxWorkers.Items[listBoxWorkers.SelectedIndex].ToString() + SplitMarker + " ";
                txtWorkers.SelectionStart = txtWorkers.Text.Length;
            }
            hideResults();
        }

        private void addWorkerNameFromWorker()
        {
            string namesString = txtWorkers.Text;

            int _currentEndOfNamesString = findPrevEndOfNameString(namesString);
            bool isExist = false;

            foreach (string s in txtWorkers.AutoCompleteCustomSource)
            {
                if (s.Trim().Equals(namesString.Substring(_currentEndOfNamesString).Trim()))
                {
                    isExist = true;
                    break;
                }
            }

            if (isExist)
            {
                txtWorkers.Text += SplitMarker + " ";
                txtWorkers.SelectionStart = txtWorkers.Text.Length;
            }
        }

        void listBoxWorkers_LostFocus(object sender, EventArgs e)
        {
            hideResults();
        }

        void hideResults()
        {
            listBoxWorkers.Visible = false;
            this.SendToBack();
        }

        private int findPrevEndOfNameString(string namesString)
        {
            int index = namesString.LastIndexOf(SplitMarker) + 1;
            return (index < 0 || index > namesString.Length ? 0 : index);
        }

        private void txtWorkers_KeyPress(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addWorkerNameFromWorker();
            }
            else
                if (e.KeyData == Keys.Down || e.KeyData == Keys.Up)
                listBoxWorkers.Focus();
        }

        private void listBoxWorkers_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int index = listBoxWorkers.SelectedIndex;

                if (0 <= index && index < listBoxWorkers.Items.Count)
                {
                    addWorkerNameFromProposedList();
                    txtWorkers.Focus();
                }
            }
            else
            if (e.KeyData != Keys.Down && e.KeyData != Keys.Up)
                txtWorkers.Focus();
        }

        private void listBoxWorkers_MouseDown(object sender, EventArgs e)
        {
            int index = listBoxWorkers.SelectedIndex;

            if (0 <= index && index < listBoxWorkers.Items.Count)
                addWorkerNameFromProposedList();
        }

        private void putCurrentReceivers()
        {
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentReceivers = NamesWorkers;
        }

        private void buttonAllWorkers_Click(object sender, EventArgs e)
        {
            putCurrentReceivers();

            using (var addWorkersForm = new Forms.AddWorkersFromAllWorkersList(ServiceProvider))
            {
                if (addWorkersForm.ShowDialog() == DialogResult.OK)
                {
                    NamesWorkers = (((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentReceivers).Select(str=>str.Trim());
                }
            }

            hideResults();
            txtWorkers.Focus();
            txtWorkers.SelectionStart = txtWorkers.Text.Length;
        }
    }
}

