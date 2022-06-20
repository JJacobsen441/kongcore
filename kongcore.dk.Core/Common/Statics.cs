using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using www.kongcore.dk.Common;

namespace kongcore.dk.Core.Common
{
    public class Statics
    {
        public static void Visitor()
        {
            HttpRequestBase httpRequestBase = new HttpRequestWrapper(System.Web.HttpContext.Current.Request);
            string ip = RequestHelpers.GetClientIpAddress(httpRequestBase);

            string subject = "visitor.."; 
            string body = "who: " + ip;
            Notification.Run("mail@kongcore.dk", "mail@kongcore.dk", "mail@kongcore.dk", subject, body);
        }

        public static string RichStrip(string html)
        {
            if (String.IsNullOrEmpty(html))
                return "";

            html = html
                .Replace("<!DOCTYPE html>", "")
                .Replace("<html>", "")
                .Replace("</html>", "")
                .Replace("<head>", "")
                .Replace("</head>", "")
                .Replace("<body>", "")
                .Replace("</body>", "")
                .Trim();
            
            return html;
        }

        public static string RemoveFirstParagraphTag(string html)
        {
            if (String.IsNullOrEmpty(html))
                return "";

            if (html.Length > 5)
            {
                if (html.ToUpper().Substring(0, 3) == "<P>" && html.ToUpper().Substring(html.Length - 4, 4) == "</P>")
                {
                    html = html.Substring(3, html.Length - 3);
                    html = html.Substring(0, html.Length - 4);
                }
            }
            return html;
        }

        private static string RegEx(string inputString) 
        {
            if (String.IsNullOrEmpty(inputString))
                return "";

            //Regex regex = new Regex(@"(\.|[a-z]|[A-Z]|[0-9])*@(\.|[a-z]|[A-Z]|[0-9])*");
            Regex regex2 = new Regex(@"([a-zA-Z0-9\.]*)\@([a-zA-Z0-9\.]*)");
            
            Match m = regex2.Match(inputString);
            string email = m.Groups[1] + "@" + m.Groups[2];
            
            return email;
        }

        public static string FormatMail(string text) 
        {
            if (String.IsNullOrEmpty(text))
                return "";

            string mail = RegEx(text);
            string to_mail = "" + mail;
            string new_mail = FormatEmailMailto(to_mail, mail);
            text = text.Replace(mail, new_mail);
            text = FormatEmailIcon(text);


            return text;
        }

        public static string FormatEmailMailto(string mail, string text)
        {
            if (String.IsNullOrEmpty(text))
                return "";

            text = "<a href=\"mailto:" + mail + "\">" + text + "</a>";

            return text;
        }

        public static string FormatEmailAt(string text)
        {
            if (String.IsNullOrEmpty(text))
                return "";

            if (text.ToLower().Contains("@"))
                text = text.Replace("@", "(at)");

            return text;
        }

        public static string FormatEmailIcon(string text)
        {
            if (String.IsNullOrEmpty(text))
                return "";

            if (text.ToLower().Contains("icon_email"))
                text = text.Replace("icon_email", "<i class=\"far fa-envelope color-black\"></i>");

            return text;
        }

        public static string FormatPhoneIcon(string text)
        {
            if (String.IsNullOrEmpty(text))
                return "";

            if (text.ToLower().Contains("icon_phone"))
                text = text.Replace("icon_phone", "<i class=\"fas fa-phone color-black\"></i>");

            return text;
        }

        public static bool IsDebug()
        {
            HttpRequestBase httpRequestBase = new HttpRequestWrapper(System.Web.HttpContext.Current.Request);
            string host = httpRequestBase.Url.Host.Trim();

            return host == "localhost"; 
            //return 
            //    host != "kongcore-dk.s1.umbraco.io" &&
            //    host != "www.kongcore.dk" &&
            //    host != "kongcore.dk";
        }

        /*public static bool IsDebug()
        {
            bool isDebug = false;
#if DEBUG
            isDebug = true;
#endif
            return isDebug;
        }/**/

        public static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;

                MailAddress addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }        
    }
}
