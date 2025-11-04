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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(Swd392Se1834G2T1Context context) : base(context)
        {
        }

        public async Task<List<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            var payments = await _context.Payments
                                         .Where(x => x.UserId == userId && x.Status == "Paid")
                                         .OrderByDescending(x => x.CreatedAt)
                                         .ToListAsync();

            return payments;
        }
    }
}
