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
    public partial class ImportanceDegreeEditorControl : UserControl
    {
        private IDictionary<Model.ImportanceDegree, string> _importanceDegree = new Dictionary<Model.ImportanceDegree, string>();

        public ImportanceDegreeEditorControl()
        {
            InitializeComponent();
        }

        private void InitializeImportanceDegreeControl()
        {
            foreach (int value in Enum.GetValues(typeof(Model.ImportanceDegree)))
            {
                string importanceDegreeStringValue = (string)Resources.ResourceManager.GetObject(value.ToString());

                Model.ImportanceDegree importanceDegreeEnumValue;
                Enum.TryParse(importanceDegreeStringValue, out importanceDegreeEnumValue);
                _importanceDegree.Add(importanceDegreeEnumValue, importanceDegreeStringValue);
            }

            comboImportanceDegree.DataSource = new BindingSource(_importanceDegree, null);
            comboImportanceDegree.DisplayMember = "Value";
            comboImportanceDegree.ValueMember = "Key";
        }

        public Model.ImportanceDegree SelectedImportanceDegree
        {
            set
            {
                comboImportanceDegree.SelectedValue = value;
            }
            get
            {
                return (Model.ImportanceDegree)comboImportanceDegree.SelectedValue;
            }
        }

        private void ImportanceDegreeEditorControl_Load_1(object sender, EventArgs e)
        {
            InitializeImportanceDegreeControl();
        }
    }
}
