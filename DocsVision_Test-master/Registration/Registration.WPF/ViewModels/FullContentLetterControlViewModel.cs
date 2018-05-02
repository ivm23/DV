using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;
using Registration.WinForms;

namespace Registration.WPF.ViewModels
{
    class FullContentLetterControlViewModel : ViewModelBase, ILetterPropertiesUIPlugin
    {
        public FullContentLetterControlViewModel()
        {

        }

        public void OnLoad(IServiceProvider serviceProvider)
        {
            LetterView = ((ApplicationState)serviceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
        }

        private LetterView _letterView;

        public LetterView LetterView
        {
            set
            {
                _letterView = value;
                OnPropertyChanged(nameof(LetterView));
            }
            get
            {
                return _letterView;
            }
        }

        public event EventHandler AddedReceiver;

        private bool _readOnly;

        public bool ReadOnly
        {
            set
            {
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
            get
            {
                return _readOnly;
            }
        }
     
    }
}
