using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.WinForms;
using System.Windows;

namespace Registration.WPF.ViewModels
{
    class ViewPluginShower
    {
        public static ILetterPropertiesUIPlugin Show(IServiceProvider provider)
        {
            if (null == provider)
                throw new ArgumentNullException();

            var selectedLetterType = ((ApplicationState)provider.GetService(typeof(ApplicationState))).SelectedLetterType;
            

       var a = ((PluginService)(provider.GetService(typeof(PluginService)))).GetLetterPropetiesPlugin(selectedLetterType);
            return a;
            //MessageBox.Show(selectedLetterType.TypeClientUI);            
        }   

    }
}
