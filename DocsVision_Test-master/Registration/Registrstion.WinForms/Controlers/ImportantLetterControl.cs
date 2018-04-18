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
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Configuration;

namespace Registration.WinForms.Controlers
{
    public partial class ImportantLetterControl : UserControl, ILetterPropertiesUIPlugin
    {
        public event EventHandler AddedReceiver;

        private LetterView _letterView = new LetterView();

        // private  DataSerialization.IDataSerializationService<ImportantLetterData> _dataSerializer = DataSerialization.DataSerializationServiceFactory<ImportantLetterData>.InitializeDataSerializationService();

        private DataSerialization.DataSerializationXML<ImportantLetterData> _dataSerializer = new DataSerialization.DataSerializationXML<ImportantLetterData>();

        public ImportantLetterControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            fullContentLetterControl1.OnLoad(serviceProvider);
        }


        public LetterView LetterView
        {
            set
            {
                fullContentLetterControl1.LetterView = value;

                _letterView.ExtendedData = value.ExtendedData;

                Model.ImportantLetterData importantLetterData = _dataSerializer.DeserializeData(_letterView.ExtendedData);

                importanceDegreeEditorControl1.ImportanceDegree = importantLetterData.DegreeImportance;
            }
            get
            {
                _letterView = fullContentLetterControl1.LetterView;

                var importantLetterData = new ImportantLetterData();

                importantLetterData.DegreeImportance = importanceDegreeEditorControl1.ImportanceDegree;

                _letterView.ExtendedData = _dataSerializer.SerializeData(importantLetterData);

                return _letterView;
            }
        }

        public bool ReadOnly
        {
            set
            {
                fullContentLetterControl1.ReadOnly = value;
                importanceDegreeEditorControl1.Enabled = !value;
            }
            get
            {
                return fullContentLetterControl1.ReadOnly;
            }
        }

        private void ImportantLetterControl_Load(object sender, EventArgs e)
        {
            Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top);
        }
    }
}
