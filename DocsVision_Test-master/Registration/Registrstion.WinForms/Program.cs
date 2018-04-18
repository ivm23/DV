using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;
using Registration.ClientInterface;
using Registration.Model;

namespace Registration.WinForms
{
    static class Program
    {
        private static IServiceContainer _serviceContainer = new ServiceContainer();
       
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {   
            IClientRequests clientRequests = new ClientRequests();
           
            _serviceContainer.AddService(typeof(IClientRequests), clientRequests);
            _serviceContainer.AddService(typeof(PluginService), new PluginService(_serviceContainer));
            _serviceContainer.AddService(typeof(Message.IMessageService), new Message.MessageService());
            _serviceContainer.AddService(typeof(ApplicationState), new ApplicationState());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.MainWorkerForm(_serviceContainer));
        }
    }
}
