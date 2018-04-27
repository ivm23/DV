using System;
using System.Collections.Generic;
using Registration.WinForms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.Windows.Forms;

namespace Registration
{

    public interface IFolderPropertiesUIPlugin : IPropertiesUIPlugin
    {
        void OnLoad(IServiceProvider serviceProvider);
        FolderProperties FolderProperties { set; get; }

        event EventHandler ChangedFolderTypePlugin;

        FolderType FolderType { set; get; }
    }
}
