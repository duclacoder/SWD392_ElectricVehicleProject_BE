using EV.Application.RequestDTOs.BatteryPostRequestDTO;
using EV.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserPostDTO
{
    public class CreateUserPostDTO
    {
        public int UserId { get; set; }

        public string? Title { get; set; }

        public int Year { get; set; }

        public VehicleUserPostDTO? Vehicle { get; set; }
        public BatteryUserPostDTO? Battery { get; set; }

        public int UserPackageId { get; set; }


        public List<IFormFile>? ImageUrls { get; set; }

    }
}
