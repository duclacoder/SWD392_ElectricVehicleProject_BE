using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserPostDTO
{
    public class CreateUserPostDTO
    {
        public string UserName { get; set; }

        public string? Title { get; set; }

        public int Year { get; set; }

        public VehicleUserPostDTO? Vehicle { get; set; }

        public string? PackageName { get; set; }


        public List<string>? ImageUrls { get; set; }
    }
}
