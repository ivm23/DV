using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Registration.ClientInterface;
namespace Registration.WPF.ViewModels
{
    class RenameFolderViewModel : Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        private string _name;
        public RenameFolderViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;

            Name = ((ApplicationState)provider.GetService(typeof(ApplicationState))).SelectedFolder.Name;
            RenameFolder = new ViewModels.Command(arg => RenameFolderMethod());
        }

        private void RenameFolderMethod()
        {
            ((IClientRequests)ServiceProvider.GetService(typeof(IClientRequests))).UpdateFolder(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id, Name);
        }

        public ICommand RenameFolder { set; get; }

        private IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }
        public string Name
        {
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
            get { return _name; }
        }
    }

}
