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
        private List<ServiceVo> _serviceList;
        #endregion

        #region ctor

        public PayViewModel()
        {
            _reservationRepository = ReservationRepository.Rr;
            _reservedServiceRepository = ReservedServiceRepository.RSR;            
            _resList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            _serviceList = new List<ServiceVo>(ServiceRepository.SR.GetServicesFromLocal());
            _sum = 0;
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

        #endregion

        #region method
        private void onSelResChanged()
        {
            ResServiceList = new ObservableCollection<ReservedServiceVo>(_reservedServiceRepository.GetReservedServices(SelRes.ResNum));
            foreach(ReservedServiceVo rsv in ResServiceList)
            {
                Sum += _serviceList.Single(x => x.ServiceId == rsv.SerId).ServicePrice;
            }
        }

        #endregion
    }
}
