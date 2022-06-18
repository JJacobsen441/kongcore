using System;
using System.Collections.Generic;
using System.Linq;
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
            //if(res.Count(x => x.ToString() == "<p>") > 1)
            //{
            //    res = res.Substring(res.IndexOf("<p>") + 3, res.Length);
            //    res = res.Substring(0, res.LastIndexOf("</p>"));
            //    res = res.Trim();
            //}
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
            //Regex regex = new Regex(@"(\.|[a-z]|[A-Z]|[0-9])*@(\.|[a-z]|[A-Z]|[0-9])*");
            Regex regex2 = new Regex(@"([a-zA-Z0-9\.]*)\@([a-zA-Z0-9\.]*)");
            //foreach (Match match in regex.Matches(inputString))
            //{
            //match.Value == "xx@yahoo.com.my"
            //string name = match.Groups[1]; // "xx"
            //string domain = match.Groups[2]; // "yahoo.com.my"
            Match m = regex2.Match(inputString);
            string email = m.Groups[1] + "@" + m.Groups[2]; // "yahoo.com.my"
            //}
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

        public static bool IsDebug(HttpRequestBase req)
        {
            //Statics.Notification.Run("mail@kongcore.dk", "mail@kongcore.dk", "mail@kongcore.dk", "host", req.Url.Host);
            return req.Url.Host.Trim() != "kongcore-dk.s1.umbraco.io" && req.Url.Host.Trim() != "www.kongcore.dk" && req.Url.Host.Trim() != "kongcore.dk";
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

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static bool IsNullOrEmpty<T>(ICollection<T> value) where T : class
        {
            return value == null || value.Count() == 0;
        }

        public static bool IsNull<T>(T value) where T : class
        {
            return value == null;
        }

        public static bool IsNotNull<T>(T value) where T : class
        {
            return value != null;
        }

        public static bool IsNull<T>(T? nullableValue) where T : struct
        {
            return !nullableValue.HasValue;
        }

        public static bool IsNotNull<T>(T? nullableValue) where T : struct
        {
            return nullableValue.HasValue;
        }

        public static bool HasValue<T>(T? nullableValue) where T : struct
        {
            return nullableValue.HasValue;
        }

        public static bool HasNoValue<T>(T? nullableValue) where T : struct
        {
            return !nullableValue.HasValue;
        }
    }
}
