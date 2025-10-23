using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.PaymentRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVnPayService _vnPayService;

        public PaymentController(IPaymentService paymentService, IUnitOfWork unitOfWork, IVnPayService vnPayService)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _vnPayService = vnPayService;
        }

        [HttpGet("GetAllPayment")]
        public async Task<ActionResult<ResponseDTO<List<Payment>>>> GetAllPayment()
        {
            var transactions = await _paymentService.GetAllPaymentsAsync();
            return new ResponseDTO<List<Payment>>("Get all transactions successfully", true, transactions);
        }

        [HttpPost("CreatePayment")]
        public async Task<ActionResult<ResponseDTO<object>>> CreatePayment(CreatePaymentRequestDTO request)
        {
            var newPayment = new Payment
            {
                UserId = request.UserId,
                TransferAmount = request.TransferAmount,
                Status = "Pending"
            };

            await _paymentService.CreatePaymentAsync(newPayment);
            await _unitOfWork.SaveChangesAsync();
            _vnPayService.CreatePaymentUrl(newPayment.PaymentsId.ToString(), newPayment.TransferAmount, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");
            return new ResponseDTO<object>("Payment created successfully", true, newPayment);

        }

        [HttpPatch("Direct_Pay")]
        public async Task<ActionResult<ResponseDTO<object>>> DirectPay(int paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment is null)
            {
                return BadRequest(new ResponseDTO<object>("Payment is not exist", false));
            }

            payment!.Status = "Paid";
            payment!.PaymentMethodId = 3;
            payment.Gateway = "Direct";
            payment.TransactionDate = DateTime.Now;
            payment.Content = "Thanh toán trực tiếp";
            payment.UpdatedAt = DateTime.Now;

            // (Tuỳ chọn) Cộng dồn vào Accumulated 
            payment.Accumulated = (payment.Accumulated ?? 0) + payment.TransferAmount;

            _paymentService.UpdatePayment(payment);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<object>("Payment successful", true, payment);
        }

        [HttpGet("VNPay")]
        public async Task<ActionResult<ResponseDTO<object>>> VNPay(int paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment is null)
            {
                return BadRequest(new ResponseDTO<object>("Payment is not exist", false));
            }

            if (payment.Status != "Paid")
            {
                HttpContext context = HttpContext;
                var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                if (string.IsNullOrEmpty(ipAddress))
                    ipAddress = context.Connection.RemoteIpAddress?.ToString();

                if (string.IsNullOrEmpty(ipAddress))
                    ipAddress = "127.0.0.1";

                var vnpayUrl = _vnPayService.CreatePaymentUrl(payment.PaymentsId.ToString(), payment.TransferAmount, ipAddress);
                await _unitOfWork.SaveChangesAsync();
                Redirect(vnpayUrl);
                return new ResponseDTO<object>("Payment successful", true, vnpayUrl);
            }
            return BadRequest(new ResponseDTO<object>("Payment has been paid", false));
        }



        [HttpGet("/VNPay-ReturnUrl")]
        public async Task<ActionResult<ResponseDTO<object>>> PaymentReturn()
        {
            var query = HttpContext.Request.Query;

            // 1️⃣ Xác thực chữ ký
            var isValidSignature = _vnPayService.ValidateSignature(query);
            if (!isValidSignature)
            {
                return BadRequest(new ResponseDTO<object>("Invalid signature", false));
            }

            // 2️⃣ Lấy các giá trị trả về từ VNPay
            string responseCode = query["vnp_ResponseCode"];
            string vnp_TxnRef = query["vnp_TxnRef"]; // (PaymentId)
            string vnp_TransactionNo = query["vnp_TransactionNo"]; // VNPay id
            string vnp_PayDate = query["vnp_PayDate"]; 
            string vnp_Amount = query["vnp_Amount"]; 
            string vnp_OrderInfo = query["vnp_OrderInfo"];

            if (responseCode == "00")
            {
                if (int.TryParse(vnp_TxnRef, out int paymentId))
                {
                    var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
                    if (payment != null)
                    {                        
                        payment.Status = "Paid";
                        payment.PaymentMethodId = 1; 
                        payment.Gateway = "VNPay";
                        payment.TransactionDate = DateTime.Now;
                        payment.Content = vnp_OrderInfo ?? "Thanh toán qua VNPay";

                        if (decimal.TryParse(vnp_Amount, out decimal amount))
                            payment.TransferAmount = amount;

                        payment.AccountNumber = vnp_TransactionNo;
                        payment.UpdatedAt = DateTime.Now;

                        // (Tuỳ chọn) Cộng dồn vào Accumulated 
                        payment.Accumulated = (payment.Accumulated ?? 0) + payment.TransferAmount;

                        _paymentService.UpdatePayment(payment);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                return Redirect("http://localhost:5173/paymentSuccess");
            }
            else
            {
                return Redirect("http://localhost:5173/paymentFail");
            }
        }
    }
}
