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

        private LetterProperties _letterProperties = new LetterProperties();
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

        public LetterProperties LetterExtendedProperties
        {
            set
            {
                _letterProperties = value;
                Model.ImportantLetterData importantLetterData = _dataSerializer.DeserializeData(value.ExtendedProperty);

                importanceDegreeEditorControl1.ImportanceDegree = importantLetterData.DegreeImportance;
            }
            get
            {
                var importantLetterData = new ImportantLetterData();

                importantLetterData.DegreeImportance = importanceDegreeEditorControl1.ImportanceDegree;

                _letterProperties.ExtendedProperty = _dataSerializer.SerializeData(importantLetterData);

                return _letterProperties;
            }    
        }


        public LetterView StandartLetter
        {
            set
            {
                fullContentLetterControl1.StandartLetter = value;
            }
            get
            {
                return fullContentLetterControl1.StandartLetter;
            }
        }

        public LetterView LetterView
        {
            set
            {
                StandartLetter = value;
                _letterProperties.ExtendedProperty = value.ExtendedData;
                LetterExtendedProperties = _letterProperties;
            }
            get
            {
                _letterView = StandartLetter;
                _letterView.ExtendedData = LetterExtendedProperties.ExtendedProperty;
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

        private void importanceDegreeEditorControl1_Load(object sender, EventArgs e)
        {

        }

        private void fullContentLetterControl1_Load(object sender, EventArgs e)
        {
        }

    }
}
