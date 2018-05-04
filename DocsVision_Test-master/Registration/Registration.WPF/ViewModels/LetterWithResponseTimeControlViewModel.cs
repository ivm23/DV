using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;

namespace Registration.WPF.ViewModels
{
    class LetterWithResponseTimeControlViewModel : ViewModelBase//, ILetterPropertiesUIPlugin
    {

        private DataSerialization.IDataSerializationService _dataSerializer = DataSerialization.DataSerializationServiceFactory.InitializeDataSerializationService();

        public LetterWithResponseTimeControlViewModel()
        {

        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            LetterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
        }

        private LetterView _letterView;
        public LetterView LetterView
        {
            set
            {
                _letterView = value;
                OnPropertyChanged(nameof(LetterView));
                ReminderLetterData = (_dataSerializer.DeserializeData<LetterWithReminderData>(value.ExtendedData)).ReminderData.ToString();
            }
            get
            {
                return _letterView;
            }
        }

        private string _reminderLetterData;
        public string ReminderLetterData
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

        public event EventHandler AddedReceiver;

        private bool _readOnly;
        private bool _enable;
        public bool ReadOnly
        {
            set
            {
                _readOnly = value;
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
