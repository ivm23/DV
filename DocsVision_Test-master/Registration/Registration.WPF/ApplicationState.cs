using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;


namespace Registration.WPF
{
    public class ApplicationState
    {
        public Worker Worker { get; set; } = new Worker();
        public Folder SelectedFolder { get; set; } = new Folder();
        public FolderType SelectedFolderType { get; set; } = new FolderType();
        public Letter SelectedLetter { get; set; } = new Letter();
        public LetterView SelectedLetterView { get; set; } = new LetterView();
        public LetterType SelectedLetterType { get; set; } = new LetterType();
        public ILetterPropertiesUIPlugin CurrentLetterPropertiesPlugin { get; set; }
        public IFolderPropertiesUIPlugin CurrentFolderPropertiesPlugin { get; set; }
        public IEnumerable<string> CurrentReceivers { get; set; } = new List<string>();
    }
}
