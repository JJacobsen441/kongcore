using System.Net;
using System.Net.Mail;

namespace kongcore.dk.Core.Common
{
    public static class NotificationHelper
    {
        public static void Run(string from, string to, string cred, string subject, string body)
        {

            MailMessage mail = new MailMessage(from, to);
            SmtpClient client = new SmtpClient();
            client = new SmtpClient();
            client.Credentials = new NetworkCredential(cred, "Nostromo2503");
            client.Port = 25;
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            client.Host = "80.161.50.61";
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            client.Send(mail);
        }
    }
}