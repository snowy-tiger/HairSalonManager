using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Vo
{
    class ReservationVo
    {
        public int ResNum { get; set; }

        public int StylistId { get; set; }

        public string userTel { get; set; }

        public string Note { get; set; }

        public int MyProperty { get; set; }

        public int Gender { get; set; }

        public DateTime UserBirthday { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public string UserName { get; set; }
    }
}
