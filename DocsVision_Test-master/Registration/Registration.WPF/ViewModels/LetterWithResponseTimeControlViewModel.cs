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
        public LetterWithResponseTimeControlViewModel()
        {
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
