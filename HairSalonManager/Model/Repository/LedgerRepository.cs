using HairSalonManager.Model.Vo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Repository
{
    class LedgerRepository : BaseRepository
    {
        #region singleTon
        private static LedgerRepository _ledgerRepository;

        public static LedgerRepository LR
        {
            get
            {
                if (_ledgerRepository == null)
                {
                    _ledgerRepository = new LedgerRepository();
                }
                return _ledgerRepository;
            }
        }
        #endregion

        #region ctor
        private LedgerRepository()
        {

        }
        #endregion

        public uint InsertLedger(LedgerVo lv)
        {
            _conn.Msc.Open();

            _sql = "INSERT INTO ledger(ledgerNum,resNum,sum) VALUES(@ledgerNum,@resNum,@sum)";

            MySqlCommand cmd = new MySqlCommand(_sql, _conn.Msc);

            cmd.Parameters.AddWithValue("@ledgerNum", lv.LedgerNum);
            cmd.Parameters.AddWithValue("@resNum", lv.ResNum);
            cmd.Parameters.AddWithValue("@sum", lv.Sum);

            if (cmd.ExecuteNonQuery() == -1) //실패시
            {
                _conn.Msc.Close();
                return 0;
            }

            uint recentId = (uint)cmd.LastInsertedId;
            _conn.Msc.Close();
            return recentId; //성공시
           
        }
    }
}
