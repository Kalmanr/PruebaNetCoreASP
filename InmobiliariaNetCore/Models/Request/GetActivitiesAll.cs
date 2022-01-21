using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetCore.Models.Request
{
    public class GetActivitiesAll
    {
        public int Id { get; set; }
        public DateTime Schedule { get; set; }
        public String Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Status { get; set; }
        public String Condition { get; set; }
        public int IdPropierty { get; set; }
        public String TitlePropierty { get; set; }
        public String AddressPropierty { get; set; }
        public String Survey { get; set; }


    }
}
