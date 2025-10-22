using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IPaymentService 
    {
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task CreatePaymentAsync(Payment payment);
        void UpdatePaymentAsync(Payment payment);
        void DeletePaymentAsync(Payment payment);

    }
}
