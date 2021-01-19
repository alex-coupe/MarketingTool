using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public class EmailService
    {
        private SmtpClient _client;
        private MailAddress from;
        public EmailService(IConfiguration configuration)
        {
            _client = new SmtpClient(configuration["SmtpCredentials:Host"], configuration.GetValue<int>("SmtpCredentials:Port"))
            {
                Credentials = new NetworkCredential(configuration["SmtpCredentials:Username"], configuration["SmtpCredentials:Password"]),
                EnableSsl = true
            };

            from = new MailAddress("noreply@marketingtool.co.uk");
        }

        public void Send(MailAddress to, MailMessage message)
        {
            _client.Send(from.Address, to.Address, message.Subject, message.Body);
        }
    }
}
