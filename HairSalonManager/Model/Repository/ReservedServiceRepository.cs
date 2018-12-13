using HairSalonManager.Model.Vo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Repository
{
    class ReservedServiceRepository : BaseRepository
    {
        #region field
        readonly List<ReservedServiceVo> _list;
        #endregion

        #region ctor
        public ReservedServiceRepository()
        {
            _list = GetReservedServices();
        }
        #endregion

        #region RerservedService Method
        public List<ReservedServiceVo> GetReservedServicesFromLocal()
        {
            return new List<ReservedServiceVo>(_list);
        }


        public List<ReservedServiceVo> GetReservedServices()
        {
            List<ReservedServiceVo> list = new List<ReservedServiceVo>(); 
            _sql = "SELECT * FROM reservedservice";
            MySqlCommand cmd = new MySqlCommand(_sql,_conn.Msc);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ReservedServiceVo rsv = new ReservedServiceVo();
                rsv.ResNum = (int) rdr["resNum"];
                rsv.SerId = (int)rdr["serId"];
                list.Add(rsv);
            }
            return list;
        }

        //public bool UpdateReservedService(ReservedServiceVo rsv)
        //{
        //    _sql = "UPDATE reservedservice SET serId = @serId Where resNuM = @resNum AND serId = @serId";
        //    MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

        //    cmd.Parameters.Add("@serId",rsv.)
        //    Mysql
        //}

        public bool RemoveReservedService(int resNum,int serId)
        {
            _sql = "DELETE FROM reservedservice WHERE resNum = @resNum AND serId = @serId";
            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            cmd.Parameters.Add("@resNum", resNum);
            cmd.Parameters.Add("@serId", serId);

            if (cmd.ExecuteNonQuery() != -1)
                return true;
            return false; 

        }
        #endregion
    }
}
