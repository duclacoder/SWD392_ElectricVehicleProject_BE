using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<CustomPayment>, IPaymentRepository
    {
        public PaymentRepository(Swd392Se1834G2T1Context context) : base(context)
        {
        }

        public async Task<List<CustomPayment>> GetPaymentsByUserIdAsync(int userId)
        {
            var payments = await _context.Payments
                                         .Where(x => x.UserId == userId && x.Status == "Paid")
                                         .OrderByDescending(x => x.CreatedAt)
                                         .ToListAsync();
            var customPayments = payments.Select(p => new CustomPayment
            {
                PaymentsId = p.PaymentsId,
                UserId = p.UserId,
                PaymentMethodId = p.PaymentMethodId,
                Gateway = p.Gateway,
                TransactionDate = p.TransactionDate,
                AccountNumber = p.AccountNumber,
                Content = p.Content,
                TransferType = p.TransferType,
                TransferAmount = p.TransferAmount,
                Currency = p.Currency,
                Accumulated = p.Accumulated,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                ReferenceId = p.ReferenceId,
                ReferenceType = p.ReferenceType,
                Status = p.Status
            }).ToList();

            return customPayments;
        }
    }
}
