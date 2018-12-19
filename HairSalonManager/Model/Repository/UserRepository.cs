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
    class UserRepository : BaseRepository
    {
        #region singleTon
        private static UserRepository _userRepository;

        public static UserRepository UR
        {
            get {
                if (_userRepository == null)
                    _userRepository = new UserRepository();
                return _userRepository; }          
        }
        #endregion

        #region ctor
        private UserRepository()
        {
            _ds = new DataSet();
            _sql = "SELECT * FROM user";
            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql,_conn.Msc);
            adapter.Fill(_ds, "user");
            _list = new List<UserVo>(GetUserList());
        }
        #endregion

        #region field
        DataSet _ds;

        private List<UserVo> _list;
        #endregion

        #region method

        public List<UserVo> GetUserList()
        {
            List<UserVo> list = new List<UserVo>();

            foreach (DataRow row in _ds.Tables[0].Rows)
            {
                UserVo u = new UserVo();
                u.UserTel = row["userTel"] as string;
                u.Point = (uint)row["point"];
                list.Add(u);
            }

            return list;
        }
        public bool InsertUser(UserVo user)
        {
            if (_list.Exists(x => x.UserTel == user.UserTel))
            {
                return false;
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];
            DataRow row = table.NewRow();

            row["userTel"] = user.UserTel;
            row["point"] = user.Point;
 
            table.Rows.Add(row);

            if (Save(adapter) == -1)
                return false;
            return true;
        }

        public bool UpdateUser(UserVo user)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];

            DataRow row = table.Select().Single(x => (string)x["userTel"] == user.UserTel);
            
            row["point"] = user.Point;            

            if (Save(adapter) == -1)
                return false;
            return true;
        }

        public int Save(MySqlDataAdapter adapter)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            adapter.Update(_ds, "user");
            _list = GetUserList();
            return 0;
        }
        #endregion
    }
}
