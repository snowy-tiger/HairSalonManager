using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Vo
{
    class TimeTableVo //예약 시간표에 대한 Vo
    {
        public uint StylistId { get; set; }

        public uint ResNum { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        public int OperationTime
        {
            get
            {
                if (StartAt == null || EndAt == null) //startAt , endAt 안들어갈시 처리
                    return -1;
                else
                {
                    TimeSpan ts = EndAt.Value - StartAt.Value;
                    return ts.Days;
                }
                
            }
            
        }
    }
}
