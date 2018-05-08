using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Registration.ClientInterface;
using Registration.Model;
using System.Windows.Controls;
using System.Windows;

namespace Registration.WPF.ViewModels
{

    class LetterViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private Worker _worker;
        public ILetterPropertiesUIPlugin LetterPlugin { get; set; }

        public LetterViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            DeleteLetter = new ViewModels.Command(arg => DeleteLetterMethod(arg));
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

        public void InitializeLetterPlugin()
        {
            InitializeClientRequests();

            LetterPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            LetterPlugin.OnLoad(ServiceProvider);
            LetterPlugin.ReadOnly = true;
        }
        
        public ICommand DeleteLetter { set; get; }

        private void DeleteLetterMethod(object arg)
        {
            ClientRequests.DeleteLetter(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            ((Window)arg).Close();
        }
    }
}
