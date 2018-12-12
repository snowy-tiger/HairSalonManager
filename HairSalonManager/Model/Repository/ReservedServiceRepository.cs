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

        public void UpdateReservedService()
        {

        }

        public void RemoveReservedService()
        {

        }

    }
}
