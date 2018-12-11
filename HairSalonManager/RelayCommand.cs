using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HairSalonManager
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        readonly Action<object> _action;
        readonly Func<object, bool> _func;

        public RelayCommand(Action<object> action) : this(action,null)
        {

        }
        public RelayCommand(Action<object> action,Func<object,bool> func)
        {
            _action = action;
            _func = func;
        }

        public bool CanExecute(object parameter)
        {
            return _func(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);                
        }
    }
}
