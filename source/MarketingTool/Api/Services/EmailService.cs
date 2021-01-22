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
            //TODO add better error handling and logging to DB
            try
            {
                _client.Send(from.Address, to.Address, message.Subject, message.Body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            
            }
        }
    }
}
