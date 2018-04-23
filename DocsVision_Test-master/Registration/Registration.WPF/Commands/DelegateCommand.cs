using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Registration.WPF.Commands
{
    public class DelegateCommand : ICommand
    {
        Func<object, bool> _canExecute;
        Action<object> _execute;

        //Конструктор
        public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        //Проверка доступности команды
        public bool CanExecute(object parameter)
        {
            return this._canExecute(parameter);
        }

        //Выполнение команды
        public void Execute(object parameter)
        {
            this._execute(parameter);
        }

        //Служебное событие
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
    }
}
