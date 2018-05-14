using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Registration.WPF
{
    public interface IMessageService
    {
        void InfoMessage(string message);
        void ErrorMessage(string message);
        MessageBoxResult QuestionMessage(string message);
        void SingleMessage(string message);
    }
}
