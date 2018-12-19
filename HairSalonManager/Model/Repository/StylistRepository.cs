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

        readonly List<StylistVo> _list;

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
            _sql = "SELECT * FROM stylist";
            _list = GetStylists();            
        }

        public List<StylistVo> GetStylistsFromLocal()
        {
            return new List<StylistVo>(_list);
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

        public bool InsertStylist(StylistVo s)
        {       

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];
            DataRow row = table.NewRow();

            row["stylistId"] = s.StylistId;
            row["stylistName"] = s.StylistName;
            row["additionalPrice"] = s.AdditionalPrice;
            row["personalDay"] = s.PersonalDay;

            table.Rows.Add(row);
          
            if (Save(adapter) == -1)
                return false;
            return true;

            
        }

        public bool UpdateStylist(StylistVo s)
        {
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];

            DataRow row = table.Select().Single(x => (uint)x["stylistId"] == s.StylistId);
            
            row["stylistName"] = s.StylistName;
            row["additionalPrice"] = s.AdditionalPrice;
            row["personalDay"] = s.PersonalDay;

            if (Save(adapter) == -1)
                return false;
            return true;


        }

        public bool RemoveStylist(uint stylistId)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];

            DataRow row = table.Select().Single(x => (uint)x["stylistId"] == stylistId);

            row.Delete();

            if (Save(adapter) == -1)
                return false;
            return true;
        }

        public int Save(MySqlDataAdapter adapter)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                
            return adapter.Update(_ds,"stylist");
        }
    }
}
