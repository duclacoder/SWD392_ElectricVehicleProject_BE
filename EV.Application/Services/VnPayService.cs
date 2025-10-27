using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using VNPay.NetCore;

namespace EV.Application.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;

        public VnPayService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _config = config;
        }

        public string CreatePaymentUrl(string paymentId, decimal? amount, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress) || ipAddress.Contains("::"))
                ipAddress = "127.0.0.1";

            var vnp_TmnCode = _config["VNPay:TmnCode"]?.Trim();
            var vnp_HashSecret = _config["VNPay:HashSecret"]?.Trim();
            var vnp_BaseUrl = _config["VNPay:BaseUrl"]?.Trim().TrimEnd('/');
            var vnp_ReturnUrl = _config["VNPay:ReturnUrl"]?.Trim();

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode!);
            vnpay.AddRequestData("vnp_Amount", ((long)(amount* 100)).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang {paymentId}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl ?? "");
            vnpay.AddRequestData("vnp_TxnRef", paymentId);

            string paymentUrl = vnpay.CreateRequestUrl(vnp_BaseUrl, vnp_HashSecret!);
            if (paymentUrl == null) return "Fail to create VN Pay URL";
            return paymentUrl;
        }

        public bool ValidateSignature(IQueryCollection query)
        {
            var vnpay = new VnPayLibrary();

            // Add all params from query except hash
            foreach (var (key, value) in query)
            {
                if (!string.Equals(key, "vnp_SecureHash", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(key, "vnp_SecureHashType", StringComparison.OrdinalIgnoreCase))
                {
                    vnpay.AddResponseData(key, value);
                }
            }

            string vnp_SecureHash = query["vnp_SecureHash"];
            string hashSecret = _config["VNPay:HashSecret"];

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, hashSecret);
            return checkSignature;
        }
    }
}