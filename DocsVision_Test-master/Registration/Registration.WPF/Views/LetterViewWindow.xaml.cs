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
using System.Windows.Shapes;
using Registration.Model;
using Registration.ClientInterface;

namespace Registration.WPF.Views
{
    /// <summary>
    /// Interaction logic for FullContentLetterWindow.xaml
    /// </summary>
    public partial class LetterViewWindow : Window
    {
        ViewModels.LetterViewModel _fullContentLetterViewModel;
        public LetterViewWindow(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            InitializeComponent();

            _fullContentLetterViewModel = new ViewModels.LetterViewModel(provider);
            DataContext = _fullContentLetterViewModel;
            _fullContentLetterViewModel.InitializeLetterPlugin();
            stackPanel.Children.Add((Control)(_fullContentLetterViewModel.LetterPlugin));
        }
    }
}
