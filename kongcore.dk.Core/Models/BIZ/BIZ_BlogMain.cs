using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using kongcore.dk.Core.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_BlogMain
    {
        public List<Blog> GetBlogs(ContentHelper helper, SearchViewModel model)
        {
            List<IPublishedContent> blogs = new List<IPublishedContent>();

            if(model.IsNull())
            {
                blogs = helper.NodesType(helper._CurrentRoot(), "blogItem").OrderByDescending(x => x.CreateDate).ToList();
                //list = helper._GetItems(blogs.ToList(), "imagePicker", "blogItemTitle", "blogItemContent", null);
            }
            else
            {
                var blog_main = helper.NodeType(helper._Root(), "blogMain");
                var blog_items = blog_main.Children.Where(x => x.ContentType.Alias == "blogItem");
                blogs = model.search_string.IsNullOrEmpty() ? 
                    blog_items.ToList() :
                    blog_items.Where(x =>
                    ((string)x.GetProperty("BlogItemTitle").GetValue().ToString().ToLower()).Contains(model.search_string) ||
                    ((string)x.GetProperty("BlogItemContent").GetValue().ToString().ToLower()).Contains(model.search_string)
                ).ToList();
            }

            if (blogs.IsNull())
                throw new Exception();

            List<Blog> items = new List<Blog>();
            foreach (var item in blogs)
            {
                IPublishedContent mediaItem = helper.GetMedia1(item, "imagePicker");
                if (mediaItem.IsNull())
                    continue;

                string title = helper.GetValue(item, "blogItemTitle");
                string content = helper.GetValue(item, "blogItemContent").RichStrip();

                string url = item.Url();
                string alt = item.Name;
                string media_url = mediaItem?.Url();
                string create_date = "" + item.CreateDate.ToString("dd/MM-yyyy");


                items.Add(new Blog() { title = title, alt = alt, media_url = media_url, content = content, create_date = create_date });
            }

            return items;
        }
    }
}
