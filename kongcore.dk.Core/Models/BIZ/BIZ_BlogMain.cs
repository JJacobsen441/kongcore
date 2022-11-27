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
            IPublishedContent root = helper._Root();
            IPublishedContent current = helper._CurrentRoot();

            List<IPublishedContent> blogs = new List<IPublishedContent>();

            if(model.IsNull())
            {
                blogs = helper.NodesType(current, "blogItem").OrderByDescending(x => x.CreateDate).ToList();
            }
            else
            {
                var blog_items = helper.NodesType(current, "blogItem");
                blogs = model.search_string.IsNullOrEmpty() ? 
                    blog_items.ToList() :
                    blog_items.Where(x =>
                    helper.GetPropertyValue(x, "BlogItemTitle").ToLower().Contains(model.search_string) ||
                    helper.GetPropertyValue(x, "BlogItemContent").ToLower().Contains(model.search_string)
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

        public DTO_BlogMain ToDTO(ContentHelper helper, SearchViewModel model) 
        {
            IPublishedContent root = helper._Root();
            IPublishedContent current = helper._CurrentRoot();

            DTO_BlogMain dto = new DTO_BlogMain(current);

            dto.blogTitle = helper.GetValue(current, "blogTitle");
            dto.blogBodyText = helper.GetValue(current, "blogBodyText").FormatParagraph();

            IPublishedContent block1Node = helper.NodeType(root, "block1");
            dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
            dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
            dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

            IPublishedContent block2Node = helper.NodeType(root, "block2");
            dto.block2header = helper.GetPropertyValue(block2Node, "block2Header");
            dto.block2text = helper.GetPropertyValue(block2Node, "block2Text").FormatParagraph();
            dto.block2buttontext = helper.GetPropertyValue(block2Node, "block2ButtonText");

            dto.blogs = GetBlogs(helper, model);

            return dto;
        }
    }
}
