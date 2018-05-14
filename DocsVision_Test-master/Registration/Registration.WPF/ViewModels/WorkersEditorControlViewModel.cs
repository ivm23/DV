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
        private int stringEndIndex = 0;
        public WorkersEditorControlViewModel(IEnumerable<string> allWorkers)
        {
            _allWorkers = allWorkers;
            AllWorkers = allWorkers;

            ChangedText = new ViewModels.Command(arg => ChangedTextMethod(arg));
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
                return workers.AsQueryable().Where(str => !string.IsNullOrEmpty(str) && _allWorkers.Contains(str.Trim()));
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

        private string _selectedWorker;
        public string SelectedWorker
        {
            set
            {
                _selectedWorker = value;
                OnPropertyChanged(nameof(SelectedWorker));
            }
            get
            {
                return _selectedWorker;
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

        private void findNewIndex(string str)
        {
            int i = str.LastIndexOf(SplitMarker);

            str = str.Substring(0, i - 1);

            stringEndIndex = str.LastIndexOf(SplitMarker);
            if (stringEndIndex < 0)
                stringEndIndex = 0;
            else
            {
                ++stringEndIndex;
                if (stringEndIndex < str.Length)
                {
                    while (str[stringEndIndex] == ' ' && stringEndIndex < str.Length - 1) ++stringEndIndex;
                    ++stringEndIndex;
                }
            }
        }

        public ICommand ChangedText { set; get; }
        private void ChangedTextMethod(object arg)
        {
            SelectedWorker = null;
            string currentString = (string)arg;

            if (stringEndIndex > (currentString).Length )
            {
                findNewIndex(currentString.Trim());
                Names = currentString;
                selectedWorkers.RemoveAt(selectedWorkers.Count() - 1);
            }

            Enable = Visibility.Visible;
            string newName = currentString.Substring(stringEndIndex);
            if (!string.IsNullOrEmpty(newName.Trim()))
            {
                var matchWorkerNames = new List<string>();

                foreach (string workerName in _allWorkers)
                {
                    if (workerName.Contains(newName))
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
            else Enable = Visibility.Collapsed;
        }

        private IList<string> selectedWorkers = new List<string>();

        public ICommand AddWorker { get; set; }
        private void AddWorkerMethod(object arg)
        {
              selectedWorkers.Add((string)arg);

                NamesWorkers = selectedWorkers;
            
            stringEndIndex = Names.Length;
            Enable = Visibility.Hidden;
        }

        public ICommand AddSeveralWorkers { get; set; }
        private void AddSeveralWorkersMethod()
        {
            IEnumerable<string> nonSelectedWorkers = new List<string>();
            nonSelectedWorkers = _allWorkers.Where(str => !selectedWorkers.Contains(str));

            var window = new Views.AddWorkersFromAllWorkersListWindow(selectedWorkers, nonSelectedWorkers);

            if (window.ShowDialog() == true)
            {
                selectedWorkers = window.GetSelectedWorkers().ToList();
                NamesWorkers = selectedWorkers;
                stringEndIndex = Names.Length;
            }
        }
    }
}
