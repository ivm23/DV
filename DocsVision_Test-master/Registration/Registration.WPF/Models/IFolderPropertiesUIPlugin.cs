using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration
{

    public interface IFolderPropertiesUIPlugin //: IPropertiesUIPlugin
    {
        void OnLoad(IServiceProvider serviceProvider, WPF.Models.IMakeFolderWindow parent);
        FolderProperties FolderProperties { set; get; }
        FolderType FolderType { set; get; }
        string FolderName { set; get; }
    }
}
