using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_BlogMain
    {
        public List<IPublishedContent> GetBlogs(ContentHelper root, SearchViewModel model)
        {
            var blog_main = root.NodeType(root._Root(), "blogMain");
            var blog_items = blog_main.Children.Where(x => x.ContentType.Alias == "blogItem");
            List<IPublishedContent> _res = model.search_string.IsNullOrEmpty() ? 
                blog_items.ToList() :
                blog_items.Where(x =>
                ((string)x.GetProperty("BlogItemTitle").GetValue().ToString().ToLower()).Contains(model.search_string) ||
                ((string)x.GetProperty("BlogItemContent").GetValue().ToString().ToLower()).Contains(model.search_string)
                ).ToList();

            if (_res == null)
                throw new Exception();

            return _res;
        }
    }
}
