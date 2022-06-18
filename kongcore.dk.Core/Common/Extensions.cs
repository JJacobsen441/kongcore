using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kongcore.dk.Core.Common
{
    public static class Extensions
    {
        public static MvcHtmlString HtmlWithBreaksFor(this HtmlHelper html, string text/*, Expression<Func<TModel, TValue>> expression*/)
        {
            //var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            //string model = html.Encode(metadata.Model).Replace(Environment.NewLine, "<br />");

            //if (string.IsNullOrEmpty(model))
            //    return MvcHtmlString.Empty;

            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Empty;

            //var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string model = "" + html.Raw(text.Replace(Environment.NewLine, "<br />"));


            return MvcHtmlString.Create(model);
        }

        public static string StringWithBreaksFor(string text/*, Expression<Func<TModel, TValue>> expression*/)
        {
            //var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            //string model = html.Encode(metadata.Model).Replace(Environment.NewLine, "<br />");

            //if (string.IsNullOrEmpty(model))
            //    return MvcHtmlString.Empty;

            if (string.IsNullOrEmpty(text))
                return "";

            //var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            
            string model = "" + text.Replace(Environment.NewLine, "<br />");


            return model;
        }

        public static string HtmlEncode(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var httpUtil = new HttpServerUtilityWrapper(HttpContext.Current.Server);
                //string encoded = httpUtil.HtmlEncode(html).Replace("\r\n", "<br />\r\n");
                string encoded = httpUtil.HtmlEncode(html).Replace(Environment.NewLine, "<br />");

                //if (String.IsNullOrEmpty(encoded))
                //    return MvcHtmlString.Empty;

                //return MvcHtmlString.Create(encoded);
                return encoded;
            }
            return "";
        }
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }
    }
}