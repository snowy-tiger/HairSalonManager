using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Vo
{
    class ReservationVo
    {
        public uint ResNum { get; set; }

        public uint? StylistId { get; set; }

        public string UserTel { get; set; }

        public string Note { get; set; }       

        public int Gender { get; set; }

        public DateTime? UserBirthday { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        public string UserName { get; set; }

        public bool IsPaid { get; set; }

        public bool IsMan { get; set; }

        public bool IsWoman { get; set; }
    }
}
