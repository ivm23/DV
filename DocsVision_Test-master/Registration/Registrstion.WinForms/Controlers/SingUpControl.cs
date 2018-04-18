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
    internal partial class SingUpControl : UserControl
    {
        public SingUpControl()
        {
            InitializeComponent();
        }
        public string NameW
        {
            set { nameWorkerTB.Text = value; }
            get { return nameWorkerTB.Text; }
        }
        public string Login
        {
            set { loginWorkerTB.Text = value; }
            get { return loginWorkerTB.Text; }
        }
        public string Password
        {
            set { passwordWorkerTB.Text = value; }
            get { return passwordWorkerTB.Text; }
        }

        public IEnumerable<string> DatabaseNames
        {
            set
            {
                databaseNamesCB.Items.Clear();
                foreach (string databaseName in value)
                {
                    databaseNamesCB.Items.Add(databaseName);
                }                
            }
            get
            {
                List<string> databaseNames = new List<string>();
                foreach(var item in databaseNamesCB.Items)
                {
                    databaseNames.Add(item.ToString());
                }
                return databaseNames;
            }
          
        }

        public string SelectDatabaseName
        {
            get
            {
                return databaseNamesCB.SelectedItem.ToString();
            }
            set
            {
                databaseNamesCB.SelectedItem = value;
            }
        }

    }
}
