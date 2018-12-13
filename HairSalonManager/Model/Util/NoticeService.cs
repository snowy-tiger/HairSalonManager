using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HairSalonManager.Model.Util
{
    class NoticeService //예약 알림을 띄우는 클래스
    {
        private static NoticeService _ns;

        public static NoticeService NS
        {
            get
            {
                if (_ns == null)
                {
                    _ns = new NoticeService();
                }
                return _ns;
            }        
        }

        DispatcherTimer _timer;

        ObservableCollection<ReservationVo> _list;

        private NoticeService()
        {            
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += Timer_tick;
        }

        public void StartNoticeService(ref ObservableCollection<ReservationVo> list) //알림 서비스를 시작함
        {
            _list = list;
            _timer.Start();
        }

        public void StopNoticeService() //알림 서비스를 종료함
        {
            _timer.Stop();
        }
        private void Timer_tick(object sender, EventArgs e)
        {
            HasNewReservation(ref _list);
        }

        private void HasNewReservation(ref ObservableCollection<ReservationVo> list)
        {
            ReservationRepository rr = ReservationRepository.Rr;

            List<ReservationVo> recentList;
           
            if ((recentList = rr.GetReservations(rr.RecentResNum)) != null)
            {
                foreach (ReservationVo rv in recentList)
                {
                    list.Add(rv);
                }
                ShowMessage();//알림을 나타내는 메소드
            }                    
        }

        private void ShowMessage() //알림을 나타내는 메소드
        {
            
        }
    }
}
