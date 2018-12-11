using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.ViewModel
{
    class AllReservationViewModel : Notifier
    {
        private int _resNum;

        public int ResNum
        {
            get { return _resNum; }
            set {
                _resNum = value;
                OnPropertyChanged("ResNum");
            }
        }

        private int _stylistId;

        public int StylistId
        {
            get { return _stylistId; }
            set {
                _stylistId = value;
                OnPropertyChanged("StylistId");
            }
        }

        private string _userTel;

        public string UserTel
        {
            get { return _userTel; }
            set {
                _userTel = value;
                OnPropertyChanged("UserTel");
            }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set {
                _note = value;
                OnPropertyChanged("Note");
            }
        }

        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set {
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private DateTime _userBirthday;

        public DateTime UserBirthday
        {
            get { return _userBirthday; }
            set {
                _userBirthday = value;
                OnPropertyChanged("UserBirthday");
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

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private bool _isPaid;

        public bool IsPaid
        {
            get { return _isPaid; }
            set {
                _isPaid = value;
                OnPropertyChanged("IsPaid");
            }
        }

        public AllReservationViewModel()
        {

        }
    }
}
