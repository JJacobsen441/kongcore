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

            IPublishedContent article = helper.NodeType(helper._Root(), "articles");
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
    }
}

