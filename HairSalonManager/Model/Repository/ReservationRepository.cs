using HairSalonManager.Model.Vo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Repository
{
    class ReservationRepository
    {
        private static ReservationRepository _rr;

        public static ReservationRepository Rr
        {
            get
            {
                if (_rr == null)
                    _rr = new ReservationRepository();
                return _rr;
            }
           
        }

        private ReservationRepository()
        {
            _conn = Connection.Conn;
            RecentResNum = 0;
        }

        private Connection _conn;

        private string _sql;

        private static int _recentResNum;

        public int RecentResNum {
            get
            {
                return _recentResNum;
            }
            private set;
        }

        public List<ReservationVo> GetReservations() //전체의 예약리스트를 가져옴.
        {           
            List<ReservationVo> list = new List<ReservationVo>();
            _conn.Msc.Open();
            _sql = "SELECT * FROM RESERVATION WHERE isPaid == 0";
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ReservationVo rv = new ReservationVo();
                rv.ResNum = (int)rdr["resNum"];
                rv.EndAt = rdr["endAt"] as DateTime?;
                rv.Gender = rdr["gender"] as int?;
                rv.IsPaid = (bool)rdr["isPaid"];
                rv.Note = rdr["note"] as string;
                rv.StartAt = rdr["startAt"] as DateTime?;
                rv.StylistId = (int)rdr["stylistId"];
                rv.UserBirthday = rdr["userBirthday"] as DateTime?;
                rv.UserName = rdr["userName"] as string;
                rv.UserTel = rdr["userTel"] as string;
                list.Add(rv);
                RecentResNum = rv.ResNum; //계속 최근 받아온 예약번호를 저장한다 --> 추가적인 예약알람을 위해
            }
            _conn.Msc.Close();
            return list;
        }

        public List<ReservationVo> GetReservations(int recentResNum) //추가적인 예약정보를 가져옴 
        {
            List<ReservationVo> list = new List<ReservationVo>();
            _conn.Msc.Open();
            _sql = $"SELECT * FROM RESERVATION WHERE isPaid == 0 AND resNum > {recentResNum} ";
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ReservationVo rv = new ReservationVo();
                rv.ResNum = (int)rdr["resNum"];
                rv.EndAt = rdr["endAt"] as DateTime?;
                rv.Gender = rdr["gender"] as int?;
                rv.IsPaid = (bool)rdr["isPaid"];
                rv.Note = rdr["note"] as string;
                rv.StartAt = rdr["startAt"] as DateTime?;
                rv.StylistId = (int)rdr["stylistId"];
                rv.UserBirthday = rdr["userBirthday"] as DateTime?;
                rv.UserName = rdr["userName"] as string;
                rv.UserTel = rdr["userTel"] as string;
                list.Add(rv);
                RecentResNum = rv.ResNum;
            }
            _conn.Msc.Close();
            return list;
        }

    }
}
