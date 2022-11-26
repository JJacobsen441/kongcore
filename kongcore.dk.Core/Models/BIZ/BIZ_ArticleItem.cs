using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_ArticleItem
    {
        public Img GetImage(ContentHelper helper, string elem, string alt)
        {
            if (alt.IsNull())
                throw new Exception();

            IPublishedContent item = helper.GetMedia2(helper._CurrentRoot(), elem);
            if (item.IsNull())
                throw new Exception();

            string url = item.Url();
            string article_title = "" + helper._CurrentRoot().Value(alt);
            Img _i = new Img() { url = url, alt = article_title };

            return _i;
        }
    }
}
