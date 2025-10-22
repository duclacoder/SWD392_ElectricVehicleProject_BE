using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(string paymentId, decimal? amount, string ipAddress);
    }
}
