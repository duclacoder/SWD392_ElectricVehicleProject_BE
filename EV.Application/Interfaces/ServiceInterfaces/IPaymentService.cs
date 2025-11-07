using EV.Application.CustomEntities;
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
        Task<List<CustomPayment>> GetAllPaymentsAsync();
        Task<CustomPayment> GetPaymentByIdAsync(int id);
        Task CreatePaymentAsync(CustomPayment payment);
        void UpdatePayment(CustomPayment payment);
        void DeletePaymentAsync(CustomPayment payment);

        Task<ResponseDTO<List<CustomPayment>>> GetPaymentsByUserIdAsync(int userId);
    }
}
