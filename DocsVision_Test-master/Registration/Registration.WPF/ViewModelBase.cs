using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Registration.WPF.ViewModels;
using System.Windows.Input;

namespace Registration.WPF
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private IServiceProvider _serviceProvider;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase()
        {
            ClickCommand = new ViewModels.Command(arg => ClickMethod());
        }

        public ICommand ClickCommand { get; set; }
        public string id = "dasd";

        public string Id
        {
            set
            {
                id = value;
            }
            get { return id;  }
        }

        private void ClickMethod()
        {
            id = "111";
        }
    }
}
