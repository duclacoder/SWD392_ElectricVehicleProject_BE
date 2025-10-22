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
                PaymentMethodId = request.PaymentMethodId,
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
            if(payment is null)
            {
                return BadRequest(new ResponseDTO<object>("Payment is not exist", false));
            }
            payment!.Status = "Paid";
            _paymentService.UpdatePaymentAsync(payment);
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
            payment!.Status = "Paid";
            _paymentService.UpdatePaymentAsync(payment);
            await _unitOfWork.SaveChangesAsync();
            var vnpayUrl = _vnPayService.CreatePaymentUrl(payment.PaymentsId.ToString(), payment.TransferAmount, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");
            return new ResponseDTO<object>("Payment successful", true, vnpayUrl);
        }

        [HttpGet("VNPay-ReturnUrl")]
        public async Task<ActionResult<ResponseDTO<object>>> PaymentReturn()
        {
            var query = HttpContext.Request.Query;
            string responseCode = query["vnp_ResponseCode"];

            if (responseCode == "00")
            {
                Redirect("");
                return Ok(new ResponseDTO<object>("Payment successful", true));                
            }
                
            else
                return BadRequest(new ResponseDTO<object>("Payment fail", false));
        }
    }
}
