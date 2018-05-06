using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.WPF.ViewModels
{
    class AddWorkersFromAllWorkersListViewModel : Notifier
    {
        private IEnumerable<string> _selectedWorkers;
        private IEnumerable<string> _nonSelectedWorkers;

        public AddWorkersFromAllWorkersListViewModel(IEnumerable<string> selectedWorkers, IEnumerable<string> nonSelectedWorkers)
        {
            SelectedWorkers = selectedWorkers;
            NonSelectedWorkers = nonSelectedWorkers;
        }

        public IEnumerable<string> SelectedWorkers
        {
            set {
                _selectedWorkers = value;
                OnPropertyChanged(nameof(SelectedWorkers));
            }
            get
            {
                return _selectedWorkers;
            }
        }


        public IEnumerable<string> NonSelectedWorkers
        {
            set
            {
                _nonSelectedWorkers = value;
                OnPropertyChanged(nameof(NonSelectedWorkers));
            }
            get
            {
                return _nonSelectedWorkers;
            }
        }
    }
}
