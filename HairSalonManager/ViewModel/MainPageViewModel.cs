using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Util;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace HairSalonManager.ViewModel
{
    class MainPageViewModel : ViewModelBase
    {

        #region field
        readonly ReservationRepository _reservationRepository;

        readonly ReservedServiceRepository _reservedServiceRepository;

        readonly ServiceRepository _serviceRepository;

        readonly StylistRepository _stylistRepository;

        private ObservableCollection<ReservationVo> _resList;

        DispatcherTimer _timer;

        NoticeService _noticeService;
        #endregion

        #region property        
        private  ObservableCollection<DataCommandViewModel<ReservedServiceVo>> _serviceCommands;

        public  ObservableCollection<DataCommandViewModel<ReservedServiceVo>> ServiceCommands
        {
            get { return _serviceCommands; }
            set
            {
                _serviceCommands = value;               
            }
        }
            
        private ObservableCollection<ServiceVo> _servicelist;

        public ObservableCollection<ServiceVo> ServiceList
        {
            get { return _servicelist; }
            set { _servicelist = value; }
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

        private ObservableCollection<StylistVo> _stylistList;

        public ObservableCollection<StylistVo> StylistList
        {
            get { return _stylistList; }
            set { _stylistList = value; }
        }

        private ReservationVo _selectedRes;

        public ReservationVo SelectedRes
        {
            get { return _selectedRes;  }
            set {
                _selectedRes = value;                
                IsSelected = false;
                IsSelectedService = true;
                OnPropertyChanged("SelectedRes");
                if(SelectedRes != null)
                    onSelectedResChanged(SelectedRes.ResNum);
            }
        }
              
        public Command InsertCommand { get; set; }
        public Command ModifyCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command InsertRSCommand { get; set; }
        public Command InitalizeCommand { get; }


       

        private ServiceVo _isselectedService;

        public ServiceVo SelectedService
        {
            get { return _isselectedService; }
            set
            {
                _isselectedService = value;
                OnPropertyChanged("IsSelectedService");
            }
        }
       
        private bool isMan;

        public bool IsMan
        {
            get { return isMan; }
            set { isMan = value; if (value == true) SelectedRes.Gender = 0; OnPropertyChanged("IsMan"); }
        }
        
        private bool man;

        public bool Man
        {
            get { return (SelectedRes.Gender == 1) ? true : false; }
            set { man = value;  if (value == true) SelectedRes.Gender = 1; OnPropertyChanged("Man"); }
        }

        private bool _isSelected;

        public bool IsSelected //선택될때 -> gender가 -1가 아닐때
        {
            get {
                if (SelectedRes == null)
                {
                    _isSelected = true;
                }
                else
                {
                    if (SelectedRes.Gender == -1)
                        _isSelected = true;
                }
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                //if()
                OnPropertyChanged("IsSelected");
            }
           
        }

        private bool _isSelectedService;

        public bool IsSelectedService
        {
            get { return _isSelectedService; }
            set
            {
                _isSelectedService = value;
                OnPropertyChanged("IsSelectedService");
            }
        }

        private DateTime _selDate;

        public DateTime SelDate
        {
            get { return _selDate; }
            set {
                _selDate = value;
                OnPropertyChanged("SelDate");
                SelectedRes.StartAt = SelDate.Date;
            }
        }

        private int _selHour;

        public int SelHour
        {
            get { return AvailableHour.IndexOf(_selHour); }
            set { _selHour = value;
                OnPropertyChanged("SelHour");
                TimeSpan ts = new TimeSpan(_selHour, 0, 0);
                SelectedRes.StartAt += ts;
            }
        }

        private int _selMinute;

        public int SelMinute
        {
            get { return AvailableMinute.IndexOf(_selMinute); }
            set {
                _selMinute = value;                
                OnPropertyChanged("SelMinute");
                TimeSpan ts = new TimeSpan(0,_selMinute, 0);
                SelectedRes.StartAt += ts;
            }
        }
        

        public DateTime StartDate
        {
            get { return DateTime.Today; }           
        }

        private readonly List<int> _availableHour;

        public List<int> AvailableHour
        {
            get { return _availableHour; }
            
        }

        private readonly List<int> _availableMinute;

        public  List<int> AvailableMinute
        {
            get { return _availableMinute; }           
        }
        #endregion

        #region ctor
        public MainPageViewModel()
        {            
            _reservationRepository = ReservationRepository.Rr; ;
            _reservedServiceRepository = ReservedServiceRepository.RSR;
            _serviceRepository = ServiceRepository.SR;
            _stylistRepository = StylistRepository.SR;

            _selectedRes = new ReservationVo();            
            SelectedRes.Gender = -1;
            ResList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            ServiceList = new ObservableCollection<ServiceVo>(_serviceRepository.GetServicesFromLocal());
            ServiceCommands = new ObservableCollection<DataCommandViewModel<ReservedServiceVo>>();
            StylistList = new ObservableCollection<StylistVo>(_stylistRepository.GetStylistsFromLocal());

            InsertCommand = new Command(ExecuteInsertMethod, CanExecuteMethod);
            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
            DeleteCommand = new Command(ExecuteDeleteMethod, CanExecuteMethod);
            InsertRSCommand = new Command(ExecuteInsertRSMethod, CanExecuteMethod);
            InitalizeCommand = new Command(ExecuteInitalizeMethod, CanExecuteMethod);

            _noticeService = NoticeService.NS;

            
            _timer = new DispatcherTimer();
            _timer.Interval = new System.TimeSpan(0, 0, 5);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
       

        #endregion

        #region method
        private void ExecuteInitalizeMethod(object obj)
        {
            SelectedRes = new ReservationVo();
            IsSelected = true;
            IsSelectedService = false;
        }
        private void ExecuteDeleteMethod(object obj)
        {            
            _reservationRepository.RemoveReservation(SelectedRes.ResNum);
            ResList.Remove(SelectedRes);
            SelectedRes = new ReservationVo(); //삭제되면 SelectedRes도 삭제 됨으로 다시 생성
        }

        private void ExecuteModifyMethod(object obj)
        {
            if (!Check(SelectedRes))
            {               
                return;
            }
            _reservationRepository.UpdateReservation(SelectedRes);
        }

        private void ExecuteInsertMethod(object obj)
        {
            if (!Check(SelectedRes))
            {               
                return;
            }
            SelectedRes.ResNum =  _reservationRepository.InsertReservation(SelectedRes);
            ResList.Add(SelectedRes);
            _reservationRepository.RecentResNum = SelectedRes.ResNum;
        }

        private void ExecuteRemoveMethod(ReservedServiceVo rsv)
        {
            if (!Check(SelectedRes))
            {                
                return;
            }
            _reservedServiceRepository.RemoveReservedService(rsv.ResNum, rsv.SerId);
        }

        private void ExecuteInsertRSMethod(object obj)
        {
            if (SelectedService == null)
            {
                MessageBox.Show("추가할 서비스를 선택해주세요.");
                return;
            }
            List<ReservedServiceVo> list = _reservedServiceRepository.GetReservedServices(SelectedRes.ResNum);
            if (list.FirstOrDefault(x => (x.SerId == SelectedService.ServiceId)) != null)
            {
                MessageBox.Show("이미 존재하는 서비스입니다.");
                return;
            }
            ReservedServiceVo rv = new ReservedServiceVo();
            rv.ResNum = SelectedRes.ResNum;
            rv.SerId = SelectedService.ServiceId;
            _reservedServiceRepository.InsertReservedService(rv);            
            ServiceCommands.Add(new DataCommandViewModel<ReservedServiceVo>(SelectedService.ServiceName, new Command(RemoveRS), rv));
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        private void onSelectedResChanged(uint resNum)
        {
            List<ReservedServiceVo> list = _reservedServiceRepository.GetReservedServices(resNum);
            ServiceCommands.Clear();
            foreach (var v in list) 
            {                
                string serviceName = ServiceList.Single(x => (x.ServiceId == v.SerId)).ServiceName;                    
                ServiceCommands.Add(new DataCommandViewModel<ReservedServiceVo>(serviceName,new Command(RemoveRS),v));                
            }
        }
       
        private void RemoveRS(object obj)
        {
            ReservedServiceVo rsv = (ReservedServiceVo)obj;
            _reservedServiceRepository.RemoveReservedService(rsv.ResNum, rsv.SerId);
            ServiceCommands.Remove(ServiceCommands.Single(x => (x.Data == rsv)));            
        }
       
        private bool Check(ReservationVo rv)
        {
            if (rv.UserName != null && rv.UserTel != null && rv.StylistId != null && rv.Gender != -1)
            {
                if (rv.UserTel.Length > 12)
                {
                    MessageBox.Show("번호가 너무 깁니다.");
                    return false;
                }
                return true;
            }
            MessageBox.Show("빈칸이 존재합니다.");
            return false;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_noticeService.IsNewReservationExistent == true)
            {
                ResList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
                _noticeService.IsNewReservationExistent = false;
            }
        }

       
        #endregion

    }
}
