using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Vo
{
    class ServiceVo
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public int ServicePrice { get; set; }

        public int ServiceTime { get; set; }

        public string ServiceDescription { get; set; }
    }
}
