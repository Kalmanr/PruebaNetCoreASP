using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetCore.Models.Request
{
    public class GetActivity
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public String Status { get; set; }
    }
}
