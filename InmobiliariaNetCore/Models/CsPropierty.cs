using System;
using System.Collections.Generic;

namespace InmobiliariaNetCore.Models
{
    public partial class CsPropierty
    {
        public CsPropierty()
        {
            CsActivities = new HashSet<CsActivity>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DisabledAt { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<CsActivity> CsActivities { get; set; }
    }
}
