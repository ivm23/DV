using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.WinForms;
using Registration.Model;

namespace Registration.WPF.ViewModels
{
    public class ImportantLetterControl : Notifier, ILetterPropertiesUIPlugin
    {
        public void OnLoad(IServiceProvider serviceProvider) { }

        private LetterView _letterView;
        public LetterView LetterView
        {
            set
            {
                _letterView = value;
                OnPropertyChanged(nameof(LetterView));
            }
            get {
                return _letterView;
            }
        }
        public event EventHandler AddedReceiver;

        private bool _readOnly;

        public bool ReadOnly {
            set
            {
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));   
            }
            get {
                return _readOnly; 
            }
        }
    }
}
