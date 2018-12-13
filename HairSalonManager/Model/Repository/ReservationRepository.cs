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
    class ReservationRepository : BaseRepository
    {
        #region singleTon 
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
            _list = GetReservations();
        }
        #endregion

        #region Fields

        private static int _recentResNum;

        readonly List<ReservationVo> _list;

        
        public int RecentResNum {
            get
            {
                return _recentResNum;
            }
            private set
            {
                _recentResNum = value;
            }
        }
        #endregion
        
        #region Reservation Methods
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

        public List<ReservationVo> GetReservationsFromLocal() //미리 받아놓은 리스트를 준다.
        {
            return new List<ReservationVo>(_list);
        }

        public bool InsertReservation(ReservationVo rv) //예약 추가
        {
            _conn.Msc.Open();
            _sql = "INSERT INTO reservation(resNum,stylistId,note,gender,userBirthday,startAt,endAt,userName) VALUES(@resNum,@resNum,@stylistId,@note,@gender,@userBirthday,@startAt,@endAt,@userName)";

            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            cmd.Parameters.Add("@resNum", rv.ResNum);
            cmd.Parameters.Add("@stylistId", rv.StylistId);
            cmd.Parameters.Add("@userTel", rv.UserTel);
            cmd.Parameters.Add("@note", rv.Note);
            cmd.Parameters.Add("@gender", rv.Gender);
            cmd.Parameters.Add("@userBirthday", rv.UserBirthday);
            cmd.Parameters.Add("@startAt", rv.StartAt);
            cmd.Parameters.Add("@endAt", rv.EndAt);
            cmd.Parameters.Add("@userName", rv.UserName);

            if (cmd.ExecuteNonQuery() == -1) //실패시
            {
                return false;
            }

            return true; //성공시
        }

        public bool UpdateReservation(ReservationVo rv) //예약 수정
        {
            _conn.Msc.Open();
            _sql = "UPDATE reservation SET stylistId = @stylistId, userTel = @userTel, " +
                "note = @note, gender = @gender, userBirthday = @userBirthday, startAt = @startAt, endAt = @endAt, userName = @userName WHERE resNum = @resNum";
            
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            cmd.Parameters.Add("@resNum", rv.ResNum);
            cmd.Parameters.Add("@stylistId", rv.StylistId);
            cmd.Parameters.Add("@userTel", rv.UserTel);
            cmd.Parameters.Add("@note", rv.Note);
            cmd.Parameters.Add("@gender", rv.Gender);
            cmd.Parameters.Add("@userBirthday", rv.UserBirthday);
            cmd.Parameters.Add("@startAt", rv.StartAt);
            cmd.Parameters.Add("@endAt", rv.EndAt);
            cmd.Parameters.Add("@userName", rv.UserName);

            if (cmd.ExecuteNonQuery() == -1) //실패시
            {
                return false;
            }

            return true; //성공시
        }

        public bool RemoveReservation(int resNum) //예약 삭제
        {
            _conn.Msc.Open();
            _sql = $"DELETE FROM reservation WHERE resNum = {resNum}";
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            if (cmd.ExecuteNonQuery() == -1) //실패시
            {
                return false;
            }

            return true; //성공시
        }
        #endregion 
    }
}
