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

        private LetterView _letterView = new LetterView();
        private ImportantLetterData _importantLetterData = new ImportantLetterData();
        public ImportantLetterControlViewModel(LetterView letterView)
        {
            if (null == letterView)
                throw new ArgumentNullException();

            StringSelectedImportanceDegree = letterView.ExtendedData;
        }


        private IDictionary<Model.ImportanceDegree, string> _importanceDegrees = new Dictionary<Model.ImportanceDegree, string>();

        public IDictionary<Model.ImportanceDegree, string> ImportanceDegrees
        {
            set
            {
                _importanceDegrees = value;
            }
            get
            {
                return _importanceDegrees;
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

        public void InitializeImportanceDegreeControl()
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
        }

        private Model.ImportanceDegree _selectedImportance;

        public string StringSelectedImportanceDegree
        {
            set
            {
                if (null != value)
                {
                    _importantLetterData = _dataSerializer.DeserializeData<ImportantLetterData>(value);

                    _selectedImportance = _importantLetterData.DegreeImportance;
                    NameSelectedDegree = Resource.ResourceManager.GetString(Convert.ToString((int)_importantLetterData.DegreeImportance));
                }
            }
            get
            {
                foreach(KeyValuePair<Model.ImportanceDegree, string> pair in ImportanceDegrees)
                {
                    if (pair.Value == NameSelectedDegree)
                    {
                        _importantLetterData.DegreeImportance = pair.Key;
                    }
                }
               
                return _dataSerializer.SerializeData(_importantLetterData);
            }
        }

 
        private string _nameSelectedDegree;
        public string NameSelectedDegree
        {
            set
            {
                _nameSelectedDegree = value;
                OnPropertyChanged(nameof(NameSelectedDegree));
            }
            get
            {
                return _nameSelectedDegree;
            }
        }
    }

}


