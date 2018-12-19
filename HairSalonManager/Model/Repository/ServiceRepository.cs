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
    class ServiceRepository : BaseRepository
    {
        #region singleTon
        private static ServiceRepository _serviceRepository;

        public static ServiceRepository SR
        {
            get
            {
                if (_serviceRepository == null)
                    _serviceRepository = new ServiceRepository();
                return _serviceRepository;
            }            
        }

        private ServiceRepository()
        {
            _sql = "SELECT * FROM service";
            _list = GetServices();            
        }
        #endregion

        DataSet _ds;

         List<ServiceVo> _list;





        #region method

        public List<ServiceVo> GetServicesFromLocal()
        {
            return new List<ServiceVo>(_list);
        }

        public List<ServiceVo> GetServices()
        {
            List<ServiceVo> list = new List<ServiceVo>();

            _ds = new DataSet();

           

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql,_conn.Msc);

            adapter.Fill(_ds, "Service");

            foreach (DataRow r in _ds.Tables[0].Rows)
            {
                ServiceVo service = new ServiceVo();
                service.ServiceId = (ushort)r["serviceId"];
                service.ServiceName = r["serviceName"] as string;
                service.ServicePrice = (uint)r["servicePrice"];
                service.ServiceTime = (ushort)r["serviceTime"];
                service.ServiceDescription = r["serviceDescription"] as string;
                list.Add(service);
            }

            return list;

        }

        public bool UpdateService(ServiceVo s)
        {            

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql,_conn.Msc);         

            DataTable table = _ds.Tables[0];

            DataRow row = table.Select().Single(x => (ushort)x["serviceId"] == s.ServiceId);
           
            row["serviceName"] = s.ServiceName;
            row["servicePrice"] = s.ServicePrice;
            row["serviceTime"] = s.ServiceTime;
            row["serviceDescription"] = s.ServiceDescription;
            
            if (Save(adapter) == -1)
                return false;
            return true;
            
        }

        public bool DeleteService(int serviceId)
        {          

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];

            DataRow row = table.Select().Single(x => (ushort)x["serviceId"] == serviceId);

            row.Delete();

            if (Save(adapter) == -1)
                return false;
            return true;

        }

        public bool InsertService(ServiceVo s)
        {           

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            DataTable table = _ds.Tables[0];

            DataRow row = table.NewRow();

            row["serviceId"] = s.ServiceId;
            row["serviceName"] = s.ServiceName;
            row["servicePrice"] = s.ServicePrice;
            row["serviceTime"] = s.ServiceTime;
            row["serviceDescription"] = s.ServiceDescription;

            table.Rows.Add(row);

            if (Save(adapter) == -1)
                return false;
            return true;

        }

        public int Save(MySqlDataAdapter adapter)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            adapter.Update(_ds, "service");
            _list = new List<ServiceVo>(GetServices());
            return 0;
        }
        #endregion
    }
}
