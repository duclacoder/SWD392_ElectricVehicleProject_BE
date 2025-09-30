using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserPostDTO
{
    public class UpdateUserPostDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public VehicleUserPostDTO Vehicle { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
