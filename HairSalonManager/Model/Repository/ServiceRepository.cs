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
        private ServiceRepository _serviceRepository;

        public ServiceRepository SR
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

        

        public List<ServiceVo> ServiceList
        {
            get { return _list; }            
        }

        #region method
        public List<ServiceVo> GetServices()
        {
            List<ServiceVo> list = new List<ServiceVo>();

            _sql = "SELECT * FROM service";
            MySqlDataAdapter adapter = new MySqlDataAdapter(_sql,_conn.Msc);

            adapter.Fill(_ds, "Service");

            foreach (DataRow r in _ds.Tables[0].Rows)
            {
                ServiceVo service = new ServiceVo();
                service.ServiceId = (int)r["serviceId"];
                service.ServiceName = r["serviceName"] as string;
                service.ServicePrice = (int)r["servicePrice"];
                service.ServiceTime = (int)r["serviceTime"];
                service.ServiceDescription = r["serviceDescription"] as string;
                list.Add(service);
            }

            return list;

        }
        #endregion
    }
}
