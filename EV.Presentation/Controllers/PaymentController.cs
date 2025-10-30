using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionFeeRequestDTO;
using EV.Application.RequestDTOs.AuctionParticipantDTO;
using EV.Application.RequestDTOs.PaymentRequestDTO;
using EV.Application.RequestDTOs.UserPackagesDTO;
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
        private readonly IUserPackagesServices _userPackageService;
        private readonly IAuctionsFeeService _auctionsFeeService;
        private readonly IUserService _userService;
        private readonly IAuctionParticipantService _auctionParticipantService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, IUnitOfWork unitOfWork, IVnPayService vnPayService, IUserPackagesServices userPackageService, IAuctionsFeeService auctionsFeeService, IUserService userService, IAuctionParticipantService auctionParticipantService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _vnPayService = vnPayService;
            _userPackageService = userPackageService;
            _auctionsFeeService = auctionsFeeService;
            _userService = userService;
            _auctionParticipantService = auctionParticipantService;
            _logger = logger;
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
            if (request.UserId is null)
                return BadRequest(new ResponseDTO<object>("UserId is required", false));

            if ((!request.UserPackageId.HasValue || request.UserPackageId <= 0) && (request.AuctionsFeeId <= 0 || !request.AuctionsFeeId.HasValue))
            {
                return BadRequest(new ResponseDTO<object>("Either UserPackageId or AuctionsFeeId is required", false));
            }

            if (request.UserPackageId.HasValue && request.AuctionsFeeId.HasValue)
                return BadRequest(new ResponseDTO<object>("Only UserPackageId or AuctionsFeeId", false));

            if (request.UserPackageId.HasValue && request.UserPackageId > 0)
            {
                var userPackage = (await _userPackageService.GetUserPackageById(request.UserPackageId.Value)).Result;
                if (userPackage is null)
                    return BadRequest(new ResponseDTO<object>("User package not found", false));

                var newPayment = new Payment
                {
                    UserId = request.UserId.Value,
                    TransferAmount = userPackage.PurchasedAtPrice,
                    Currency = userPackage.Currency ?? "VND",
                    TransactionDate = DateTime.UtcNow,
                    ReferenceType = "UserPackage",
                    ReferenceId = userPackage.UserPackagesId,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    Content = "Thanh toan goi"
                };
                await _paymentService.CreatePaymentAsync(newPayment);
                await _unitOfWork.SaveChangesAsync();

                var result = new
                {
                    newPayment.PaymentsId,
                    userPackage.UserPackagesId,
                    newPayment.TransferAmount,
                };
                return Ok(new ResponseDTO<object>("Payment created successfully", true, result));
            }

            if (request.AuctionsFeeId.HasValue && request.AuctionsFeeId > 0)
            {
                var auctionFee = (await _auctionsFeeService.GetAuctionFeeById(request.AuctionsFeeId.Value)).Result;
                if (auctionFee is null)
                    return BadRequest(new ResponseDTO<object>("Auction Fee not found", false));

                var newPayment = new Payment
                {
                    UserId = request.UserId.Value,
                    TransferAmount = auctionFee.EntryFee,
                    Currency = auctionFee.Currency ?? "VND",
                    TransactionDate = DateTime.UtcNow,
                    ReferenceType = "AuctionFee",
                    ReferenceId = auctionFee.AuctionsFeeId,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    Content = "Phi tham gia dau gia"
                };

                await _paymentService.CreatePaymentAsync(newPayment);
                await _unitOfWork.SaveChangesAsync();

                var result = new
                {
                    newPayment.PaymentsId,
                    auctionFee.AuctionsFeeId,
                    newPayment.TransferAmount,

                };
                return Ok(new ResponseDTO<object>("Payment created successfully", true, result));
            }

            return Ok(new ResponseDTO<object>("Payment created successfully", true));
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
                Redirect(vnpayUrl);
                return new ResponseDTO<object>("Payment successful", true, vnpayUrl);
            }
            return BadRequest(new ResponseDTO<object>("Payment has been paid", false));
        }

        [HttpGet("/VNPay-ReturnUrl")]
        public async Task<ActionResult<ResponseDTO<object>>> PaymentReturn()
        {
            var query = HttpContext.Request.Query;
            var isValidSignature = _vnPayService.ValidateSignature(query);
            if (!isValidSignature)
                return BadRequest(new ResponseDTO<object>("Invalid signature", false));

            string responseCode = query["vnp_ResponseCode"];
            string vnp_TxnRef = query["vnp_TxnRef"]; // (PaymentId)
            string vnp_TransactionNo = query["vnp_TransactionNo"]; // VNPay id
            string vnp_PayDate = query["vnp_PayDate"];
            string vnp_Amount = query["vnp_Amount"];
            string vnp_OrderInfo = query["vnp_OrderInfo"];

            if (responseCode == "00" && int.TryParse(vnp_TxnRef, out int paymentId))
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
                    payment.Accumulated = (payment.Accumulated ?? 0) + payment.TransferAmount;

                    if (string.Equals(payment.ReferenceType, "UserPackage", StringComparison.OrdinalIgnoreCase)
                        && payment.ReferenceId.HasValue)
                    {
                        var userPackage = (await _userPackageService.GetUserPackageById(payment.ReferenceId.Value)).Result;
                        UserPackagesDTO userPackagesDTO = new UserPackagesDTO
                        {
                            PackagesName = userPackage.Package?.PackageName ?? "",
                            PurchasedDuration = userPackage.PurchasedDuration ?? 0,
                            PurchasedAtPrice = userPackage.PurchasedAtPrice ?? 0,
                            Currency = userPackage.Currency ?? "VND"
                        };
                        userPackage.Status = "Active";
                        _paymentService.UpdatePayment(payment);
                        await _userPackageService.UpdateUserPackage(payment.ReferenceId.Value, userPackagesDTO);
                    }

                    if (string.Equals(payment.ReferenceType, "AuctionFee", StringComparison.OrdinalIgnoreCase)
                        && payment.ReferenceId.HasValue)
                    {
                        var auctionFeeResponse = await _auctionsFeeService.GetAuctionFeeById(payment.ReferenceId.Value);
                        var auctionFee = auctionFeeResponse?.Result;

                        if (auctionFee == null)
                        {
                            _logger.LogWarning("AuctionFee not found for ReferenceId {ReferenceId}", payment.ReferenceId.Value);
                        }
                        else if (!auctionFee.AuctionsId.HasValue)
                        {
                            _logger.LogWarning("AuctionFee found but AuctionsId is null for ReferenceId {ReferenceId}", payment.ReferenceId.Value);
                        }
                        else
                        {
                            try
                            {
                                var auctionParticipant = new CreateAuctionParticipantRequestDTO
                                {
                                    AuctionsId = auctionFee.AuctionsId.Value,
                                    UserId = payment.UserId ?? 0, // nếu UserId có thể null thì xử lý thích hợp
                                    PaymentsId = payment.PaymentsId,
                                    Status = "Active",
                                    RefundStatus = "NotRefunded",
                                    IsWinningBid = false,
                                    DepositAmount = auctionFee.EntryFee ?? 0,
                                    DepositTime = DateTime.Now,
                                };

                                await _auctionParticipantService.CreateAuctionParticipantAsync(auctionParticipant);
                                await _unitOfWork.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Failed to create auction participant for PaymentId {PaymentId}", payment.PaymentsId);
                            }
                        }
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return Redirect("http://localhost:5173/paymentSuccess");
            }

            return Redirect("http://localhost:5173/paymentFail");
        }
    }
}
