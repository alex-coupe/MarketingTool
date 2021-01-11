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
        public EmailService()
        {
            _client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3737419d462efd", "d5a393aa4e4237"),
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
