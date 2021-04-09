using System.Net;
using System.Net.Mail;
using System.Web;

namespace kongcore.dk.Core.Common
{
    public class Statics
    {
        public static void Visitor(string ip)
        {
            string subject = "visitor..";
            string body = "who: " + ip;
            Statics.Notification.Run("mail@kongcore.dk", "mail@kongcore.dk", "mail@kongcore.dk", subject, body);
        }

        public static bool IsDebug(HttpRequestBase req)
        {
            //Statics.Notification.Run("mail@kongcore.dk", "mail@kongcore.dk", "mail@kongcore.dk", "host", req.Url.Host);
            return req.Url.Host.Trim() != "kongcore-dk.s1.umbraco.io" && req.Url.Host.Trim() != "www.kongcore.dk";
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
