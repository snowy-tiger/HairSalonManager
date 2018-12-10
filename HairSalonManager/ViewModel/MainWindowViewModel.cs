using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HairSalonManager.ViewModel
{
    class MainWindowViewModel:Notifier
    {
        private WindowState _windowState;

        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }


        public Command MinimizeCommand { get; set; }
        public Command CloseCommand { get; set; }

        public MainWindowViewModel()
        {
            MinimizeCommand = new Command(MinimizeMethod, CanExecuteMethod);
            CloseCommand = new Command(CloseMethod, CanExecuteMethod);
        }

        private void MinimizeMethod(object parameter)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseMethod(object parameter)
        {
            Environment.Exit(0);
        }
        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }
    }
}
