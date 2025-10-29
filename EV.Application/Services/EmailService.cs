using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EV.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;


        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string htmlContent)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("SWD392_EVM", "ducpvse183843@fpt.edu.vn"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = htmlContent
            };

            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_configuration["Gmail:SmtpHost"],Convert.ToInt32(_configuration["Gmail:SmtpPort"]), SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(_configuration["Gmail:Email"], _configuration["Gmail:AppPassword"]);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while sending the mail: " + e.Message);
            }
        }

        public async Task<string> LoadTemplateAsync(string fileName, Dictionary<string, string> replacements)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", fileName);
            var content = await File.ReadAllTextAsync(path);

            foreach (var kvp in replacements)
            {
                content = content.Replace($"{{{{{kvp.Key}}}}}", kvp.Value);
            }
            return content;
        }

    }
}
