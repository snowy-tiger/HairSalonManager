using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HairSalonManager.ViewModel
{
    class Command : ICommand
    {
        private Action<object> _action;
        private Func<object, bool> _func;

        public Command(Action<object> action) : this(action,null)
        {
            _action = action;
        }

        public Command(Action<object> action, Func<object, bool> func)
        {
            this._action = action;
            this._func = func;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            //return _func(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
