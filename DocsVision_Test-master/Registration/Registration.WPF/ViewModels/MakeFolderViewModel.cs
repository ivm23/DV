using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Registration.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Registration.ClientInterface;
using Registration.WPF.ViewModels;


namespace Registration.WPF.ViewModels
{
    class MakeFolderViewModel : Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private Worker _worker;
        private readonly Models.IMakeFolderWindow _parentWindow;
        public ICommand CreateFolder { get; set; }
        
        public MakeFolderViewModel(IServiceProvider provider, Models.IMakeFolderWindow parent)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;
            CreateFolder = new ViewModels.Command(arg => CreateFolderMethod());
            _parentWindow = parent;
        }

        private void CreateFolderMethod()
        {
            ClientRequests.CreateFolder(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id, FolderPlugin.FolderName, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType.Id, "");
        }

        private IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private void InitializeClientRequests()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private void InitializeWorker()
        {
            _worker = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker;
        }

        private IFolderPropertiesUIPlugin _folderPlugin;
        public IFolderPropertiesUIPlugin FolderPlugin
        {
            get
            {
                return _folderPlugin;
            }
            set
            {
                _folderPlugin = value;
            }
        }

        public void InitializeFolderPlugin()
        {
            InitializeClientRequests();
            InitializeWorker();

            FolderPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolderType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            FolderPlugin.OnLoad(ServiceProvider, _parentWindow);

            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CurrentFolderPropertiesPlugin = FolderPlugin;
        }
    }

}
