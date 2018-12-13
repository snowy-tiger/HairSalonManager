using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Vo
{
    class StylistVo
    {
        public uint StylistId { get; set; }

        public string StylistName { get; set; }

        public uint AdditionalPrice { get; set; }

        public int PersonalDay { get; set; }
    }
}
