using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.ComponentModel;

namespace Registration.WPF.ViewModels
{
    class StandartLetterControlViewModel : Notifier
    {
        private LetterView _letterView = new LetterView();

        public StandartLetterControlViewModel(LetterView letterView)
        {
            if (null == letterView)
                throw new ArgumentNullException();

            Title = letterView.Name;
            Text = letterView.Text;
            SenderName = letterView.SenderName;
            Date = letterView.Date;           
        }

        public string Title
        {
            set
            {
                _letterView.Name = value;
                OnPropertyChanged(nameof(Title));
            }
            get
            {
                return _letterView.Name;
            }
        }

        public string Text
        {
            set
            {
                _letterView.Text = value;
                OnPropertyChanged(nameof(Text));
            }
            get
            {
                return _letterView.Text;
            }
        }

        public string SenderName
        {
            set
            {
                _letterView.SenderName = value;
                OnPropertyChanged(nameof(SenderName));
            }
            get
            {
                return _letterView.SenderName;
            }
        }

        public DateTime Date
        {
            set
            {
                _letterView.Date = value;
                OnPropertyChanged(nameof(Date));
            }
            get
            {
                return _letterView.Date;
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
    }
}
