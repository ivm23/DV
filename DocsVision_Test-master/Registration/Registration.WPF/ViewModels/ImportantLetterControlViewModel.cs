using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;
using Registration.WinForms;

namespace Registration.WPF.ViewModels
{
    class ImportantLetterControlViewModel : ViewModelBase
    {

        private DataSerialization.IDataSerializationService _dataSerializer = DataSerialization.DataSerializationServiceFactory.InitializeDataSerializationService();

        public ImportantLetterControlViewModel()
        {

        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            LetterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
            InitializeImportanceDegreeControl();
            
        }

        private LetterView _letterView;
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


        public event EventHandler AddedReceiver;

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
