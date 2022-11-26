using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_ArticlesMain
    {
        public List<Article> GetArticles(ContentHelper helper)
        {
            List<IPublishedContent> articles = new List<IPublishedContent>();

            articles = helper.NodesType(helper._CurrentRoot(), "articlesItem").OrderByDescending(x => x.CreateDate).ToList();
            //dto.articles = helper.GetItems(articles.ToList(), "articleImageMain", "articleTitle", "articleContent", "articleLink");

            if (articles.IsNull())
                throw new Exception();

            List<Article> items = new List<Article>();
            foreach (var item in articles)
            {
                IPublishedContent mediaItem = helper.GetMedia1(item, "articleImageMain");
                if (mediaItem.IsNull())
                    continue;

                string title = helper.GetValue(item, "articleTitle");
                string content = helper.GetValue(item, "articleContent").RichStrip();
                
                string name = item.Name;
                string url = item.Url();
                string alt = item.Name;
                string media_url = mediaItem.Url();
                
                items.Add(new Article() { title = title, url = url, alt = alt, media_url = media_url, content = content });
            }

            return items;
        }
    }
}
