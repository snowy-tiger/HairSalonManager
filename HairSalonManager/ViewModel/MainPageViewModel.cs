using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace HairSalonManager.ViewModel
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationVo> _resList;

        public ObservableCollection<ReservationVo> ResList
        {
            get { return _resList; }
            set
            {
                _resList = value;
                OnPropertyChanged("ResList");
            }
        }

        private ReservationVo _selectedRes;

        public ReservationVo SelectedRes
        {
            get { return _selectedRes; }
            set {
                _selectedRes = value;
                OnPropertyChanged("SelectedRes");
            }
        }

        
        public Command InsertCommand { get; set; }
        public Command ModifyCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public MainPageViewModel()
        {
            InsertCommand = new Command(ExecuteInsertMethod, CanExecuteMethod);
            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
            DeleteCommand = new Command(ExecuteDeleteMethod, CanExecuteMethod);
        }

        private void ExecuteDeleteMethod(object obj)
        {
            
        }

        private void ExecuteModifyMethod(object obj)
        {
            
        }

        private void ExecuteInsertMethod(object obj)
        {
            
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }
    }
}
