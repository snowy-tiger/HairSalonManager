using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HairSalonManager
{
    class Command : ICommand
    {
        Action<object> action;

        Func<object, bool> func;

        public Command(Action<object> action,Func<object, bool> func)
        {
            this.action = action;
            this.func = func;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return func(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
