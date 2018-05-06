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

        public void InitializeWorkersEditorControl(IEnumerable<string> allWorkers )
        {
            _workersEditorControlViewModel = new ViewModels.WorkersEditorControlViewModel(allWorkers);

            DataContext = _workersEditorControlViewModel;
            //    _workersEditorControlViewModel.NamesWorkers = allWorkers();
        }
        public bool ReadOnly
        {
            set
            {
                txtWorkers.IsReadOnly = value;
               //listWorkers.Visibility = (value ? Visibility.Hidden : Visibility.Visible);
                btnAllWorkers.Visibility = (value ? Visibility.Hidden : Visibility.Visible);
            }

            get
            {
                return txtWorkers.IsReadOnly;
            }
        }
        private bool _enable;
        public bool Enable
        {
            set
            {
                listWorkers.Visibility = (value ? Visibility.Hidden : Visibility.Visible);
                _enable = value;
            }
            get
            {
                return _enable;
            }
        }

        public IEnumerable<string> NamesWorkers
        {
            set
            {
                _workersEditorControlViewModel.NamesWorkers = value;
            }
            get
            {
                return _workersEditorControlViewModel.NamesWorkers;
            }
        }
    }
}
