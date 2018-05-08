using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;

namespace Registration.WPF.ViewModels
{
    class LetterWithResponseTimeControlViewModel : Notifier
    {

        private DataSerialization.IDataSerializationService _dataSerializer = DataSerialization.DataSerializationServiceFactory.InitializeDataSerializationService();
        private LetterView _letterView = new LetterView();
        public LetterWithResponseTimeControlViewModel(string ExtendedData)
        {
            if (null != ExtendedData)
                LetterWithReminderData = ExtendedData;
        }


        private DateTime _reminderLetterData;
        public DateTime ReminderLetterData
        {
            set
            {
                _reminderLetterData = value;
                OnPropertyChanged(nameof(ReminderLetterData));
            }
            get
            {
                return _reminderLetterData;
            }
        }

        private LetterWithReminderData _letterWithReminderDate = new LetterWithReminderData();
        public string LetterWithReminderData
        {
            set
            {
                _letterWithReminderDate = _dataSerializer.DeserializeData<LetterWithReminderData>(value);
                ReminderLetterData = _letterWithReminderDate.ReminderData;
            }
            get
            {
                _letterWithReminderDate.ReminderData = ReminderLetterData;
                return _dataSerializer.SerializeData(_letterWithReminderDate);
            }
        }

        private bool _readOnly;
        private bool _enable;
        public bool ReadOnly
        {
            set
            {
                _readOnly = value;
                Enable = !value;
                OnPropertyChanged(nameof(ReadOnly));
            }
            get
            {
                return _readOnly;
            }
        }

        public bool Enable
        {
            set
            {
                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
            get
            {
                return _enable;
            }
        }
    }
}
