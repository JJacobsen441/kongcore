using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_BlogMain : Umbraco.Web.PublishedModels.BlogMain
    {
        public DTO_BlogMain(IPublishedContent _c) : base(_c)
        {
            this.content = _c;
        }

        public DTO_BlogMain(IPublishedContent _c, List<Item> data) : base(_c)
        {
            this.content = _c;
            this.blogs = data;
        }

        public IPublishedContent content { get; set; }
        public string blogTitle { get; set; }
        public string blogBodyText { get; set; }

        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block2header { get; set; }
        public string block2text { get; set; }
        public string block2buttontext { get; set; }

        public List<Item> blogs { get; set; }
    }
}
