using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Util;
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
        #region field, property
        readonly ReservationRepository _reservationRepository;

        private ObservableCollection<ReservationVo> _resList;

        private ReadOnlyCollection<ButtonCommandViewModel> _menuCommands;

        public ReadOnlyCollection<ButtonCommandViewModel> MenuCommands
        {
            get
            {
                if (_menuCommands == null)
                {
                    List<ButtonCommandViewModel> list = this.CreateCommands();
                    _menuCommands = new ReadOnlyCollection<ButtonCommandViewModel>(list);
                }
                return _menuCommands;
            }
            
        }

        private List<ButtonCommandViewModel> CreateCommands()
        {
            return new List<ButtonCommandViewModel>()
            {
                new ButtonCommandViewModel("메인",new Command(GoMainWindowPage),"red"),
                new ButtonCommandViewModel("미용사",new Command(GoStylistPage),"yellow"),
                new ButtonCommandViewModel("결제",new Command(GoPayPage),"blue"),
                new ButtonCommandViewModel("서비스",new Command(GoServicePage),"blue"),
                new ButtonCommandViewModel("전체 일정",new Command(GoSchedulePage),"blue"),
                new ButtonCommandViewModel("설정",new Command(GoSettingPage),"blue")
            };
        }

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
        #endregion

        #region ctor
        public MainPageViewModel()
        {
            _reservationRepository = ReservationRepository.Rr; ;
            ResList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            InsertCommand = new Command(ExecuteInsertMethod, CanExecuteMethod);
            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
            DeleteCommand = new Command(ExecuteDeleteMethod, CanExecuteMethod);
        }
        #endregion

        #region method
        private void ExecuteDeleteMethod(object obj)
        {
            ResList.Remove(SelectedRes);
            _reservationRepository.RemoveReservation(SelectedRes.ResNum);
        }

        private void ExecuteModifyMethod(object obj)
        {
            _reservationRepository.UpdateReservation(SelectedRes);
        }

        private void ExecuteInsertMethod(object obj)
        {
            ResList.Add(SelectedRes);
            _reservationRepository.InsertReservation(SelectedRes);
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }
        #endregion

        #region Navigator xaml
        private void GoSettingPage(object obj)
        {

        }

        private void GoSchedulePage(object obj)
        {

        }

        private void GoServicePage(object obj)
        {

        }

        private void GoPayPage(object obj)
        {
            //NavigationServiceProvider.Navigate("/PayPage.xaml");
        }

        private void GoStylistPage(object obj)
        {
            //NavigationServiceProvider.Navigate("/StylistPage.xaml");
        }

        private void GoMainWindowPage(object o)
        {
            NavigationServiceProvider.Navigate("/MainPage.xaml");
        }
        #endregion
    }
}
