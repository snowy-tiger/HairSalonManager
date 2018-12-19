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
            _sql = "INSERT INTO stylist(stylistId,stylistName,additionalPrice,personalDay) " +
                "VALUES(@stylistId,@stylistName,@additionalPrice,@personalDay)";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            adapter.SelectCommand.Parameters.AddWithValue("@stylistId", s.StylistId);
            adapter.SelectCommand.Parameters.AddWithValue("@stylistName", s.StylistName);
            adapter.SelectCommand.Parameters.AddWithValue("@additionalPrice", s.AdditionalPrice);
            adapter.SelectCommand.Parameters.AddWithValue("@personalDay", s.PersonalDay);

            if (Save(adapter) == -1)
                return false;
            return true;

            
        }

        public bool UpdateStylist(StylistVo s)
        {
            _sql = "UPDATE  stylist SET(stylistName,additionalPrice,personalDay) " +
                "VALUES(@stylistName,@additionalPrice,@personalDay WHERE stylistId = @stylistId)";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            
            adapter.UpdateCommand.Parameters.AddWithValue("@stylistName", s.StylistName);
            adapter.UpdateCommand.Parameters.AddWithValue("@additionalPrice", s.AdditionalPrice);
            adapter.UpdateCommand.Parameters.AddWithValue("@personalDay", s.PersonalDay);
            adapter.UpdateCommand.Parameters.AddWithValue("@stylistId", s.StylistId);

            if (Save(adapter) == -1)
                return false;
            return true;


        }

        public bool RemoveStylist(uint stylistId)
        {
            _sql = "DELETE FROM stylist WHERE stylistId = @stylistId)";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            adapter.DeleteCommand.Parameters.AddWithValue("@stylistId", stylistId);           

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
