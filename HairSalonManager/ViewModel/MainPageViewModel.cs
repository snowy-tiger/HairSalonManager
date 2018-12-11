using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace HairSalonManager.ViewModel
{
    class MainPageViewModel : Notifier
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

        public Command LoadCommand { get; set; }

        public MainPageViewModel()
        {
            LoadCommand = new Command(ExecuteLoadMethod, CanExecuteMethod);
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        private void ExecuteLoadMethod(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
