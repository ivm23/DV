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
using Registration.Logger;

namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for LetterWithResponseTimeControl.xaml
    /// </summary>
    public partial class LetterWithResponseTimeControl : UserControl, ILetterPropertiesUIPlugin
    {
        private ViewModels.LetterWithResponseTimeControlViewModel _letterWithResponseTimeViewModel;
        private LetterView _letterView = new LetterView();

        public LetterWithResponseTimeControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            try
            {
                fullContentLetterControl.OnLoad(serviceProvider);

                _letterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
                _letterWithResponseTimeViewModel = new ViewModels.LetterWithResponseTimeControlViewModel(_letterView.ExtendedData);

                DataContext = _letterWithResponseTimeViewModel;
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString()); 
            }
        }

        public LetterView LetterView
        {
            set
            {
                fullContentLetterControl.LetterView = value;
                _letterWithResponseTimeViewModel.LetterWithReminderData = value.ExtendedData;
            }
            get
            {
                _letterView = fullContentLetterControl.LetterView;
                _letterView.ExtendedData = _letterWithResponseTimeViewModel.LetterWithReminderData;
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
            get { return fullContentLetterControl.ReadOnly; }
        }
    }

}
