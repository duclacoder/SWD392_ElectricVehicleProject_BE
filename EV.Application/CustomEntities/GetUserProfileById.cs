using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class GetUserProfileById
    {
        public int UsersId { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        //public string? Password { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? Status { get; set; }

        public decimal? Wallet {  get; set; }
    }
}
