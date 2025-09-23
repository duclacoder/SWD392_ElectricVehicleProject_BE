using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.AdminRequestDTO
{
    public class GetAllUsersRequestDTO
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
