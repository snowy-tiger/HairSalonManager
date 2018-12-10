using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.ViewModel
{
    class MainWindowViewModel
    {
        public Command CloseCommand { get; set; }

        public MainWindowViewModel()
        {
            CloseCommand = new Command(CloseMethod, CanExecuteMethod);
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
