using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

            AddReceivers = new ViewModels.Command(arg => AddReceiversMethod());
            DeleteReceivers = new ViewModels.Command(arg => DeleteReceiversMethod());
        }

        private void AddReceiversMethod()
        {
            IList<string> receivers = SelectedWorkers.ToList();
            IList<string> nonSelectedWorkers = NonSelectedWorkers.ToList();
            int index = nonSelectedWorkers.IndexOf(ReceiverForAdd);

            if (!string.IsNullOrEmpty(ReceiverForAdd))
            {
                receivers.Add(ReceiverForAdd);
                SelectedWorkers = receivers;
                nonSelectedWorkers.Remove(ReceiverForAdd);
                NonSelectedWorkers = nonSelectedWorkers;
                if (0 <= index && index < NonSelectedWorkers.Count())
                    ReceiverForAdd = NonSelectedWorkers.ElementAt(index);
            }
        }

        private void DeleteReceiversMethod()
        {

            IList<string> receivers = SelectedWorkers.ToList();
            IList<string> nonSelectedWorkers = NonSelectedWorkers.ToList();
            if (!string.IsNullOrEmpty(ReceiverForDelete))
            {
                nonSelectedWorkers.Add(ReceiverForDelete);
                NonSelectedWorkers = nonSelectedWorkers;

                receivers.Remove(ReceiverForDelete);
                SelectedWorkers = receivers;
            }
        }

        public IEnumerable<string> SelectedWorkers
        {
            set
            {
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


        private string _receiverForAdd;
        private string _receiverForDelete;

        public string ReceiverForAdd
        {
            set
            {
                _receiverForAdd = value;
                OnPropertyChanged(nameof(ReceiverForAdd));
            }
            get
            {
                return _receiverForAdd;
            }
        }

        public string ReceiverForDelete
        {
            set
            {
                _receiverForDelete = value;
                OnPropertyChanged(nameof(ReceiverForDelete));
            }
            get { return _receiverForDelete; }
        }

        public ICommand AddReceivers { set; get; }
        public ICommand DeleteReceivers { set; get; }
    }
}
