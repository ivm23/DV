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
    /// Interaction logic for FullContentLetterControl.xaml
    /// </summary>
    public partial class StandartLetterControl : UserControl, ILetterPropertiesUIPlugin
    {
        private ViewModels.StandartLetterControlViewModel standartLetterControlViewModel;
        public StandartLetterControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            var let = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
            standartLetterControlViewModel = new ViewModels.StandartLetterControlViewModel(let);

            DataContext = standartLetterControlViewModel;
        }

        public bool ReadOnly
        {
            set
            {
                standartLetterControlViewModel.ReadOnly = value;
            }
            get
            {
                return standartLetterControlViewModel.ReadOnly;
            }
        }

        public LetterView LetterView
        {
            set
            {
                standartLetterControlViewModel.Title = value.Name;
                standartLetterControlViewModel.Text = value.Text;
                standartLetterControlViewModel.Date = value.Date;
                standartLetterControlViewModel.SenderName = value.SenderName;
            }
            get
            {
                return new LetterView() {
                    Name = standartLetterControlViewModel.Title,
                    Text = standartLetterControlViewModel.Text,
                    Date = standartLetterControlViewModel.Date,
                    SenderName = standartLetterControlViewModel.SenderName
                };

            }
        }
    }
}

