using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Vo
{
    class TimeTableVo //예약 시간표에 대한 Vo
    {
        public int StylistId { get; set; }

        public int ResNum { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int OperationTime
        {
            get
            {
                TimeSpan ts = EndAt - StartAt;
                return ts.Days;
            }
            
        }
    }
}
