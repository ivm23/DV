using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

namespace Registration.WPF.ViewModels
{
    class AuthorizationViewModel : INotifyPropertyChanged
    {
        public string Name
        {
            get { return this._name; }
            set
            {
                this._name = value;

                OnPropertyChanged("Name");
                OnPropertyChanged("HelloText");
            }
        }

        public string HelloText
        {
            get
            {
                return "fff";//_service.SayHello(this.Name);
            }
        }

        string _name;

        public AuthorizationViewModel()
        {
            //    this._service = service;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,
                        new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}

