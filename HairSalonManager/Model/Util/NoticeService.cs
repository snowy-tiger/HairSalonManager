using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
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

        uint _recentNum;

        ReservationRepository _rr;
        

        private NoticeService()
        {
            _rr = ReservationRepository.Rr;
            _recentNum = _rr.RecentResNum;
            MessageBox.Show($"{_recentNum}");
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += Timer_tick;
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
            MessageBox.Show($"{_rr.RecentResNum}, {_recentNum}");
            if (_rr.RecentResNum != _recentNum)
            {             
                _recentNum = _rr.RecentResNum;
                ShowMessage();
            }                                   
        }

        private void ShowMessage() //알림을 나타내는 메소드
        {
            MessageBox.Show("새로운 예약이 도착했습니다.");
        }
    }
}
