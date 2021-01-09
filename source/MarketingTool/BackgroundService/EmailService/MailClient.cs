using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundService.EmailService
{
    public class MailClient
    {
        SmtpClient client;
        public MailClient()
        {
            client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3737419d462efd", "d5a393aa4e4237"),
                EnableSsl = true
            };
        }

        public void Send(string from, string to, string subject, string body)
        {
            client.Send(from, to, subject, body);
        }
    }
}
