using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.WinForms;
using System.Windows;
using Registration.Model;

namespace Registration.WPF.ViewModels
{
    class ViewPluginCreater
    {
        public static ILetterPropertiesUIPlugin Create(LetterType letterType, PluginService pluginService)
        {
            if (null == letterType)
                throw new ArgumentNullException();

            return pluginService.GetLetterPropetiesPlugin(letterType);
        }

        public static IFolderPropertiesUIPlugin Create(FolderType folderType, PluginService pluginService)
        {
            if (null == folderType)
                throw new ArgumentNullException();

            return pluginService.GetFolderPropetiesPlugin(folderType);
        }

    }
}
