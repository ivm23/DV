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
using Registration.Plugins;

namespace Registration.WinForms.Controlers
{
    public partial class LetterWithResponseTimeControl : UserControl, ILetterPropertiesUIPlugin
    {
        public event EventHandler AddedReceiver;

        private LetterView _letterView = new LetterView();
        private DataSerialization.IDataSerializationService _dataSerializer = DataSerialization.DataSerializationServiceFactory.InitializeDataSerializationService();
        private LetterWithReminderData _reminderLetterData = new LetterWithReminderData();


        public LetterWithResponseTimeControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException(nameof(serviceProvider));

            fullContentLetterControl1.OnLoad(serviceProvider);
        }

        public LetterView LetterView
        {
            set
            {
                fullContentLetterControl1.LetterView = value;

                _letterView = value;
                LetterWithReminderData reminderLetterData = _dataSerializer.DeserializeData<LetterWithReminderData>(_letterView.ExtendedData);
                dateTimePickerResponseRequired.Value = reminderLetterData.ReminderData;
            }
            get
            {
                _letterView = fullContentLetterControl1.LetterView;
                _reminderLetterData.ReminderData = dateTimePickerResponseRequired.Value;
                _letterView.ExtendedData = _dataSerializer.SerializeData(_reminderLetterData);
                return _letterView;
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

        private void LetterWithResponseTimeControl_Load(object sender, EventArgs e)
        {
            Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top);
        }
    }
}
