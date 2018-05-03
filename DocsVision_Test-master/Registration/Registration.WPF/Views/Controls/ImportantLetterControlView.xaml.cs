using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Registration.WinForms;
using Registration.Model;


namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для ImportantLetterControlView.xaml
    /// </summary>
    public partial class ImportantLetterControlView : UserControl, ILetterPropertiesUIPlugin
    {
        private DataSerialization.IDataSerializationService _dataSerializer = DataSerialization.DataSerializationServiceFactory.InitializeDataSerializationService();
        public ImportantLetterControlView()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            LetterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
            InitializeImportanceDegreeControl();
            CurrentImportantLetterControlView = this;
        }

        private LetterView _letterView;
        public LetterView LetterView
        {
            set
            {
                _letterView = value;
                ImportantLetterData importantLetterData = _dataSerializer.DeserializeData<ImportantLetterData>(_letterView.ExtendedData);

                SelectedImportanceDegree = importantLetterData.DegreeImportance.ToString();
            }
            get
            {
                return _letterView;
            }
        }

        private ImportantLetterControlView _currentImportantLetterControlView;

        public ImportantLetterControlView CurrentImportantLetterControlView
        {
            set
            {
                _currentImportantLetterControlView = value;
            }
            get
            {
                return _currentImportantLetterControlView;
            }
        }


        public event EventHandler AddedReceiver;

        private bool _readOnly;

        public bool ReadOnly
        {
            set
            {
                _readOnly = value;
                Enable = !value;    
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
        }


        private string _selectedImportance;
        public string SelectedImportanceDegree
        {
            set
            {
                _selectedImportance = value;
            }
            get
            {
                return _selectedImportance;
            }
        }

        private bool _enable;

        public bool Enable
        {
            set
            {
                _enable = value;
            }
            get
            {
                return _enable;
            }
        }
    }
}
