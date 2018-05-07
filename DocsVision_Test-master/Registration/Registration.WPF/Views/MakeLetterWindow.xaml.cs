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
using Registration.ClientInterface;

namespace Registration.WPF.Views
{
    /// <summary>
    /// Interaction logic for MakeLetterWindow.xaml
    /// </summary>
    public partial class MakeLetterWindow : Window
    {

        ViewModels.MakeLetterViewModel _makeLetterViewModel;

        public MakeLetterWindow(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            InitializeComponent();

            _makeLetterViewModel = new ViewModels.MakeLetterViewModel(provider);
            DataContext = _makeLetterViewModel;
            _makeLetterViewModel.InitializeLetterPlugin();

            stackPanel.Children.Add((Control)(_makeLetterViewModel.LetterPlugin));
        }
    }
}
