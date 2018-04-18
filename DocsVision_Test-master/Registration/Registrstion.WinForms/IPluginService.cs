using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.WinForms
{
    interface IPluginService
    {
        IFolderPropertiesUIPlugin GetFolderPropetiesPlugin(FolderType selectedFolderType);
        ILetterPropertiesUIPlugin GetLetterPropetiesPlugin(LetterType selectedLetterType);
    }
}
