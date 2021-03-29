using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace kongcore.dk.Core.Common
{
    public class Statics
    {
        public static bool IsDebug
        {
            get
            {
                bool isDebug = false;
#if DEBUG
                isDebug = true;
#endif
                return isDebug;
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static class Notification
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
}
