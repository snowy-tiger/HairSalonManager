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
    class TimetableRepository : BaseRepository  //연결형
    {
        #region singleTon
        private TimetableRepository _tr;
       
        public TimetableRepository TR
        {
            get
            {
                if (_tr == null)
                    _tr = new TimetableRepository();
                return _tr;
            }            
        }

        private TimetableRepository()
        {

        }
        #endregion

        #region Timetable Methods
        private DataSet _ds;

        public List<TimeTableVo> GetTimeTables()
        {
            List<TimeTableVo> list = new List<TimeTableVo>();
            _sql = "SELECT * FROM timetable";
            _conn.Msc.Open();           
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                TimeTableVo tv = new TimeTableVo();
                tv.ResNum = (int) rdr["resNum"];
                tv.StylistId = (int)rdr["stylistId"];
                tv.StartAt = rdr["startAt"] as DateTime?;
                tv.EndAt = rdr["endAt"] as DateTime?;
                list.Add(tv);
            }
            _conn.Msc.Close();
            return list;
        }
        #endregion
    }
}
