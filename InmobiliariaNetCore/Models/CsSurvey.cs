using System;
using System.Collections.Generic;

namespace InmobiliariaNetCore.Models
{
    public partial class CsSurvey
    {
        public int Id { get; set; }
        public int? ActivityId { get; set; }
        public string Answers { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }

        public virtual CsActivity? Activity { get; set; }
    }
}
