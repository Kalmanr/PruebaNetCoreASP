using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetCore.Models.Request
{
    public class ActivityRequest
    {

        public int id { get; set; }
        public int PropiertyId { get; set; }
        public DateTime Schedule { get; set; }

        public String Title { get; set; }

        public String Status { get; set; }


    }
}
