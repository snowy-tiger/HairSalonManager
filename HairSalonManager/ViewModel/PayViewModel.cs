using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.ViewModel
{
    class PayViewModel : ViewModelBase
    {
        #region field
        private readonly ReservationRepository _reservationRepository;
        private readonly ReservedServiceRepository _reservedServiceRepository;
        private readonly LedgerRepository _ledgerRepository;
        private readonly UserRepository _userRepository;

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
            _resList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            _serviceList = new List<ServiceVo>(ServiceRepository.SR.GetServicesFromLocal());
            _userList = new List<UserVo>(UserRepository.UR.GetUserList());

            _sum = 0;
            _selRes = new ReservationVo();
            InsertCommand = new Command(ExcuteInsertMethod);
        }
        
        #endregion


        #region property

        private readonly ObservableCollection<ReservationVo> _resList;

        public ObservableCollection<ReservationVo> ResList
        {
            get { return _resList; }
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

        private int _consumePoint;

        public int ConsumePoint
        {
            get { return _consumePoint; }
            set { _consumePoint = value;
                OnPropertyChanged("ConsumePoint");
            }
        }

        public Command InsertCommand { get; set; }

        #endregion

        #region method
        private void onSelResChanged()
        {
            ResServiceList = new ObservableCollection<ReservedServiceVo>(_reservedServiceRepository.GetReservedServices(SelRes.ResNum));
            foreach(ReservedServiceVo rsv in ResServiceList)
            {
                Sum += _serviceList.Single(x => x.ServiceId == rsv.SerId).ServicePrice;
            }
            Point = Sum / 10;
            UserPoint = _userList.Single(x => x.UserTel == SelRes.UserTel).Point;
        }

        private void ExcuteInsertMethod(object obj)
        {
            LedgerVo l = new LedgerVo();
            l.ResNum = SelRes.ResNum;
            l.Sum = Sum;
            _ledgerRepository.InsertLedger(l);

            UserVo user = _userList.Single(x => x.UserTel == SelRes.UserTel);
            user.Point += Point;
            _userRepository.UpdateUser(user);

            ReservationVo r = ResList.Single(x => x.ResNum == SelRes.ResNum);
            r.IsPaid = true;
            _reservationRepository.UpdateReservation(r);

        }

        #endregion
    }
}
