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
    class StylistRepository : BaseRepository
    {
        DataSet _ds;

        private static StylistRepository _stylistRepository;

        public static StylistRepository SR
        {
            get {
                if (_stylistRepository == null)
                    _stylistRepository = new StylistRepository();
                return _stylistRepository;
            }
        }

        private StylistRepository()
        {

        }

        public List<StylistVo> GetStylists()
        {
            List<StylistVo> list = new List<StylistVo>();

            _ds = new DataSet();
            _sql = "SELECT * FROM stylist";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql,_conn.Msc);

            adapter.Fill(_ds, "stylist");

            foreach (DataRow r in _ds.Tables[0].Rows)
            {
                StylistVo s = new StylistVo();
                s.StylistId = (uint)r["stylistId"];
                s.StylistName = r["stylistName"] as string;
                s.AdditionalPrice = (uint)r["additionalPrice"];
                s.PersonalDay = (byte)r["personalDay"];
                list.Add(s);
            }

            return list;
        }

    }
}
