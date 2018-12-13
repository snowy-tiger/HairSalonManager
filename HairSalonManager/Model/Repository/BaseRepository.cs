using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Repository
{
    class BaseRepository //모든 레퍼지토리의 기반 클래스
    {
#pragma warning disable 612, 618
#pragma warning restore 612, 618
        protected Connection _conn;

        protected string _sql;

        protected BaseRepository()
        {
            _conn = Connection.Conn;
        }
    }
}
