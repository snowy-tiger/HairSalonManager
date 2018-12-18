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
            _list = new List<ReservationVo>();
            //RecentResNum = 0;            
        }
        #endregion

        #region Fields

        private List<ReservationVo> _list;

        public List<ReservationVo> List
        {
            get { return _list; }
            set { _list = value; }
        }


        public uint RecentResNum { get; set; }

        #endregion

        #region Reservation Methods
        public List<ReservationVo> GetReservations() //전체의 예약리스트를 가져옴.
        {           
            List<ReservationVo> list = new List<ReservationVo>();
            _conn.Msc.Open();
            _sql = "SELECT * FROM RESERVATION WHERE isPaid = 0";
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ReservationVo rv = new ReservationVo();
                rv.ResNum = (uint)rdr["resNum"];
                rv.EndAt = rdr["endAt"] as DateTime?;
                rv.Gender = (bool)rdr["gender"] ? 1 : 0;
                rv.IsPaid = (bool)rdr["isPaid"];
                rv.Note = rdr["note"] as string;
                rv.StartAt = rdr["startAt"] as DateTime?;
                rv.StylistId = (uint)rdr["stylistId"];
                rv.UserBirthday = rdr["userBirthday"] as DateTime?;
                rv.UserName = rdr["userName"] as string;
                rv.UserTel = rdr["userTel"] as string;
                list.Add(rv);
                RecentResNum = rv.ResNum;
            }            
            _conn.Msc.Close();
            List = list;
            return list;
        }

        public List<ReservationVo> GetReservations(uint recentResNum) //추가적인 예약정보를 가져옴 
        {
            List<ReservationVo> list = new List<ReservationVo>();
            _conn.Msc.Open();
            _sql = $"SELECT * FROM RESERVATION WHERE isPaid = 0 AND resNum > {recentResNum} ";
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ReservationVo rv = new ReservationVo();
                rv.ResNum = (uint)rdr["resNum"];
                rv.EndAt = rdr["endAt"] as DateTime?;
                rv.Gender = (bool)rdr["gender"] ? 1 : 0;
                rv.IsPaid = (bool)rdr["isPaid"];
                rv.Note = rdr["note"] as string;
                rv.StartAt = rdr["startAt"] as DateTime?;
                rv.StylistId = (uint)rdr["stylistId"];
                rv.UserBirthday = rdr["userBirthday"] as DateTime?;
                rv.UserName = rdr["userName"] as string;
                rv.UserTel = rdr["userTel"] as string;
                list.Add(rv);               
            }           
            _conn.Msc.Close();            
            return list;
        }
        

        public uint InsertReservation(ReservationVo rv) //예약 추가
        {
            _conn.Msc.Open();
            _sql = "INSERT INTO reservation(stylistId,note,gender,userBirthday,startAt,endAt,userName,userTel) VALUES(@stylistId,@note,@gender,@userBirthday,@startAt,@endAt,@userName,@userTel)";

            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            cmd.Parameters.AddWithValue("@stylistId", rv.StylistId);            
            cmd.Parameters.AddWithValue("@userTel", rv.UserTel);
            cmd.Parameters.AddWithValue("@note", rv.Note);
            cmd.Parameters.AddWithValue("@gender", rv.Gender);
            cmd.Parameters.AddWithValue("@userBirthday", rv.UserBirthday);
            cmd.Parameters.AddWithValue("@startAt", rv.StartAt);
            cmd.Parameters.AddWithValue("@endAt", rv.EndAt);
            cmd.Parameters.AddWithValue("@userName", rv.UserName);
           
            if (cmd.ExecuteNonQuery() == -1) //실패시
            {
                _conn.Msc.Close();
                return 0;
            }

            uint recentId = (uint)cmd.LastInsertedId;
            _conn.Msc.Close();
            return recentId; //성공시
        }

        public bool UpdateReservation(ReservationVo rv) //예약 수정
        {
            _conn.Msc.Open();
            _sql = "UPDATE reservation SET stylistId = @stylistId, userTel = @userTel, " +
                "note = @note, gender = @gender, userBirthday = @userBirthday, startAt = @startAt, endAt = @endAt, userName = @userName WHERE resNum = @resNum";
            
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            cmd.Parameters.AddWithValue("@resNum", rv.ResNum);
            cmd.Parameters.AddWithValue("@stylistId", rv.StylistId);
            cmd.Parameters.AddWithValue("@userTel", rv.UserTel);
            cmd.Parameters.AddWithValue("@note", rv.Note);
            cmd.Parameters.AddWithValue("@gender", rv.Gender);
            cmd.Parameters.AddWithValue("@userBirthday", rv.UserBirthday);
            cmd.Parameters.AddWithValue("@startAt", rv.StartAt);
            cmd.Parameters.AddWithValue("@endAt", rv.EndAt);
            cmd.Parameters.AddWithValue("@userName", rv.UserName);

            if (cmd.ExecuteNonQuery() == -1) //실패시
            {
                _conn.Msc.Close();
                return false;
            }
            _conn.Msc.Close();
            return true; //성공시
        }

        public bool RemoveReservation(uint resNum) //예약 삭제
        {
            bool isRecent = false;

            if (resNum == GetRecentNum()) //최근예약이 삭제시 최근 num을 삭제된 전의 resNum으로 바꿔야함
            {
                isRecent = true;
            }

            _conn.Msc.Open();
            _sql = $"DELETE FROM reservation WHERE resNum = {resNum}";
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);
            
            if (cmd.ExecuteNonQuery() == -1) //실패시
            {
                _conn.Msc.Close();
                return false;                
            }
            
            _conn.Msc.Close();

            if (isRecent) //삭제된게 맨 마지막 번호일시
            {
                RecentResNum = GetRecentNum();
            }

            return true; //성공시

        }

        public uint GetRecentNum()
        {
            uint recentResNum = 0;

            _conn.Msc.Open();

            _sql = "SELECT resNum FROM reservation ORDER BY resNum DESC LIMIT 1";

            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                recentResNum = (uint)reader["resNum"];
            }

            _conn.Msc.Close();

            return recentResNum;
        }
        #endregion 
    }
}
