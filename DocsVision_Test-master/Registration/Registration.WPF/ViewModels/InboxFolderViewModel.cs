using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.WPF.ViewModels
{
    class InboxFolderViewModel : Notifier
    {
        private readonly Folder _folder;
        public InboxFolderViewModel()
        {
            _folder = new Folder();
        }

        public string Name
        {
            set
            {
                _folder.Name = value;
                OnPropertyChanged(nameof(Name));
            }
            get
            {
                return _folder.Name;
            }
        }
        public int Type
        {
            set
            {
                _folder.Type = value;
                OnPropertyChanged(nameof(Type));
            }
            get
            {
                return _folder.Type;
            }
        }
    }
}
