using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Registration.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Registration.ClientInterface;
using Registration.WPF.ViewModels;
using Registration.WinForms;


namespace Registration.WPF.ViewModels
{
    class MakeFolderViewModel
    {
        public MakeFolderViewModel()
        {
            ClickCommand = new ViewModels.Command(args => ClickCommandMethod(args));
        }
        public ICommand ClickCommand { get; set; }

        private void ClickCommandMethod(object args)
        {
            int a = 1;
        }

    }
}
