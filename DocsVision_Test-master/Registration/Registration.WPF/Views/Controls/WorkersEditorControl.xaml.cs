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

namespace Registration.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for WorkersEditorControl.xaml
    /// </summary>
    public partial class WorkersEditorControl : UserControl
    {
        private ViewModels.WorkersEditorControlViewModel _workersEditorControlViewModel;
        public WorkersEditorControl()
        {
            InitializeComponent();
        }

        public void InitializeWorkersEditorControl(IEnumerable<string> allWorkers)
        {
            _workersEditorControlViewModel = new ViewModels.WorkersEditorControlViewModel(allWorkers);

            DataContext = _workersEditorControlViewModel;
        }

        public bool ReadOnly
        {
            set
            {
                txtWorkers.IsReadOnly = value;
                btnAllWorkers.Visibility = (value ? Visibility.Collapsed : Visibility.Visible);
            }

            get { return txtWorkers.IsReadOnly; }
        }

        public IEnumerable<string> NamesWorkers
        {
            set { _workersEditorControlViewModel.NamesWorkers = value; }
            get { return _workersEditorControlViewModel.NamesWorkers; }
        }

        public void FocusToListOfWorker(object sender, KeyEventArgs e)
        {
            if (Key.Down == e.Key)
            {
                listWorkers.Focus();
            }
        }

        public void FocusToNamesReceiversString(object sender, KeyEventArgs e)
        {
            if (Key.Up != e.Key && Key.Down != e.Key && Key.Enter != e.Key)
            {
                txtWorkers.Focus();
                txtWorkers.SelectionStart = txtWorkers.Text.Length;
            }
        }

        private void FocusToTextBox(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtWorkers.SelectionStart = txtWorkers.Text.Length;
            txtWorkers.Focus();
        }
    }
}
