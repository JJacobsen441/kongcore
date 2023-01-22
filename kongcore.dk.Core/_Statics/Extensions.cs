using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kongcore.dk.Core._Statics
{
    public static class Extensions
    {
        public static string FormatParagraph(this string _s)
        {
            if (string.IsNullOrEmpty(_s))
                return "";

            return StaticsHelper.RemoveFirstParagraphTag(StaticsHelper.RichStrip("" + _s));
        }

        public static string RichStrip(this string _s)
        {
            if (string.IsNullOrEmpty(_s))
                return "";

            return StaticsHelper.RichStrip("" + _s);
        }

        public static string FormatEmailAdvanced(this string _s)
        {
            if (string.IsNullOrEmpty(_s))
                return "";

            return StaticsHelper.RichStrip("" +
                   StaticsHelper.FormatEmailIcon("" +
                   StaticsHelper.FormatPhoneIcon("" +
                   StaticsHelper.FormatMail("" + _s))));
        }

        public static string FormatEmailSimple(this string _s)
        {
            if (string.IsNullOrEmpty(_s))
                return "";

            return StaticsHelper.FormatMail("" + _s);
        }

        public static MvcHtmlString HtmlWithBreaksFor(this HtmlHelper html, string text/*, Expression<Func<TModel, TValue>> expression*/)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create("");

            string model = "" + html.Raw(text.Replace(Environment.NewLine, "<br />"));

            return MvcHtmlString.Create(model);
        }

        public static string StringWithBreaksFor(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            string model = "" + text.Replace(Environment.NewLine, "<br />");

            return model;
        }

        public static string HtmlEncode(string html)
        {
            if (string.IsNullOrEmpty(html))
                return "";

            var httpUtil = new HttpServerUtilityWrapper(HttpContext.Current.Server);
            string encoded = httpUtil.HtmlEncode(html).Replace(Environment.NewLine, "<br />");

            return encoded;
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> value)
        {
            return value == null || value.Count() == 0;
        }

        public static bool IsNull<T>(this T value)
        {
            return value == null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Length == 0;
        }
    }
}