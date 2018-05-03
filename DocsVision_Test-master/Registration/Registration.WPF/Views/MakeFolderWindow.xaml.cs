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
        private IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;

        public MakeFolderWindow(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException(); 

            InitializeComponent();

            _serviceProvider = provider;
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        public IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        public IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        public void InitializeMakeFolderWindow(int selectedFolderType)
        {
            InitializeClientRequests();
            FolderType folderType = ClientRequests.GetFolderType(selectedFolderType);

            IFolderPropertiesUIPlugin folderPlugin = ViewModels.ViewPluginCreater.Create(folderType, (PluginService)ServiceProvider.GetService(typeof(PluginService)));
            folderPlugin.OnLoad(ServiceProvider);

            stackPanel.Children.Add((Control)(folderPlugin));
        }
       
    }
}
