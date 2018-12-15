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
            _list = GetServices();
        }
        #endregion

        DataSet _ds;

        readonly List<ServiceVo> _list;





        #region method

        public List<ServiceVo> GetServicesFromLocal()
        {
            return new List<ServiceVo>(_list);
        }

        public List<ServiceVo> GetServices()
        {
            List<ServiceVo> list = new List<ServiceVo>();

            _ds = new DataSet();

            _sql = "SELECT * FROM service";

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
            _sql = "UPDATE service SET(serviceName,servicePrice,serviceTime,serviceDescription) " +
                "VALUES(@serviceName,@servicePrice,@serviceTime,@serviceDescription) WHERE serviceId = @serviceId";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql,_conn.Msc);

            adapter.UpdateCommand.Parameters.AddWithValue("@serviceId", s.ServiceId);
            adapter.UpdateCommand.Parameters.AddWithValue("@serviceName", s.ServiceName);
            adapter.UpdateCommand.Parameters.AddWithValue("@servicePrice", s.ServicePrice);
            adapter.UpdateCommand.Parameters.AddWithValue("@serviceTime", s.ServiceTime);
            adapter.UpdateCommand.Parameters.AddWithValue("@serviceDescription", s.ServiceDescription);

            if (Save(adapter) == -1)
                return false;
            return true;
            
        }

        public bool DeleteService(int serviceId)
        {
            _sql = "DELETE FROM service WHERE serviceId = @serviceId";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            adapter.DeleteCommand.Parameters.AddWithValue("@serviceId", serviceId);
            

            if (Save(adapter) == -1)
                return false;
            return true;

        }

        public bool InsertService(ServiceVo s)
        {
            _sql = "INSERT INTO service(serviceId,serviceName,servicePrice,serviceTime,serviceDescription) " +
                "VALUES(@serviceId,@serviceName,@servicePrice,@serviceTime,@serviceDescription)";

            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql, _conn.Msc);

            adapter.InsertCommand.Parameters.AddWithValue("@serviceId", s.ServiceId);
            adapter.InsertCommand.Parameters.AddWithValue("@serviceName", s.ServiceName);
            adapter.InsertCommand.Parameters.AddWithValue("@servicePrice", s.ServicePrice);
            adapter.InsertCommand.Parameters.AddWithValue("@serviceTime", s.ServiceTime);
            adapter.InsertCommand.Parameters.AddWithValue("@serviceDescription", s.ServiceDescription);

            if (Save(adapter) == -1)
                return false;
            return true;

        }

        public int Save(MySqlDataAdapter adapter)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            return adapter.Update(_ds, "service");
        }
        #endregion
    }
}
