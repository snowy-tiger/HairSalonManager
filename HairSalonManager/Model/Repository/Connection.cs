
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Repository
{
    class Connection
    {
        private static Connection _conn  = new Connection();

        private MySqlConnection _msc;

        private string _connStr = "Server=localhost;Database=beautysalon;uid=root;Pwd=cs1234";

        private Connection()
        {
            _msc = new MySqlConnection(_connStr); //db와 연결
        }

        public static Connection Conn //singleton pattern 
        {
            get { return _conn; }      
        }

        public MySqlConnection Msc
        {
            get { return _msc; }
        }

    }
}
