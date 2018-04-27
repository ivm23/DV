﻿using System;
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
using Registration.WinForms;
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
            _serviceContainer.AddService(typeof(WinForms.Message.IMessageService), new WinForms.Message.MessageService());
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

            var form = new Views.AuthorizationWindow(_serviceContainer);
            form.ShowDialog();
      //      if (form.ShowDialog() == true)
            {
                _mainWindowViewModel.InitializeTreeView();
            }
        }
    }
}
