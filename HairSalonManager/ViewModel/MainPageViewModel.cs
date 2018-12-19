using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Util;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

        readonly UserRepository _userRepository;

        private ObservableCollection<ReservationVo> _resList;

        DispatcherTimer _timer;

        NoticeService _noticeService;

        private DateTime formerDate;

        DataTable _timeTable = new DataTable();
        
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
                DetectChangedDate(value);
            }
        }

        public DataTable TimeTable
        {
            get { return _timeTable; }
            set {
                _timeTable = value;
                OnPropertyChanged("TimeTable");
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

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
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
            _userRepository =  UserRepository.UR;

            _selectedRes = new ReservationVo();            
            SelectedRes.Gender = -1;
            SelectedRes.StartAt = DateTime.Today;
            SelectedRes.EndAt = new DateTime();
            formerDate = new DateTime(1,1,1);
            StartDate = new DateTime();
            StartDate = DateTime.Today;

            ResList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            ServiceList = new ObservableCollection<ServiceVo>(_serviceRepository.GetServicesFromLocal());
            ServiceCommands = new ObservableCollection<DataCommandViewModel<ReservedServiceVo>>();
            StylistList = new ObservableCollection<StylistVo>(_stylistRepository.GetStylistsFromLocal());

            _availableHour = new List<int>();
            for (int x = 0; x < 24; x++)
            {
                _availableHour.Add(x);
            }
            _availableMinute = new List<int>() {0,30};

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

            MakeTimeTable(_selectedRes);
        }
       

        #endregion

        #region method
        private void ExecuteInitalizeMethod(object obj)
        {
            SelectedRes = new ReservationVo();
            IsSelected = true;
            IsSelectedService = false;
            SelectedRes.StartAt = DateTime.Today;
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
                        
                int dayInterval = (SelectedRes.StartAt.Date - formerDate.Date).Days;
                int hourInterval = SelectedRes.StartAt.Hour - formerDate.Hour;
                int minInterval = SelectedRes.StartAt.Minute - formerDate.Minute;

                //SelectedRes.StartAt.AddDays(dayInterval);
                //SelectedRes.StartAt.AddHours(hourInterval);
                //SelectedRes.StartAt.AddMinutes(minInterval);

                SelectedRes.EndAt = SelectedRes.StartAt;
                int sumTime = 0;

                foreach (ReservedServiceVo rsv in ReservedServiceRepository.RSR.GetReservedServices(SelectedRes.ResNum))
                {
                    sumTime += ServiceList.Single(x => x.ServiceId == rsv.SerId).ServiceTime;                                               
                }

            DateTime endAt = new DateTime();//임시로 저장해놓은 endat;
            endAt = SelectedRes.EndAt.AddHours(sumTime / 60);
            endAt = SelectedRes.EndAt.AddMinutes(sumTime % 60);

            if (HasReservations(SelectedRes.StylistId, SelectedRes.StartAt, endAt))
            {
                MessageBox.Show("이미 예약이 존재한 시간입니다.");
                return;
            }

            //통과하면 실제 데이터에 적용
            SelectedRes.EndAt  = SelectedRes.EndAt.AddHours(sumTime / 60);
            SelectedRes.EndAt = SelectedRes.EndAt.AddMinutes(sumTime % 60);
            _reservationRepository.UpdateReservation(SelectedRes);
        }

        private void ExecuteInsertMethod(object obj)
        {
            if (!Check(SelectedRes))
            {               
                return;
            }           
            SelectedRes.EndAt = SelectedRes.StartAt;
            SelectedRes.ResNum =  _reservationRepository.InsertReservation(SelectedRes);            
            ResList.Add(SelectedRes);
            _userRepository.InsertUser(new UserVo() { UserTel = SelectedRes.UserTel, Point = 0 });
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
            ServiceVo s = ServiceList.Single(x => x.ServiceId == rv.SerId);
            ushort serviceTime = s.ServiceTime;
            if (HasReservations(SelectedRes.StylistId,SelectedRes.EndAt,SelectedRes.EndAt + new TimeSpan(serviceTime / 60, serviceTime % 60, 0)))
            {
                MessageBox.Show("이미 예약이 존재한 시간대입니다.");
                return;
            }            
            _reservedServiceRepository.InsertReservedService(rv);
            ServiceCommands.Add(new DataCommandViewModel<ReservedServiceVo>(SelectedService.ServiceName, new Command(RemoveRS), rv));
            TimeSpan ts = new TimeSpan(SelectedService.ServiceTime / 60, SelectedService.ServiceTime % 60, 0);
            SelectedRes.EndAt = SelectedRes.EndAt + ts;
            _reservationRepository.UpdateReservation(SelectedRes);

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

            ServiceVo s = ServiceList.Single(x => x.ServiceId == rsv.SerId);
            ReservationVo r = ResList.Single(x => x.ResNum == rsv.ResNum);
            TimeSpan ts = new TimeSpan(s.ServiceTime / 60, s.ServiceTime % 60, 0);
            r.EndAt = r.EndAt - ts;
            _reservationRepository.UpdateReservation(r);

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

        

        private bool HasReservations(uint? stylistId,DateTime startTime ,DateTime endTime) //time  --> 예약 끝나는 시간
        {
            // ResList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations()); //다시 db상에서 데이터 받아옴
            // 이거할시 SelectedRes 사라짐
            int startTimeTotalM = ReturnTotalMin(startTime);
            int endTimeTotalM = ReturnTotalMin(endTime);

            foreach (ReservationVo r in ResList.Where(x => x.StylistId == stylistId))
            {
                int rstartTimeTotalM = ReturnTotalMin(r.StartAt);
                int rendTimeTotalM = ReturnTotalMin(r.EndAt);
                if (rstartTimeTotalM == rendTimeTotalM)
                    continue;
                if (r.StartAt.Date == startTime.Date)
                {
                    if (rstartTimeTotalM <= startTimeTotalM && rendTimeTotalM >= endTimeTotalM) //다른 예약에 의해 포함될때
                        return true;
                    else if (startTimeTotalM <= rstartTimeTotalM && endTimeTotalM >= rendTimeTotalM) //한 예약을 포함할때
                        return true;
                }
            }           
            return false;
        }

        private int ReturnTotalMin(DateTime time)
        {
            return time.Hour * 60 + time.Minute;
        }

        private void MakeTimeTable(ReservationVo reservation)
        {
            DataColumn _col;

            for (int i = 0; i < 48; i++)
            {
                _col = TimeTable.Columns.Add();
                _col.ColumnName = (i / 2).ToString("D2") + " : " + (i % 2 * 30).ToString("D2");
            }

            ShowTimeTable(reservation);
        }

        private void ShowTimeTable (ReservationVo reservation)
        {
            DataRow _row;

            IEnumerable<ReservationVo> necessaryList;

            _row = TimeTable.NewRow(); //DataRow를 생성해서 그 사람의 예약 테이블을 채워야지

            //예약 리스트 중에서 스타일리스트 아이디와 콤보박스에서 셀렉트한 미용사의 아이디 비교
            necessaryList = ResList.Where(x => x.StylistId == SelectedRes.StylistId);

            //이 목록 중에서 선택한 날짜만 다시 불러오기
            necessaryList = necessaryList.Where(x => x.StartAt.ToString("d").Equals(SelectedRes.StartAt.ToString("d")));
            
            //SaveResInColumn(necessaryList);
            SaveResInColumn.SaveReservationInColumn(necessaryList, TimeTable, _row);

        }

        //이벤트
        private void DetectChangedDate(ReservationVo reservation)
        {
            //_timeTable.Clear();
            //ShowTimeTable(reservation);
        }

        #endregion

    }
}
