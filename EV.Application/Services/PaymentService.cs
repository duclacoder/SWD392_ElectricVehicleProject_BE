using EV.Application.CustomEntities;
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

        public async Task<CustomPayment> CreatePaymentAsync(CustomPayment payment)
        {
            var createPayment = new Payment()
            {
                PaymentsId = payment.PaymentsId,
                AccountNumber = payment.AccountNumber,
                Accumulated = payment.Accumulated,
                Content = payment.Content,
                CreatedAt = payment.CreatedAt,
                Currency = payment.Currency,
                Gateway = payment.Gateway,
                PaymentMethodId = payment.PaymentMethodId,
                ReferenceId = payment.ReferenceId,
                ReferenceType = payment.ReferenceType,
                Status = payment.Status,
                TransactionDate = payment.TransactionDate,
                TransferAmount = payment.TransferAmount,
                TransferType = payment.TransferType,
                UpdatedAt = payment.UpdatedAt,
                UserId = payment.UserId
            };
            var newPayment = await _unitOfWork.paymentRepository.CreateAsync(createPayment);
            await _unitOfWork.SaveChangesAsync();
            return new CustomPayment
            {
                PaymentsId = newPayment.PaymentsId,
                UserId = newPayment.UserId,
                PaymentMethodId = newPayment.PaymentMethodId,
                Gateway = newPayment.Gateway,
                TransactionDate = newPayment.TransactionDate,
                AccountNumber = newPayment.AccountNumber,
                Content = newPayment.Content,
                TransferType = newPayment.TransferType,
                TransferAmount = newPayment.TransferAmount,
                Currency = newPayment.Currency,
                Accumulated = newPayment.Accumulated,
                CreatedAt = newPayment.CreatedAt,
                UpdatedAt = newPayment.UpdatedAt,
                ReferenceId = newPayment.ReferenceId,
                ReferenceType = newPayment.ReferenceType,
                Status = newPayment.Status
            };

        }

        public void DeletePaymentAsync(CustomPayment payment)
        {
            var deletedPayment = new Payment()
            {
                AccountNumber = payment.AccountNumber,
                Accumulated = payment.Accumulated,
                Content = payment.Content,
                CreatedAt = payment.CreatedAt,
                Currency = payment.Currency,
                Gateway = payment.Gateway,
                PaymentMethodId = payment.PaymentMethodId,
                ReferenceId = payment.ReferenceId,
                ReferenceType = payment.ReferenceType,
                Status = payment.Status,
                TransactionDate = payment.TransactionDate,
                TransferAmount = payment.TransferAmount,
                TransferType = payment.TransferType,
                UpdatedAt = payment.UpdatedAt,
                UserId = payment.UserId
            };
            _unitOfWork.paymentRepository.Remove(deletedPayment);
        }

        public async Task<List<CustomPayment>> GetAllPaymentsAsync()
        {
            var list = await _unitOfWork.paymentRepository.GetAllAsync();
            var result = list.Select(p => new CustomPayment
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
            return result;
        }

        public async Task<CustomPayment> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.paymentRepository.GetByIdAsync(id);
            var result = payment == null ? null : new CustomPayment
            {
                PaymentsId = payment.PaymentsId,
                UserId = payment.UserId,
                PaymentMethodId = payment.PaymentMethodId,
                Gateway = payment.Gateway,
                TransactionDate = payment.TransactionDate,
                AccountNumber = payment.AccountNumber,
                Content = payment.Content,
                TransferType = payment.TransferType,
                TransferAmount = payment.TransferAmount,
                Currency = payment.Currency,
                Accumulated = payment.Accumulated,
                CreatedAt = payment.CreatedAt,
                UpdatedAt = payment.UpdatedAt,
                ReferenceId = payment.ReferenceId,
                ReferenceType = payment.ReferenceType,
                Status = payment.Status
            };
            return result;
        }

        public async Task<ResponseDTO<List<CustomPayment>>> GetPaymentsByUserIdAsync(int userId)
        {
            try
            {
                var payments = await _unitOfWork.paymentRepository.GetPaymentsByUserIdAsync(userId);

                if (payments == null || !payments.Any())
                {
                    return new ResponseDTO<List<CustomPayment>>
                    (
                       message: "No payments found for this user.",
                       isSuccess: false,
                       result: new List<CustomPayment>()
                    );
                }
                return new ResponseDTO<List<CustomPayment>>
                (
                    message: "Payments retrieved successfully.",
                    isSuccess: true,
                    result: payments
                );
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<CustomPayment>>
                (
                    message: $"Error retrieving payments: {ex.Message}",
                    isSuccess: false,
                    result: null
                );
            }
        }

        public void UpdatePayment(CustomPayment payment)
        {
            var updatedPayment = new Payment()
            {
                AccountNumber = payment.AccountNumber,
                Accumulated = payment.Accumulated,
                Content = payment.Content,
                CreatedAt = payment.CreatedAt,
                Currency = payment.Currency,
                Gateway = payment.Gateway,
                PaymentMethodId = payment.PaymentMethodId,
                ReferenceId = payment.ReferenceId,
                ReferenceType = payment.ReferenceType,
                Status = payment.Status,
                TransactionDate = payment.TransactionDate,
                TransferAmount = payment.TransferAmount,
                TransferType = payment.TransferType,
                UpdatedAt = payment.UpdatedAt,
                UserId = payment.UserId
            };
            _unitOfWork.paymentRepository.Update(updatedPayment);
        }
    }
}
