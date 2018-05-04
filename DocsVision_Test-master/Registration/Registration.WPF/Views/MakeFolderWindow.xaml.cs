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
    /// Interaction logic for MakeFolderWindow.xaml
    /// </summary>
    public partial class MakeFolderWindow : Window
    {
        ViewModels.MakeFolderViewModel _makeFolderViewModel;
        public MakeFolderWindow(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            InitializeComponent();

            _makeFolderViewModel = new ViewModels.MakeFolderViewModel(provider);
            DataContext = _makeFolderViewModel;

            _makeFolderViewModel.InitializeFolderPlugin();
            stackPanel.Children.Add((Control)(_makeFolderViewModel.FolderPlugin));
        }
    }
}
