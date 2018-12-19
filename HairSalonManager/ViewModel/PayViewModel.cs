using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HairSalonManager.ViewModel
{
    class PayViewModel : ViewModelBase
    {
        #region field
        private readonly ReservationRepository _reservationRepository;
        private readonly ReservedServiceRepository _reservedServiceRepository;
        private readonly LedgerRepository _ledgerRepository;
        private readonly UserRepository _userRepository;
        private readonly StylistRepository _stylistRepository;

        private List<ServiceVo> _serviceList;
        private List<UserVo> _userList;
        #endregion

        #region ctor

        public PayViewModel()
        {
            _reservationRepository = ReservationRepository.Rr;
            _reservedServiceRepository = ReservedServiceRepository.RSR;
            _ledgerRepository = LedgerRepository.LR;
            _userRepository = UserRepository.UR;
            _stylistRepository = StylistRepository.SR;
            _resList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            _serviceList = new List<ServiceVo>(ServiceRepository.SR.GetServicesFromLocal());
            _userList = new List<UserVo>(UserRepository.UR.GetUserList());

            _sum = 0;
            _stylistadditionalCost = 0;
            _consumePoint = 0;
            _selRes = new ReservationVo();
            _selRes.StylistId = 0;

            InsertCommand = new Command(ExcuteInsertMethod);
        }
        
        #endregion


        #region property

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

        private ObservableCollection<ReservedServiceVo> _resServiceList;

        public ObservableCollection<ReservedServiceVo> ResServiceList
        {
            get { return _resServiceList; }
            set { _resServiceList = value; }
        }

        private ReservationVo _selRes;

        public ReservationVo SelRes
        {
            get { return _selRes; }
            set {
                _selRes = value;
                OnPropertyChanged("SelRes");
                onSelResChanged();
                     }
        }

        private uint _sum;

        public uint Sum
        {
            get { return _sum; }
            set { _sum = value;
                OnPropertyChanged("Sum");
            }
        }

        private uint _point;

        public uint Point
        {
            get { return _point; }
            set { _point = value;
                OnPropertyChanged("Point");
            }
        }

        private uint _userPoint;

        public uint UserPoint
        {
            get { return _userPoint; }
            set { _userPoint = value;
                OnPropertyChanged("UserPoint");
            }
        }

        private uint _consumePoint;

        public uint ConsumePoint
        {
            get { return _consumePoint; }
            set { _consumePoint = value;
                OnPropertyChanged("ConsumePoint");
            }
        }

        private uint? _stylistadditionalCost;

        public uint? StylistAdditionalCost
        {
            get { return _stylistadditionalCost; }
            set {
                _stylistadditionalCost = value;
                OnPropertyChanged("StylistAdditionalCost");
            }
        }

        public Command InsertCommand { get; set; }

        #endregion

        #region method
        private void onSelResChanged()
        {
            Sum = 0;
            //if (SelRes == null)
            //{
            //    SelRes = new ReservationVo();
            //}
            ResServiceList = new ObservableCollection<ReservedServiceVo>(_reservedServiceRepository.GetReservedServices(SelRes.ResNum));
            foreach(ReservedServiceVo rsv in ResServiceList)
            {
                Sum += _serviceList.Single(x => x.ServiceId == rsv.SerId).ServicePrice;
            }
            
            Point = Sum / 10;
            UserPoint = _userList.Single(x => x.UserTel == SelRes.UserTel).Point;
            StylistAdditionalCost = _stylistRepository.GetStylistsFromLocal().Single(x => x.StylistId == SelRes.StylistId).AdditionalPrice;
            Sum += (uint)StylistAdditionalCost;
            Sum -= ConsumePoint;
        }

        private void ExcuteInsertMethod(object obj)
        {
            if (SelRes.StylistId == 0)
            {
                MessageBox.Show("선택된 예약이 없습니다.");
                return;
            }
            LedgerVo l = new LedgerVo();
            
            if (UserPoint < ConsumePoint)
            {
                MessageBox.Show("적립금이 부족합니다.");
                return;
            }

            if (ConsumePoint > Sum)
            {
                MessageBox.Show("사용할 적립금이 실제 가격보다 더 많습니다.");
                return;
            }
            l.ResNum = SelRes.ResNum;
            l.Sum = Sum;
            _ledgerRepository.InsertLedger(l);

            UserVo user = _userList.Single(x => x.UserTel == SelRes.UserTel);
            user.Point += Point;            
            user.Point -= ConsumePoint;
            _userRepository.UpdateUser(user);

            ReservationVo r = ResList.Single(x => x.ResNum == SelRes.ResNum);
            r.IsPaid = true;
            _reservationRepository.UpdateReservation(r);
           // ResList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            

        }

        #endregion
    }
}
