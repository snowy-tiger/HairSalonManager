using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using HairSalonManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
     
        ReservationRepository _rr;
        
        private bool _isNewReservationExistent;

        public bool IsNewReservationExistent
        {
            get { return _isNewReservationExistent; }
            set { _isNewReservationExistent = value; }
        }

        private NoticeService()
        {
            _rr = ReservationRepository.Rr;          
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += Timer_tick;
            _isNewReservationExistent = false;
        }

        public void StartNoticeService() //알림 서비스를 시작함
        {          
            _timer.Start();            
        }

        public void StopNoticeService() //알림 서비스를 종료함
        {
            _timer.Stop();
        }
        private void Timer_tick(object sender, EventArgs e)
        {
            HasNewReservation();
        }

        private void HasNewReservation()
        {     
            if (_rr.RecentResNum != _rr.GetRecentNum())
            {
                MessageBox.Show("새로운 예약이 도착했습니다.");             
                IsNewReservationExistent = true;
            }                                   
        }
       
    }
}
