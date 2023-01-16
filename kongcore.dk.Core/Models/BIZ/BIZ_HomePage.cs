using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_HomePage
    {
        public List<Site> GetSites(ContentHelper helper)
        {
            List<IPublishedContent> sites = new List<IPublishedContent>();

            IPublishedContent article = helper.NodeType(helper.Root(), "articles");
            sites = helper.NodesType(article, "articlesItem").OrderByDescending(x => x.CreateDate).ToList();
            //dto.sites = helper.GetItems(articles.ToList(), "articleImageBW", null, null, "articleLink");

            if (sites.IsNull())
                throw new Exception();

            List<Site> items = new List<Site>();
            foreach (var item in sites)
            {
                IPublishedContent mediaItem = helper.GetMedia1(item, "articleImageBW");
                if (mediaItem.IsNull())
                    continue;

                string alt = item.Name;
                string media_url = mediaItem.Url();
                string link = helper.GetValue(item, "articleLink");

                items.Add(new Site() { alt = alt, media_url = media_url, link = link });
            }

            return items;
        }

        public DTO_HomePage ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();

            DTO_HomePage dto = new DTO_HomePage(current);
            dto.aboutTitle = helper.GetValue(current, "aboutTitle");
            dto.aboutText = helper.GetValue(current, "aboutText").FormatParagraph();

            dto.conclusionTitle = helper.GetValue(current, "conclusionTitle");
            dto.conclusionText = helper.GetValue(current, "conclusionText").FormatParagraph();

            dto.bodyText1Header = helper.GetValue(current, "bodyText1Header");
            dto.bodyText1 = helper.GetValue(current, "bodyText1").FormatParagraph();
            dto.bodyText2Header = helper.GetValue(current, "bodyText2Header");
            dto.bodyText2 = helper.GetValue(current, "bodyText2").FormatParagraph();
            dto.bodyText3Header = helper.GetValue(current, "bodyText3Header");
            dto.bodyText3 = helper.GetValue(current, "bodyText3").FormatParagraph();
            dto.bodyText4Header = helper.GetValue(current, "bodyText4Header");
            dto.bodyText4 = helper.GetValue(current, "bodyText4").FormatParagraph();
            dto.bodyText5Header = helper.GetValue(current, "bodyText5Header");
            dto.bodyText5 = helper.GetValue(current, "bodyText5").FormatParagraph();
            dto.bodyText6Header = helper.GetValue(current, "bodyText6Header");
            dto.bodyText6 = helper.GetValue(current, "bodyText6").FormatParagraph();

            List<string> quotes = GeneralHelper.GetQuotes(helper, false);
            dto.quote1 = quotes[0];
            dto.quote2 = quotes[1];
            dto.quote3 = quotes[2];
            
            IPublishedContent block1Node = helper.NodeType(root, "block1");
            dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
            dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
            dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

            IPublishedContent block3Node = helper.NodeType(root, "block3");
            dto.block3header = helper.GetPropertyValue(block3Node, "block3Header");
            dto.block3text = helper.GetPropertyValue(block3Node, "block3Text").FormatParagraph();
            dto.block3buttontext = helper.GetPropertyValue(block3Node, "block3ButtonText");

            dto.sites = GetSites(helper);

            return dto;
        }
    }
}

