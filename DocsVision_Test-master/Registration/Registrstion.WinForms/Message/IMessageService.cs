using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration.WinForms.Message
{
    public interface IMessageService
    {
        void InfoMessage(string message);
        void ErrorMessage(string message);
        DialogResult QuestionMessage(string message);
        void SingleMessage(string message);
    }
}
