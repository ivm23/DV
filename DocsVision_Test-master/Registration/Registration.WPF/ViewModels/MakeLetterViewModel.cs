using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Registration.ClientInterface;
using Registration.Model;
using System.Windows;

namespace Registration.WPF.ViewModels
{
    class MakeLetterViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private IClientRequests _clientRequests;
        private Worker _worker;
        public MakeLetterViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;
            SendLetterClick = new ViewModels.Command(arg => SendLetterClickMethod());
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

        private IMessageService _messageService;

        private IMessageService MessageService
        {
            get { return _messageService; }
        }
        private void InitializeMessageService()
        {
            _messageService = (IMessageService)ServiceProvider.GetService(typeof(IMessageService));            
        }

        private void InitializeWorker()
        {
            _worker = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker;
        }

        public ICommand SendLetterClick { get; set; }

        private void SendLetterClickMethod()
        {
            var letterView = LetterPlugin.LetterView;
            if (string.IsNullOrEmpty(letterView.Name) || string.IsNullOrEmpty(letterView.Text) || letterView.ReceiversName.Count == 0)
            {
                MessageService.InfoMessage(MessageResources.EmptyListRecipient);
            }
            else
            {
                ClientRequests.CreateLetter(letterView.Name, _worker.Id, letterView.ReceiversName, letterView.Text, letterView.ExtendedData, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType.Id);
            }
        }

        public ILetterPropertiesUIPlugin LetterPlugin { get; set; }

        public void InitializeLetterPlugin()
        {
            InitializeClientRequests();
            InitializeWorker();
            InitializeMessageService();

            LetterPlugin = ViewModels.ViewPluginCreater.Create(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType, ((PluginService)ServiceProvider.GetService(typeof(PluginService))));
            LetterPlugin.OnLoad(ServiceProvider);
            LetterPlugin.ReadOnly = false;           
        }
    }

}