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
        private Views.LetterViewWindow _fullLetterContentWindow = null;
        private Views.MakeLetterWindow _makeLetterWindow = null;

        protected virtual void Closed()
        {

        }

        public bool Close()
        {
            var result = false;
            if (_fullLetterContentWindow != null)
            {
                _fullLetterContentWindow.Close();
                _fullLetterContentWindow = null;
                result = true;
            }
            return result;
        }

       /* protected void ShowFullContent(ViewModelBase viewModel)
        {
            viewModel._fullLetterContentWindow = new Views.FullContentLetterWindow();
            viewModel._fullLetterContentWindow.DataContext = viewModel;
            viewModel._fullLetterContentWindow.Closed += (sender, e) => Closed();
            viewModel._fullLetterContentWindow.ShowDialog();
        }
        */
       /* protected void ShowMakeLetterWindow(ViewModelBase viewModel)
        {
            viewModel._makeLetterWindow = new Views.MakeLetterWindow();
            viewModel._makeLetterWindow.DataContext = viewModel;
            viewModel._makeLetterWindow.ShowDialog();
        }*/

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}