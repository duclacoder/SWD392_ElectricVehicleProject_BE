using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.PostPackageDTO
{
    public class CreatePostPackageRequestDTO
    {
        public string? PackageName { get; set; }

        public string? Description { get; set; }

        public decimal? PostPrice { get; set; }

        public string? Currency { get; set; } = "VND";

        public int PostDuration { get; set; }

        public string Status { get; set; } = "Active";
    }
}
