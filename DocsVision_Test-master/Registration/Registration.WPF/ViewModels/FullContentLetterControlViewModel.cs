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
    class FullContentLetterControlViewModel : Notifier
    {
        private readonly IServiceProvider _serviceProvider;
        public FullContentLetterControlViewModel(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            _serviceProvider = provider;
        }

        private IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        private LetterView _letterView = new LetterView();
     
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

        public string LetterTitle {
            set
            {
                _letterView.Name = value;
                OnPropertyChanged(nameof(LetterTitle));
            }
            get
            {
                return _letterView.Name;
            }
        }
        public string LetterText
        {
            set
            {
                _letterView.Text = value;
                OnPropertyChanged(nameof(LetterText));
            }
            get
            {
                return _letterView.Text;
            }
        }
        public string LetterSenderName
        {
            set
            {
                _letterView.SenderName = value;
                OnPropertyChanged(nameof(LetterSenderName));
            }
            get
            {
                return _letterView.SenderName;
            }
        }
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

        public void InitializeLetterView()
        {
            LetterView = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView;
        }
    }
}
