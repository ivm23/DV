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
using Registration.WinForms;

namespace Registration.WPF.Views
{
    /// <summary>
    /// Interaction logic for FullContentLetterWindow.xaml
    /// </summary>
    public partial class FullContentLetterWindow : Window
    {
        private IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        public FullContentLetterWindow(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            InitializeComponent();
            _serviceProvider = provider;
        }

        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        public IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        public void InitializeFullContentLetterWindow()
        {
            InitializeClientRequests();

            ILetterPropertiesUIPlugin letterPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            letterPlugin.OnLoad(ServiceProvider);
            letterPlugin.ReadOnly = true;

            stackPanel.Children.Add((Control)(letterPlugin));
            N = "saas";
        }

        public string N
        {
            get; set;
        }
     public LetterView LetterView { get; set; }

    }
}
