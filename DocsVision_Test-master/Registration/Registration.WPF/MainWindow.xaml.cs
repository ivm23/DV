using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel.Design;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


using Registration.ClientInterface;
using Registration.Logger;


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
            ServiceContainer.AddService(typeof(IClientRequests), new ClientRequests());
            ServiceContainer.AddService(typeof(PluginService), new PluginService(ServiceContainer));
            ServiceContainer.AddService(typeof(ApplicationState), new ApplicationState());
            ServiceContainer.AddService(typeof(IMessageService), new MessageService());
        }

        private void InitializeMainWindow()
        {

            var form = new Views.AuthorizationWindow(_serviceContainer);

            var dialogResult = form.ShowDialog();
            if (dialogResult == true)
            {
                _mainWindowViewModel.InitializeTreeView();
                _mainWindowViewModel.InitializeMenu();

                StartTimer();
            }
            else
            {
                if (dialogResult == false)
                {
                    this.Close();
                }
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                InitializeMainWindow();
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }
        }

        private void TV_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectTreeViewItem(e);
        }

        private void SelectTreeViewItem(MouseButtonEventArgs e)
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
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            RefreshWindow();
        }

        private void RefreshWindow()
        {
            _mainWindowViewModel.InitializeTreeView();
          //  _mainWindowViewModel.InitializeDataGrid(((ApplicationState)ServiceContainer.GetService(typeof(ApplicationState))).SelectedFolder.Id);
        }
    }
}
