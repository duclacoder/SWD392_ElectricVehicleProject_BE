using EV.Application.CustomEntities;
using EV.Application.ResponseDTOs;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IPaymentService
    {
        Task<List<CustomPayment>> GetAllPaymentsAsync();
        Task<CustomPayment> GetPaymentByIdAsync(int id);
        Task<CustomPayment> CreatePaymentAsync(CustomPayment payment);
        void UpdatePayment(CustomPayment payment);
        void DeletePaymentAsync(CustomPayment payment);

        Task<ResponseDTO<List<CustomPayment>>> GetPaymentsByUserIdAsync(int userId);
    }
}
