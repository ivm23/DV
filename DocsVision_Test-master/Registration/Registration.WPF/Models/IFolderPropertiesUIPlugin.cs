using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

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
