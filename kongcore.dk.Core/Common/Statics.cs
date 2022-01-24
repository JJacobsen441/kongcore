using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using Umbraco.Web.Models;
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
            Statics.Notification.Run("mail@kongcore.dk", "mail@kongcore.dk", "mail@kongcore.dk", subject, body);
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

        //public static string GetUrl(object obj) 
        //{
        //}


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
 
        public class Characters
        {
            public static char[] All(bool withreturnnewline)
            {
                char c = '\r';
                char r = '\n';
                //char[] a = new char[] { ' ', '.', ',', '\'', '"', ':', ';', '&', '#', '!', '?', '/', '%', '+', '-', '(', ')', '[', ']', '{', '}', '<', '>' };
                char[] a = new char[] { ' ', '.', ',', '+', '-', '*', '/', ':', ';', '_', '\'', '#', '(', ')', '[', ']', '<', '>', '{', '}', '=', '?', '!', '@', '%', '$', '&' };
                if (withreturnnewline)
                    a = a.Concat(new char[] { c }).ToArray();
                if (withreturnnewline)
                    a = a.Concat(new char[] { r }).ToArray();
                return a;
            }
            public static char[] Limited(bool withsemi)
            {
                char s = ';';
                char[] a = new char[] { ' ', '_', ',', '+', '-', '\'', '&', '#', '(', ')', '[', ']' };
                if (withsemi)
                    a = a.Concat(new char[] { s }).ToArray();
                return a;
            }
            public static char[] VeryLimited()
            {
                return new char[] { ' ', '\'', '-', '&', '#' };
            }
            public static char[] Website()
            {
                return new char[] { '-', '/', '.', ':' };
            }
            public static char[] Category()
            {
                return new char[] { ' ', '-', '.' };
            }
            public static char[] Name()
            {
                return new char[] { ' ', '-', '*' };
            }
            public static char[] Country()
            {
                return new char[] { ' ', '-' };
            }
            public static char[] Address()
            {
                return new char[] { ' ', '.', ',', '-', '\'' };
            }
            public static char[] Space()
            {
                return new char[] { ' ' };
            }
            public static char[] Param()
            {
                return new char[] { '0', '1', ':', '_' };
            }
        }

        private static bool _m = false;
        public static bool Maintenance
        {
            get
            {
                return _m;
            }
            set
            {
                _m = value;
            }
        }
//        public static bool IsDebug
//        {
//            get
//            {
//                bool isDebug = false;
//#if DEBUG
//                isDebug = true;
//#endif
//                return isDebug;
//            }
//        }
        //public static string MyIP
        //{
        //    get
        //    {
        //        return "80.161.50.61";
        //    }
        //}
        //public static string Root
        //{
        //    get
        //    {
        //        string app_path = HostingEnvironment.ApplicationPhysicalPath;
        //        string nd = Path.DirectorySeparatorChar.ToString();
        //        string r = "";
        //        if (IsDebug)
        //            r = app_path;
        //        else
        //        {
        //            //Statics.Log("path > " + app_path);
        //            if (app_path.EndsWith("bin\\"))
        //                app_path = app_path.Replace("bin\\", "");
        //            if (app_path.EndsWith("bin/"))
        //                app_path = app_path.Replace("bin/", "");
        //            if (app_path.EndsWith("bin"))
        //                app_path = app_path.Substring(0, app_path.Length - 3);
        //            r = app_path;
        //            //Statics.Log("path > " + app_path);
        //        }
        //        return r;
        //    }
        //}
        //public static string Content
        //{
        //    get
        //    {
        //        string nd = Path.DirectorySeparatorChar.ToString();
        //        return Root + "_content" + nd;
        //    }
        //}
        //public static bool IsAdmin(string ip)
        //{
        //    if (IsDebug)
        //        return ip == "::1" || ip == "127.0.0.1" || ip == MyIP;

        //    return ip == MyIP;
        //}

        public static void Log(string msg, bool loop = false)
        {
            try
            {
                string nd = Path.DirectorySeparatorChar.ToString();
                string path = HostingEnvironment.ApplicationPhysicalPath + "_content" + nd + "log" + nd + "logfile.txt";

                using (StreamWriter writer = System.IO.File.AppendText(path))
                {
                    string d = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    writer.WriteLine(d + ": " + msg);
                }
            }
            catch (Exception e)
            {
                ;
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
