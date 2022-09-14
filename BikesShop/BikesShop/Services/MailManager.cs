using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BikesShop.Services
{
    public class MailManager
    {
        public static void SendMail(string data, string destinationEmail)
        {
            MailAddress from = new MailAddress("spamtemp0azaza@gmail.com", "my logg");
            MailAddress to = new MailAddress(destinationEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Bicycle purshase";
            m.Body = data;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("spamtemp0azaza@gmail.com", "hryfprkojvshqkpi");
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(m);
        }
    }
}
