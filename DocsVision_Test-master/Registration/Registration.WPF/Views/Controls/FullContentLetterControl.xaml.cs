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
    /// Interaction logic for FullContentLetterControl.xaml
    /// </summary>
    public partial class FullContentLetterControl : UserControl, ILetterPropertiesUIPlugin
    {
        private ViewModels.FullContentLetterControlViewModel fullContentLetterControlViewModel;
        public FullContentLetterControl()
        {
            InitializeComponent();
        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            if (null == serviceProvider)
                throw new ArgumentNullException();

            fullContentLetterControlViewModel = new ViewModels.FullContentLetterControlViewModel(serviceProvider);

            DataContext = fullContentLetterControlViewModel;
        }

        public bool ReadOnly
        {
            set
            {
                fullContentLetterControlViewModel.ReadOnly = value;
            }
            get
            {
                return fullContentLetterControlViewModel.ReadOnly;
            }
        }

        public LetterView LetterView
        {
            set
            {
                fullContentLetterControlViewModel.LetterView = value;
            }
            get
            {
                return fullContentLetterControlViewModel.LetterView;
            }
        }

        public void InitializeLetterView()
        {
            fullContentLetterControlViewModel.InitializeLetterView();
        }

    }
}

