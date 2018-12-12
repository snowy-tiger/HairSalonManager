using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.ViewModel
{
    class TimetableViewModel : ViewModelBase
    {
        #region prop
        private int _stylistId;

        public int StylistId
        {
            get { return _stylistId; }
            set {
                _stylistId = value;
                OnPropertyChanged("StylistId");
            }
        }

        private string _stylistName;

        public string StylistName
        {
            get { return _stylistName; }
            set {
                _stylistName = value;
                OnPropertyChanged("StylistName");
            }
        }

        private int _resName;

        public int ResName
        {
            get { return _resName; }
            set {
                _resName = value;
                OnPropertyChanged("ResNum");
            }
        }

        private DateTime _startAt;

        public DateTime StartAt
        {
            get { return _startAt; }
            set {
                _startAt = value;
                OnPropertyChanged("StartAt");
            }
        }

        private DateTime _endAt;

        public DateTime EndAt
        {
            get { return _endAt; }
            set {
                _endAt = value;
                OnPropertyChanged("EndAt");
            }
        }

        private int _operationTime;

        public int OperationTime
        {
            get { return _operationTime; }
            set {
                _operationTime = value;
                OnPropertyChanged("OperationTime");
            }
        }

        private int _serId;

        public int SerId
        {
            get { return _serId; }
            set {
                _serId = value;
                OnPropertyChanged("SerId");
            }
        }

        private string _serName;

        public string SerName
        {
            get { return _serName; }
            set {
                _serName = value;
                OnPropertyChanged("SerName");
            }
        }
        #endregion //prop

        public TimetableViewModel()
        {

        }
    }
}
    