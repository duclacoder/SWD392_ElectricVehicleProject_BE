using EV.Application.ResponseDTOs;
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
        Task<Payment> GetPaymentByIdAsync(int id);
        Task CreatePaymentAsync(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePaymentAsync(Payment payment);

        Task<ResponseDTO<List<Payment>>> GetPaymentsByUserIdAsync(int userId);
    }
}
