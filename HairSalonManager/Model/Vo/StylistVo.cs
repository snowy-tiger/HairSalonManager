using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Vo
{
    class StylistVo
    {
        public int StylistId { get; set; }

        public string StylistName { get; set; }

        public int AdditionalPrice { get; set; }

        public int PersonalDay { get; set; }
    }
}
