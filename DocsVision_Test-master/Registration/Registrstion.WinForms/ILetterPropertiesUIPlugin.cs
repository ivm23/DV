using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.WinForms
{
    public interface ILetterPropertiesUIPlugin
    {
        void OnLoad(IServiceProvider serviceProvider);

        LetterView LetterView { get; set; }

        event EventHandler AddedReceiver;
        bool ReadOnly { set; get; }
    }
}
