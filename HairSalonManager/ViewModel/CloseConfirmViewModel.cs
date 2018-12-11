using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HairSalonManager.ViewModel
{
    class CloseConfirmViewModel
    {
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }

        public CloseConfirmViewModel()
        {
            CloseCommand = new Command(ExecuteCloseMethod,CanExecute);
            CancelCommand = new Command(ExecuteCancelMethod,CanExecute);
        }

        private void ExecuteCloseMethod(object parameter)
        {
            Environment.Exit(0);
        }
        private void ExecuteCancelMethod(object parameter)
        {
            
        }
        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
