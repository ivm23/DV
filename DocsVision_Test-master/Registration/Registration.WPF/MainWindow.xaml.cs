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
using System.ComponentModel.Design;
using Registration.ClientInterface;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace Registration.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        private readonly MainWindowViewModel _mainWindowViewModel;

        private readonly IServiceContainer _serviceContainer = new ServiceContainer();
        private IServiceContainer ServiceContainer
        {
            get { return _serviceContainer; }
        }

        public MainWindow()
        {
            InitializeServiceContainer();

            _mainWindowViewModel = new MainWindowViewModel(_serviceContainer);

            DataContext = _mainWindowViewModel;

            InitializeComponent();
        }

        private void InitializeServiceContainer()
        {
            IClientRequests clientRequests = new ClientRequests();

            _serviceContainer.AddService(typeof(IClientRequests), clientRequests);
            _serviceContainer.AddService(typeof(PluginService), new PluginService(_serviceContainer));
            _serviceContainer.AddService(typeof(ApplicationState), new ApplicationState());
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            var form = new Views.AuthorizationWindow(_serviceContainer);
            form.ShowDialog();
            _mainWindowViewModel.InitializeTreeView();
            _mainWindowViewModel.InitializeMenu();

            StartTimer();
        }

        private void tv_dep_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
            }
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }

        private DispatcherTimer timer;
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var a = TV.SelectedItem;
            //   _mainWindowViewModel.InitializeDataGrid(((ApplicationState)ServiceContainer.GetService(typeof(ApplicationState))).SelectedFolder.Id);
            _mainWindowViewModel.InitializeTreeView();

        }

    }
}
