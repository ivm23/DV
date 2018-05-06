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

namespace Registration.WPF.Views
{
    /// <summary>
    /// Interaction logic for AddWorkersFromAllWorkersListWindow.xaml
    /// </summary>
    public partial class AddWorkersFromAllWorkersListWindow : Window
    {
        private ViewModels.AddWorkersFromAllWorkersListViewModel _addWorkersFromAllWorkersListViewModel;
        public AddWorkersFromAllWorkersListWindow(IEnumerable<string> selectedWorkers, IEnumerable<string> nonSelectedWorkers)
        {
            InitializeComponent();
            _addWorkersFromAllWorkersListViewModel = new ViewModels.AddWorkersFromAllWorkersListViewModel(selectedWorkers, nonSelectedWorkers);
            DataContext = _addWorkersFromAllWorkersListViewModel;
        }

        void createButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        
        public IEnumerable<string> GetSelectedWorkers()
        {
            return _addWorkersFromAllWorkersListViewModel.SelectedWorkers;
        }
    }
}
