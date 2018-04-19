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
            txtWorkers.PreviewKeyDown += new PreviewKeyDownEventHandler(comboWorkers_KeyPress);
            listBoxWorkers.SelectedIndexChanged += new EventHandler(comboWorkers_SelectedIndexChanged);
            txtWorkers.TextChanged += new EventHandler(txtWorkers_TextChanged);
            listBoxWorkers.LostFocus += new EventHandler(comboWorkers_LostFocus);
        }

        public bool ReadOnly
        {
            set
            {
                txtWorkers.ReadOnly = value;
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
                if (ReadOnly)
                {
                    StringBuilder workersString = new StringBuilder();
                    foreach (string worker in value)
                    {
                        workersString.Append(worker).Append(SplitMarker).Append(" ");
                    }
                    txtWorkers.Text = workersString.ToString();
                }
            }
            get
            {
                IEnumerable<string> workers = new List<string>();
                workers = txtWorkers.Text.Trim().Split(SplitMarker);
                return workers.AsQueryable().Where(str => !string.IsNullOrEmpty(str));
            }
        }

        public string GetSelectedWorker()
        {
            if (null == listBoxWorkers.SelectedItem)
                throw new Exception("Worker isn't select");

            return listBoxWorkers.SelectedItem.ToString();
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

        void txtWorkers_TextChanged(object sender, EventArgs e)
        {
            listBoxWorkers.Items.Clear();
            if (txtWorkers.Text.Length == 0)
            {
                hideResults();
                return;
            }

            int _currentEndOfNamesString = findPrevEndOfNameString(txtWorkers.Text);

            foreach (string s in txtWorkers.AutoCompleteCustomSource)
            {
                if (s.Contains(txtWorkers.Text.Substring(_currentEndOfNamesString).Trim()))
                {
                    listBoxWorkers.Items.Add(s);
                    listBoxWorkers.Visible = true;
                }
            }
        }

        void comboWorkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _currentEndOfNamesString = findPrevEndOfNameString(txtWorkers.Text);

            if (_currentEndOfNamesString != 0)
                ++_currentEndOfNamesString;

            txtWorkers.Text = txtWorkers.Text.Substring(0,_currentEndOfNamesString) + listBoxWorkers.Items[listBoxWorkers.SelectedIndex].ToString() + SplitMarker + " ";
            txtWorkers.SelectionStart = txtWorkers.Text.Length;

            hideResults();
        }

        void comboWorkers_LostFocus(object sender, EventArgs e)
        {
            hideResults();
        }

        void hideResults()
        {
            listBoxWorkers.Visible = false;
        }

        private int findPrevEndOfNameString(string namesString)
        {
            int index = namesString.LastIndexOf(SplitMarker) + 1;
            return (index < 0 || index > namesString.Length ? 0 : index);
        }

        private void comboWorkers_KeyPress(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string namesString = txtWorkers.Text;

                int _currentEndOfNamesString = findPrevEndOfNameString(namesString);
                bool isExist = false;

                foreach (string s in txtWorkers.AutoCompleteCustomSource)
                {
                    if (s.Contains(namesString.Substring(_currentEndOfNamesString).Trim()))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                    ((Message.IMessageService)ServiceProvider.GetService(typeof(Message.IMessageService))).ErrorMessage(Message.MessageResource.NonexistWorker);
                
                else
                {
                    txtWorkers.Text += SplitMarker + " ";
                    txtWorkers.SelectionStart = txtWorkers.Text.Length;
                }
            }
        }

    }
}

