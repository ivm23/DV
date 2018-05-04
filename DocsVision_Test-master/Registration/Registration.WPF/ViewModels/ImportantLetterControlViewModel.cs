using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;

namespace Registration.WPF.ViewModels
{
    class ImportantLetterControlViewModel : Notifier
    {

        private DataSerialization.IDataSerializationService _dataSerializer = DataSerialization.DataSerializationServiceFactory.InitializeDataSerializationService();

        private LetterView _letterView;
        public ImportantLetterControlViewModel(LetterView letterView)
        {
            if (null == letterView)
                throw new ArgumentNullException();

            Title = letterView.Name;
            Text = letterView.Text;
            SenderName = letterView.SenderName;
            Date = letterView.Date;
        }
        public string Title
        {
            set
            {
                _letterView.Name = value;
                OnPropertyChanged(nameof(Title));
            }
            get
            {
                return _letterView.Name;
            }
        }
        public string Text
        {
            set
            {
                _letterView.Text = value;
                OnPropertyChanged(nameof(Text));
            }
            get
            {
                return _letterView.Text;
            }
        }

        public string SenderName
        {
            set
            {
                _letterView.SenderName = value;
                OnPropertyChanged(nameof(SenderName));
            }
            get
            {
                return _letterView.SenderName;
            }
        }

        public DateTime Date
        {
            set
            {
                _letterView.Date = value;
                OnPropertyChanged(nameof(Date));
            }
            get
            {
                return _letterView.Date;
            }
        }

        public LetterView LetterView
        {
            set
            {
                _letterView = value;
                ImportantLetterData importantLetterData = _dataSerializer.DeserializeData<ImportantLetterData>(_letterView.ExtendedData);

                SelectedImportanceDegree = importantLetterData.DegreeImportance.ToString();

                OnPropertyChanged(nameof(LetterView));
            }
            get
            {
                return _letterView;
            }
        }

        private bool _readOnly;

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

        private IDictionary<Model.ImportanceDegree, string> _importanceDegrees = new Dictionary<Model.ImportanceDegree, string>();

        public IDictionary<Model.ImportanceDegree, string> ImportanceDegrees
        {
            set
            {
                _importanceDegrees = value;
              
                OnPropertyChanged(nameof(ImportanceDegrees));
            }
            get
            {
                return _importanceDegrees;
            }
        }

        private void InitializeImportanceDegreeControl()
        {
            IDictionary<Model.ImportanceDegree, string> importanceDegrees = new Dictionary<Model.ImportanceDegree, string>();
            foreach (int value in Enum.GetValues(typeof(Model.ImportanceDegree)))
            {
                string importanceDegreeStringValue = (string)Resource.ResourceManager.GetObject(value.ToString());

                Model.ImportanceDegree importanceDegreeEnumValue;
                Enum.TryParse(importanceDegreeStringValue, out importanceDegreeEnumValue);
                importanceDegrees.Add(importanceDegreeEnumValue, importanceDegreeStringValue);
            }
            ImportanceDegrees = importanceDegrees;

            CurrentImportantLetterControlViewModel = this;
        }

        private ImportantLetterControlViewModel _currentImportantLetterControlViewModel;
        public ImportantLetterControlViewModel CurrentImportantLetterControlViewModel
        {
            set
            {
                _currentImportantLetterControlViewModel = value;
                OnPropertyChanged(nameof(CurrentImportantLetterControlViewModel));
            }
            get { return _currentImportantLetterControlViewModel; }
        }

        private string _selectedImportance;
        public string SelectedImportanceDegree
        {
            set
            {
                _selectedImportance = value;
                OnPropertyChanged(nameof(SelectedImportanceDegree));
            }
            get
            {
                return _selectedImportance;
            }
        }

    }
}
