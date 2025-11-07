using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class UserInspectionFeesGetAll
    {
        public int InspectionFeesId { get; set; }

        public string? Description { get; set; }

        public decimal? FeeAmount { get; set; }

        public string? Currency { get; set; }

        public string? Type { get; set; }

        public int? InspectionDays { get; set; }
    }
}
