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
using Registration.Model;

namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for LetterWithResponseTimeControl.xaml
    /// </summary>
    public partial class LetterWithResponseTimeControl : UserControl, ILetterPropertiesUIPlugin
    {
        private ViewModels.LetterWithResponseTimeControlViewModel _letterWithResponseTimeViewModel;
        private LetterView _letterView = new LetterView();
        private LetterWithReminderData _reminderLetterData = new LetterWithReminderData();

        private DataSerialization.IDataSerializationService _dataSerializer = DataSerialization.DataSerializationServiceFactory.InitializeDataSerializationService();

        public LetterWithResponseTimeControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            _letterWithResponseTimeViewModel = new ViewModels.LetterWithResponseTimeControlViewModel();

            fullContentLetterControl.OnLoad(serviceProvider);

            _letterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;

            DataContext = _letterWithResponseTimeViewModel;

         //   LetterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
        }

        public LetterView LetterView
        {
            set
            {
                fullContentLetterControl.LetterView = value;
                _reminderLetterData = _dataSerializer.DeserializeData<LetterWithReminderData>(value.ExtendedData);
                _letterWithResponseTimeViewModel.ReminderLetterData = _reminderLetterData.ReminderData;               
            }
            get
            {
                _letterView = fullContentLetterControl.LetterView;
                _reminderLetterData.ReminderData = _letterWithResponseTimeViewModel.ReminderLetterData;
                _letterView.ExtendedData = _dataSerializer.SerializeData(_reminderLetterData);
                return _letterView;
            }
        }
        public bool ReadOnly
        {
            set
            {
                fullContentLetterControl.ReadOnly = value;
                _letterWithResponseTimeViewModel.ReadOnly = value;
            }
            get
            {
                return fullContentLetterControl.ReadOnly;
            }
        }
    }
}
