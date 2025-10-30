using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.PaymentRequestDTO
{
    public class CreatePaymentRequestDTO
    {
        public int? UserId { get; set; }
        public int? UserPackageId { get; set; }
        public int? AuctionsFeeId { get; set; }
        //public decimal TransferAmount { get; set; }
    }
}
