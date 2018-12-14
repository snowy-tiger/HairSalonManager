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

        public int OperationTime //시간 차이를 분(Minute)으로 계산해서 리턴
        {
            get
            {
                if (StartAt == null || EndAt == null) //startAt , endAt 안들어갈시 처리
                    return -1;
                else
                {
                    TimeSpan ts = EndAt.Value - StartAt.Value;
                    int result = (ts.Hours * 60) + ts.Minutes;
                    return result;
                }
                
            }
            
        }
    }
}
