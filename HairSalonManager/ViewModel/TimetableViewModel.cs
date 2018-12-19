using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        //public event EventHandler<System.Windows.Controls.SelectionChangedEventArgs> DetectChangedDate;

        #endregion

        #region property

        private DateTime _selectedDate = DateTime.Today;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
                DetectChangedDate(value);
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
        
        
        #endregion //property

        #region ctor
        public TimetableViewModel()
        {
            DateTime _selectedDate = new DateTime();
            _selectedDate = DateTime.Today;

            _timetableRepository = TimetableRepository.TR;
            _reservationRepository = ReservationRepository.Rr;
            _reservedServiceRepository = ReservedServiceRepository.RSR;
            _stylistRepository = StylistRepository.SR;

            TimeTableList = new ObservableCollection<TimeTableVo>(_timetableRepository.GetTimeTables());
            ReservationList = new ObservableCollection<ReservationVo>(_reservationRepository.GetReservations());
            StylistList = new ObservableCollection<StylistVo>(_stylistRepository.GetStylistsFromLocal());

            MakeTimeTable(_selectedDate);

        }

        #endregion

        #region method

        public void MakeTimeTable(DateTime selectedDate)
        {
            _col = _dataTable.Columns.Add();
            _col.ColumnName = "StylistName";

            for (int i = 0; i < 48; i++)
            {
                _col = _dataTable.Columns.Add();
                _col.ColumnName = (i / 2).ToString("D2") + " : " + (i % 2 * 30).ToString("D2");
            }

            ShowTimeTable(selectedDate);
        }

        public void ShowTimeTable(DateTime selectedDate)
        {
            IEnumerable<ReservationVo> necessaryList;

            for (int k = 0; k < StylistList.Count; k++) //미용사 리스트를 가져와서 한명씩 실행
            {
                _row = _dataTable.NewRow(); //DataRow를 생성해서 그 사람의 예약 테이블을 채워야지
                _row["StylistName"] = StylistList[k].StylistName;

                //예약 목록 중 StylistId와 StylistList[k].StylistId가 일치하는 사람 찾아서 예약목록 불러오기
                necessaryList = ReservationList.Where(x => x.StylistId == StylistList[k].StylistId);

                //이 목록 중에서 선택한 날짜만 다시 불러오기
                necessaryList = necessaryList.Where(x => x.StartAt.ToString("d").Equals(selectedDate.ToString("d")));


                SaveResInColumn(necessaryList);
            }
        }

        public void SaveResInColumn(IEnumerable<ReservationVo> necessaryList)
        {
            //각 예약을 집어넣기
            foreach (var item in necessaryList)
            {
                TimeSpan ts = item.EndAt - item.StartAt;
                int result = (ts.Hours * 60) + ts.Minutes;
                int block = result / 30;
                int i = 0;

                foreach (DataColumn column in _dataTable.Columns)
                {
                    if (column.ColumnName.Equals(item.StartAt.Hour.ToString("D2") + " : " + item.StartAt.Minute.ToString("D2")))
                    {
                        for (int j = 0; j < block; j++)
                        {
                            _row[i] = item.ResNum;
                            i++;
                        }
                    }
                    i++;
                }
            }
            _dataTable.Rows.Add(_row);
        }
        

        //이벤트
        private void DetectChangedDate(DateTime date)
        {
            if (date == null)
            {
                date = SelectedDate;
            }
            else
            {
                _dataTable.Clear();
                ShowTimeTable(date);
            }
            //var picker = sender as DatePicker;
            //DateTime? date = picker.SelectedDate;
            //if (date == null)
            //{
            //    date = SelectedDate;
            //}
            //else
            //{
            //    ShowTimeTable(date.Value);
            //}
        }

        #endregion
    }
}