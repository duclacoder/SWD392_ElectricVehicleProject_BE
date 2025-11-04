using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreatePaymentAsync(Payment payment)
        {

            await _unitOfWork.paymentRepository.CreateAsync(payment);
        }

        public void DeletePaymentAsync(Payment payment)
        {
            _unitOfWork.paymentRepository.Remove(payment);
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _unitOfWork.paymentRepository.GetAllAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _unitOfWork.paymentRepository.GetByIdAsync(id);
        }

        public async Task<ResponseDTO<List<Payment>>> GetPaymentsByUserIdAsync(int userId)
        {
            try
            {
                var payments = await _unitOfWork.paymentRepository.GetPaymentsByUserIdAsync(userId);

                if (payments == null || !payments.Any())
                {
                    return new ResponseDTO<List<Payment>>
                    (
                       message: "No payments found for this user.",
                       isSuccess: false,
                       result: new List<Payment>()
                    );
                }
                return new ResponseDTO<List<Payment>>
                (
                    message: "Payments retrieved successfully.",
                    isSuccess: true,
                    result: payments
                );
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<Payment>>
                (
                    message: $"Error retrieving payments: {ex.Message}",
                    isSuccess: false,
                    result: null
                );
            }
        }

        public void UpdatePayment(Payment payment)
        {
            _unitOfWork.paymentRepository.Update(payment);
        }
    }
}
