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
        private Connection _conn;

        private static ReservationRepository _resR = new ReservationRepository();

        private string _sql;

        private DataSet _ds;

        private ReservationRepository()
        {
            _conn = Connection.Conn;
        }

        public static ReservationRepository ResR { get; } //singleton

        public List<ReservationVo> GetReservations()
        {
            _ds = new DataSet();
            List<ReservationVo> list = new List<ReservationVo>();
            _sql = "SELECT * FROM RESERVATION WHERE isPaid == 0";
            MySqlDataAdapter adpter = new MySqlDataAdapter(_sql, _conn.Msc);
            adpter.Fill(_ds, "Reservation");

            foreach (DataRow r in _ds.Tables[0].Rows)
            {
                ReservationVo rv = new ReservationVo();
                rv.ResNum = (int) r["resNum"];
                rv.EndAt = r["endAt"] as DateTime?;
                rv.Gender = r["gender"] as int?;
                rv.IsPaid = (bool)r["isPaid"];
                rv.Note = r["note"] as string;
                rv.StartAt = r["startAt"] as DateTime?;
                rv.StylistId = (int)r["stylistId"];
                rv.UserBirthday = r["userBirthday"] as DateTime?;
                rv.UserName = r["userName"] as string;
                rv.UserTel = r["userTel"] as string;
                list.Add(rv);
            }
            return list;
        }

        

    }
}
