using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.WPF.ViewModels
{
    class WorkersEditorControlViewModel : Notifier
    {
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
                OnPropertyChanged(nameof(NamesWorkers));
            }
            get
            {
                IEnumerable<string> workers = new List<string>();
                workers = workersString.ToString().Trim().Split(SplitMarker);
                return workers.AsQueryable().Where(str => !string.IsNullOrEmpty(str) && _allWorkers.Contains(str));
            }
        }
    }
}
