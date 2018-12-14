using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Util;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HairSalonManager.ViewModel
{
    class MainPageViewModel : ViewModelBase
    {

        #region field
        readonly ReservationRepository _reservationRepository;

        readonly ReservedServiceRepository _reservedServiceRepository;

        readonly ServiceRepository _serviceRepository;

        private ObservableCollection<ReservationVo> _resList;
       

        #endregion

        #region property        
        private  ObservableCollection<DataCommandViewModel<ReservedServiceVo>> _serviceCommands;

        public  ObservableCollection<DataCommandViewModel<ReservedServiceVo>> ServiceCommands
        {
            get { return _serviceCommands; }
            set
            { _serviceCommands = value; }
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
            }
        }

        private ReservationVo _selectedRes;

        public ReservationVo SelectedRes
        {
            get { return _selectedRes; }
            set {
                _selectedRes = value;                
                OnPropertyChanged("SelectedRes");
                if(SelectedRes != null)
                    onSelectedResChanged(SelectedRes.ResNum);
            }
        }
        
      

        public Command InsertCommand { get; set; }
        public Command ModifyCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command InitalizeCommand { get; }


        //private int _resNum;

        public uint ResNum
        {
            get
            {                
                return SelectedRes.ResNum;
            }
            set
            {
                SelectedRes.ResNum = value;
                OnPropertyChanged("ResNum");
            }
        }

        //private int _stylistId;

        public uint? StylistId
        {
            get
            {                
                return SelectedRes.StylistId;
            }
            set
            {               
                SelectedRes.StylistId = value;
                OnPropertyChanged("StylistId");
            }
        }

        //private string _userTel;

        public string UserTel
        {
            get
            {                
                return SelectedRes.UserTel;
            }
            set
            {                
                SelectedRes.UserTel = value;
                OnPropertyChanged("UserTel");
            }
        }

        //private string _note;

        public string Note
        {
            get
            {  
                return SelectedRes.Note; }
            set
            {
                SelectedRes.Note = value;
                OnPropertyChanged("Note");
            }
        }

        //private int _gender;

        public int? Gender
        {
            get
            {             
                return SelectedRes.Gender;
            }
            set
            {
                SelectedRes.Gender = value;
                OnPropertyChanged("Gender");
            }
        }

       // private DateTime _userBirthday;

        public DateTime? UserBirthday
        {
            get
            {                
                return SelectedRes.UserBirthday; }
            set
            {
                SelectedRes.UserBirthday = value;
                OnPropertyChanged("UserBirthday");
            }
        }

        //private DateTime _startAt;

        public DateTime? StartAt
        {
            get
            {                
                return SelectedRes.StartAt;
            }
            set
            {
                SelectedRes.StartAt = value;
                OnPropertyChanged("StartAt");
            }
        }

        //private DateTime _endAt;

        public DateTime? EndAt
        {
            get
            {                                  
                return SelectedRes.EndAt; }
            set
            {
                SelectedRes.EndAt = value;
                OnPropertyChanged("EndAt");
            }
        }

        //private string _userName;

        public string UserName
        {
            get {                
                return SelectedRes.UserName; }
            set
            {
                SelectedRes.UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private bool _isPaid;

        

        public bool IsPaid
        {
            get { 
                return _isPaid; }
            set
            {
                _isPaid = value;
                OnPropertyChanged("IsPaid");
            }
        }
        #endregion

        #region ctor
        public MainPageViewModel()
        {
            _reservationRepository = ReservationRepository.Rr; ;
            _reservedServiceRepository = ReservedServiceRepository.RSR;
            _serviceRepository = ServiceRepository.SR;

            _selectedRes = new ReservationVo();
            ResList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            ServiceList = new ObservableCollection<ServiceVo>(_serviceRepository.ServiceList);
            ServiceCommands = new ObservableCollection<DataCommandViewModel<ReservedServiceVo>>();

            InsertCommand = new Command(ExecuteInsertMethod, CanExecuteMethod);
            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
            DeleteCommand = new Command(ExecuteDeleteMethod, CanExecuteMethod);
            InitalizeCommand = new Command(ExecuteInitalizeMethod, CanExecuteMethod);
        }

        #endregion

        #region method
        private void ExecuteInitalizeMethod(object obj)
        {

        }
        private void ExecuteDeleteMethod(object obj)
        {            
            _reservationRepository.RemoveReservation(SelectedRes.ResNum);
            ResList.Remove(SelectedRes);
        }

        private void ExecuteModifyMethod(object obj)
        {
            _reservationRepository.UpdateReservation(SelectedRes);
        }

        private void ExecuteInsertMethod(object obj)
        {
            
            _reservationRepository.InsertReservation(SelectedRes);
        }

        private void ExecuteRemoveMethod(ReservedServiceVo rsv)
        {          
            _reservedServiceRepository.RemoveReservedService(rsv.ResNum, rsv.SerId);
        }
        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        private void onSelectedResChanged(uint resNum)
        {
            List<ReservedServiceVo> list = _reservedServiceRepository.GetReservedServicesFromLocal();
            ServiceCommands.Clear();
            foreach (var v in list) 
            {
                if (v.ResNum == resNum)
                {
                    string serviceName = ServiceList.Single(x => (x.ServiceId == v.SerId)).ServiceName;                    
                    ServiceCommands.Add(new DataCommandViewModel<ReservedServiceVo>(serviceName,new Command(RemoveRS),v));
                }
            }
        }

        private void RemoveRS(object obj)
        {
            ReservedServiceVo rsv = (ReservedServiceVo)obj;
            _reservedServiceRepository.RemoveReservedService(rsv.ResNum, rsv.SerId);
            ServiceCommands.Remove(ServiceCommands.Single(x => (x.Data == rsv)));
        }
        #endregion



    }
}
