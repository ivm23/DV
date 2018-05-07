using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace Registration.WPF.ViewModels
{
    class WorkersEditorControlViewModel : Notifier
    {
        public WorkersEditorControlViewModel(IEnumerable<string> allWorkers)
        {
            _allWorkers = allWorkers;
            AllWorkers = allWorkers;
            Focus = true;
            ChangedText = new ViewModels.Command(arg => ChangedTextMethod(arg));
            FocusToListBox = new ViewModels.Command(arg => FocusToListBoxMethod(arg));
            AddWorker = new ViewModels.Command(arg => AddWorkerMethod(arg));
            AddSeveralWorkers = new ViewModels.Command(arg => AddSeveralWorkersMethod());

            Enable = Visibility.Collapsed;
        }

        private IEnumerable<string> _receivers;
        private StringBuilder workersString;
        private const char SplitMarker = ';';
        private IEnumerable<string> _allWorkers = new List<string>();

        public IEnumerable<string> NamesWorkers
        {
            set
            {
                workersString = new StringBuilder();
                foreach (string worker in value)
                {
                    workersString.Append(worker).Append(SplitMarker).Append(" ");
                }
                Names = workersString.ToString();
            }
            get
            {
                IEnumerable<string> workers = new List<string>();
                workers = workersString.ToString().Trim().Split(SplitMarker);
                return workers.AsQueryable().Where(str => !string.IsNullOrEmpty(str) && _allWorkers.Contains(str));
            }
        }
        private string _names;
        public string Names
        {
            get
            {
                return _names;
            }
            set
            {
                _names = value;
                OnPropertyChanged(nameof(Names));
            }
        }

        public IEnumerable<string> AllWorkers
        {
            set
            {
                _receivers = value;
                OnPropertyChanged(nameof(AllWorkers));
            }
            get
            {
                return _receivers;
            }
        }

        
        public void InitializeAllWorkers(IEnumerable<string> allWorkers)
        {
            NamesWorkers = allWorkers;
        }

        private Visibility _enable;
        public Visibility Enable
        {
            set
            {
                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
            get
            {
                return _enable;
            }
        }

        public string SelectedNameWorker { get; set; }

        private bool _focus;
        public bool Focus {
            get
            {
                return _focus;
            }
            set {
                _focus = value;
                OnPropertyChanged(nameof(Focus));
            }

            }

        public ICommand ChangedText { set; get; }
        private void ChangedTextMethod(object arg)
        {
            Enable = Visibility.Visible;
            var matchWorkerNames = new List<string>();
            foreach(string workerName in _allWorkers)
            {
                if (workerName.Contains((string)arg))
                {
                    matchWorkerNames.Add(workerName);
                }
            }
            if (matchWorkerNames.Count != 0)
            {
                AllWorkers = matchWorkerNames;
            }
            else Enable = Visibility.Collapsed;
        }

        public ICommand FocusToListBox { get; set; }
        private void FocusToListBoxMethod(object arg)
        {
            Focus = true;
        }

        private IList<string> selectedWorkers = new List<string>();
        public ICommand AddWorker { get; set; }
        private void AddWorkerMethod(object arg)
        {
            selectedWorkers.Add((string)arg);
        }

        public ICommand AddSeveralWorkers { get; set; }
        private void AddSeveralWorkersMethod()
        {   
            IEnumerable<string> nonSelectedWorkers = new List<string>();
            nonSelectedWorkers = _allWorkers.Where(str => !selectedWorkers.Contains(str));
       
            var window = new Views.AddWorkersFromAllWorkersListWindow(selectedWorkers, nonSelectedWorkers);


            if (window.ShowDialog() == true)
            {
                NamesWorkers = window.GetSelectedWorkers();
            }
        }
    }
}
