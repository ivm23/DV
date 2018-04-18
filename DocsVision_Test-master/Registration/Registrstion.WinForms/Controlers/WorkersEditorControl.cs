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
        public WorkersEditorControl()
        {
            InitializeComponent();
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
            IEnumerable<string> workers = ((ClientInterface.IClientRequests)serviceProvider.GetService(typeof(ClientInterface.IClientRequests))).GetAllWorkers();
            comboWorkers.Items.Clear();
            foreach (string worker in workers)
            {
                comboWorkers.Items.Add(worker);
            }
        }
    }

}

