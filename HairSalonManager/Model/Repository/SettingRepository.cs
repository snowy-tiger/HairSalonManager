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
    class SettingRepository : BaseRepository
    {
        #region singleTon
        private static SettingRepository _settingRepository;

        public static SettingRepository SR
        {
            get
            {
                if (_settingRepository == null)
                    _settingRepository = new SettingRepository();
                return _settingRepository;
            }
        }

        #endregion

        #region field
        DataSet _ds;

        #endregion

        #region ctor
        private SettingRepository()
        {
            _ds = new DataSet();
            _sql = "SELECT * FROM setting";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            adapter.Fill(_ds, "setting");
        }
        #endregion

        #region property

        #endregion

        #region method

        public List<SettingVo> GetSettings()
        {
            List<SettingVo> list = new List<SettingVo>();

            foreach (DataRow row in _ds.Tables[0].Rows)
            {
                SettingVo s = new SettingVo();
                s.Property = row["property"] as string;
                s.Value = row["value"] as string;
                list.Add(s);
            }
            return list;
        }

        public void UpdateSetting(SettingVo setting)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];

            DataRow row = table.Select().Single(x => x["property"] as string == setting.Property);

            row["value"] = setting.Value;
           
            Save(adapter);

        }

        public void DeleteSetting(SettingVo setting)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];

            DataRow row = table.Select().Single(x => x["property"] as string == setting.Property);

            row.Delete();

            Save(adapter);

        }

        public int Save(MySqlDataAdapter adapter)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            adapter.Update(_ds, "setting");
            return 0;
        }
        #endregion

    }
}
