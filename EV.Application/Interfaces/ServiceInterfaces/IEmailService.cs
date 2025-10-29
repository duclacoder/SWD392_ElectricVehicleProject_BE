using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string subject, string htmlContent);
        Task<string> LoadTemplateAsync(string fileName, Dictionary<string, string> replacements);
    }
}
