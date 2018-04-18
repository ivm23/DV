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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Registration.WinForms.Controlers
{
    public partial class LetterWithResponseTimeControl : UserControl, ILetterPropertiesUIPlugin
    {
        public event EventHandler AddedReceiver;

        private LetterProperties _letterProperties = new LetterProperties();
        private LetterView _letterView = new LetterView();
        private DataSerialization.IDataSerializationService<LetterWithReminderData> _dataSerializer = DataSerialization.DataSerializationServiceFactory<LetterWithReminderData>.InitializeDataSerializationService();


        public LetterWithResponseTimeControl()
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
                LetterWithReminderData reminderLetterData = _dataSerializer.DeserializeData(value.ExtendedProperty);
                dateTimePickerResponseRequired.Value = reminderLetterData.ReminderData;
            }
            get
            {
                var reminderLetterData = new LetterWithReminderData { ReminderData = dateTimePickerResponseRequired.Value };

                _letterProperties.ExtendedProperty = _dataSerializer.SerializeData(reminderLetterData);

                return _letterProperties;
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

        public bool ReadOnly
        {
            set
            {
                fullContentLetterControl1.ReadOnly = value;
                dateTimePickerResponseRequired.Enabled = !value;
            }
            get
            {
                return fullContentLetterControl1.ReadOnly;
            }
        }
    }
}
