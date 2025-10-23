using EV.Application.Interfaces.ServiceInterfaces;
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

        public VnPayService(IConfiguration config)
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

        private string VnPayEncode(string input)
        {
            var encoded = HttpUtility.UrlEncode(input, Encoding.UTF8);
            encoded = encoded.Replace("+", "%20")
                             .Replace("%3a", "%3A")
                             .Replace("%2d", "%2D")
                             .Replace("%2f", "%2F")
                             .Replace("%3d", "%3D")
                             .Replace("%26", "%26");
            return encoded;
        }

        private string HmacSHA512Upper(string key, string input)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key ?? "");
            var inputBytes = Encoding.UTF8.GetBytes(input ?? "");
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
            }
        }

        // ValidateSignature (dùng trong ReturnUrl)
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

        private static string HmacSHA512(string key, string inputData)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

    }
}