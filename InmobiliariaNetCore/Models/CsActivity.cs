using System;
using System.Collections.Generic;

namespace InmobiliariaNetCore.Models
{
    public partial class CsActivity
    {
        public CsActivity()
        {
            CsSurveys = new HashSet<CsSurvey>();
        }

        public int Id { get; set; }
        public int PropiertyId { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual CsPropierty Propierty { get; set; } = null!;
        public virtual ICollection<CsSurvey> CsSurveys { get; set; }
    }
}
