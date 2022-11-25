using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_ArticlesMain : Umbraco.Web.PublishedModels.Articles
    {
        public DTO_ArticlesMain(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        //public DTO_HomePage(IPublishedContent _c, List<Site> _s) : base(_c)
        //{
        //    this._content = _c;
        //    this.sites = _s;
        //}

        public IPublishedContent _content { get; set; }
        public string articlesTitle { get; set; }
        public string articlesBodyText { get; set; }

        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block2header { get; set; }
        public string block2text { get; set; }
        public string block2buttontext { get; set; }

        public List<Item> articles = new List<Item>();
    }
}
