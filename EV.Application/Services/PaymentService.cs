using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
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

        public void UpdatePaymentAsync(Payment payment)
        {
            _unitOfWork.paymentRepository.Update(payment);
        }
    }
}
