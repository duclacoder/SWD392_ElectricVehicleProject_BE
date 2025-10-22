using EV.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            if(amount == null )
            {
                amount = 0;
            }    

            var vnp_TmnCode = _config["VNPay:TmnCode"];
            var vnp_HashSecret = _config["VNPay:HashSecret"];
            var vnp_Url = _config["VNPay:BaseUrl"];
            var vnp_Returnurl = _config["VNPay:ReturnUrl"];

            var vnp_Version = "2.1.0";
            var vnp_Command = "pay";
            var vnp_CurrCode = "VND";
            var vnp_TxnRef = paymentId;
            var vnp_OrderInfo = $"Thanh toan don hang {paymentId}";
            var vnp_OrderType = "other";
            var vnp_Amount = ((int)amount * 100).ToString();
            var vnp_Locale = "vn";

            var inputData = new SortedDictionary<string, string>
        {
            {"vnp_Version", vnp_Version},
            {"vnp_Command", vnp_Command},
            {"vnp_TmnCode", vnp_TmnCode},
            {"vnp_Amount", vnp_Amount},
            {"vnp_CurrCode", vnp_CurrCode},
            {"vnp_TxnRef", vnp_TxnRef},
            {"vnp_OrderInfo", vnp_OrderInfo},
            {"vnp_OrderType", vnp_OrderType},
            {"vnp_ReturnUrl", vnp_Returnurl},
            {"vnp_IpAddr", ipAddress},
            {"vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")},
            {"vnp_Locale", vnp_Locale}
        };

            var query = string.Join("&", inputData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var signData = string.Join("&", inputData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var secureHash = HmacSHA512(vnp_HashSecret, signData);
            query += $"&vnp_SecureHash={secureHash}";

            return $"{vnp_Url}?{query}";
        }

        private object HmacSHA512(string? key, string inputData)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
