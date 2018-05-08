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

        void A(object sender, RoutedEventArgs e)
        {

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
        //    _serviceContainer.AddService(typeof(WinForms.Message.IMessageService), new WinForms.Message.MessageService());
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            var form = new Views.AuthorizationWindow(_serviceContainer);
            form.ShowDialog();
            _mainWindowViewModel.InitializeTreeView();
            _mainWindowViewModel.InitializeMenu();
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


        private void Opening(object sender, ContextMenuEventArgs e)
        {
            int a = 1;
        }

        private void Closing(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
