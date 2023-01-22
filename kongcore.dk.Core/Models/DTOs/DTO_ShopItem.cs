using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_ShopItem : Umbraco.Web.PublishedModels.ShopItem
    {
        public DTO_ShopItem(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }
        
        public IPublishedContent _content { get; set; }
        public string id { get; set; }
        public string item_url { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }

        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block3header { get; set; }
        public string block3text { get; set; }
        public string block3buttontext { get; set; }
    }
}
