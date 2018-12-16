using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.ViewModel
{
    class TimetableViewModel : ViewModelBase
    {
        #region field
        readonly TimetableRepository _timetableRepository;

        readonly ReservationRepository _reservationRepository;

        readonly ReservedServiceRepository _reservedServiceRepository;

        readonly StylistRepository _stylistRepository;

        DataTable _dataTable = new DataTable();

        DataRow _row;

        DataColumn _col;

        #endregion

        #region property

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged("DataTable");
            }
        }

        private ObservableCollection<TimeTableVo> _timeTableList;

        public ObservableCollection<TimeTableVo> TimeTableList
        {
            get { return _timeTableList; }
            set { _timeTableList = value; }
        }

        private ObservableCollection<ReservationVo> _reservationList;

        public ObservableCollection<ReservationVo> ReservationList
        {
            get { return _reservationList; }
            set
            {
                _reservationList = value;
                OnPropertyChanged("ReservationList");
            }
        }

        private ObservableCollection<StylistVo> _stylistList;

        public ObservableCollection<StylistVo> StylistList
        {
            get { return _stylistList; }
            set { _stylistList = value; }
        }

        private int _stylistId;

        public int StylistId
        {
            get { return _stylistId; }
            set
            {
                _stylistId = value;
                OnPropertyChanged("StylistId");
            }
        }

        private uint _resNum;

        public uint ResNum
        {
            get { return _resNum; }
            set
            {
                _resNum = value;
                OnPropertyChanged("ResNum");
            }
        }

        private DateTime _startAt;

        public DateTime StartAt
        {
            get { return _startAt; }
            set
            {
                _startAt = value;
                OnPropertyChanged("StartAt");
            }
        }

        private DateTime _endAt;

        public DateTime EndAt
        {
            get { return _endAt; }
            set
            {
                _endAt = value;
                OnPropertyChanged("EndAt");
            }
        }

        private int _operationTime;

        public int OperationTime
        {
            get { return _operationTime; }
            set
            {
                _operationTime = value;
                OnPropertyChanged("OperationTime");
            }
        }

        private int _serId;

        public int SerId
        {
            get { return _serId; }
            set
            {
                _serId = value;
                OnPropertyChanged("SerId");
            }
        }

        private string _selectedTime;

        public string SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
                OnPropertyChanged("SelectedTime");
            }
        }

        public Command CheckCommand { get; set; }
        #endregion //property

        #region ctor
        public TimetableViewModel()
        {
            SelectedDate = DateTime.Today;

            _timetableRepository = TimetableRepository.TR;
            _reservationRepository = ReservationRepository.Rr;
            _reservedServiceRepository = ReservedServiceRepository.RSR;
            _stylistRepository = StylistRepository.SR;

            TimeTableList = new ObservableCollection<TimeTableVo>(_timetableRepository.GetTimeTables());
            ReservationList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            StylistList = new ObservableCollection<StylistVo>(_stylistRepository.GetStylistsFromLocal());

            ShowTimeTable();

            CheckCommand = new Command(ExecuteCheckMethod, CanExecuteMethod);
        }

        #endregion

        #region method
        public void ShowTimeTable()
        {
            IEnumerable<ReservationVo> necessaryList;

            _col = _dataTable.Columns.Add();
            _col.ColumnName = "StylistName";

            for (int i = 0; i < 48; i++)
            {
                _col = _dataTable.Columns.Add();
                _col.ColumnName = (i / 2).ToString("D2") + " : " + (i % 2 * 30).ToString("D2");
            }

            for (int k = 0; k < StylistList.Count; k++) //미용사 리스트를 가져와서 한명씩 실행
            {
                _row = _dataTable.NewRow(); //DataRow를 생성해서 그 사람의 예약 테이블을 채워야지
                _row["StylistName"] = StylistList[k].StylistName;

                //예약 목록 중 StylistId와 StylistList[k].StylistId가 일치하는 사람 찾아서 예약목록 불러오기
                necessaryList = ReservationList.Where(x => x.StylistId == StylistList[k].StylistId);

                //이 목록 중에서 선택한 날짜만 다시 불러오기
                necessaryList = necessaryList.Where(x => x.StartAt.Value.ToString("d").Equals(SelectedDate.ToString("d")));

                //각 예약을 집어넣기
                foreach (var item in necessaryList)
                {
                    TimeSpan ts = item.EndAt.Value - item.StartAt.Value;
                    int result = (ts.Hours * 60) + ts.Minutes;
                    int block = result / 30;

                    for (int i = 0; i < 48; i++)
                    {
                        if (_col.ColumnName.Equals(item.StartAt.Value.Hour + " : " + item.StartAt.Value.Minute))
                        {
                            for (int j = 0; j < block; j++)
                            {
                                _row[i + 1] = item.ResNum;
                                i++;
                            }
                        }
                    }
                }
                _dataTable.Rows.Add(_row);
            }
        }



        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        private void ExecuteCheckMethod(object obj)
        {
            _reservedServiceRepository.GetReservedServices(ResNum);
        }

        #endregion
    }
}