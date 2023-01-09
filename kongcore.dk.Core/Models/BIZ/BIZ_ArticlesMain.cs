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

            articles = helper.NodesType(helper.RootCurrent(), "articlesItem").OrderByDescending(x => x.CreateDate).ToList();
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

        public DTO_ArticlesMain ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();

            DTO_ArticlesMain dto = new DTO_ArticlesMain(current);

            dto.articlesTitle = helper.GetValue(current, "articlesTitle");
            dto.articlesBodyText = helper.GetValue(current, "articlesBodyText").FormatParagraph();

            IPublishedContent block1Node = helper.NodeType(root, "block1");
            dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
            dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
            dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

            IPublishedContent block3Node = helper.NodeType(root, "block2");
            dto.block2header = helper.GetPropertyValue(block3Node, "block2Header");
            dto.block2text = helper.GetPropertyValue(block3Node, "block2Text").FormatParagraph();
            dto.block2buttontext = helper.GetPropertyValue(block3Node, "block2ButtonText");

            BIZ_ArticlesMain biz_articles = new BIZ_ArticlesMain();
            dto.articles = biz_articles.GetArticles(helper);

            List<string> quotes = GeneralHelper.GetQuotes(helper, true);
            dto.quote1 = quotes[0];
            dto.quote2 = quotes[1];
            dto.quote3 = quotes[2];

            return dto;
        }
    }
}
