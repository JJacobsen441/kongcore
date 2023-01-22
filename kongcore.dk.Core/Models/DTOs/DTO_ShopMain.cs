using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_ShopMain : Umbraco.Web.PublishedModels.Shop
    {
        public DTO_ShopMain(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        public DTO_ShopMain(IPublishedContent _c, List<Product> _p) : base(_c)
        {
            this._content = _c;
            this.products = _p;
        }

        public IPublishedContent _content { get; set; }
        public string title { get; set; }
        public string text1 { get; set; }
        
        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block3header { get; set; }
        public string block3text { get; set; }
        public string block3buttontext { get; set; }


        public List<Product> products = new List<Product>();
    }
}
