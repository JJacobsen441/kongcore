using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_ShoppingCart : Umbraco.Web.PublishedModels.ShoppingCart
    {
        public DTO_ShoppingCart(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        public IPublishedContent _content { get; set; }
        public int id { get; set; }
        public string header { get; set; }
        public string text1 { get; set; }

        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block3header { get; set; }
        public string block3text { get; set; }
        public string block3buttontext { get; set; }

        public List<OrderItemOBJ> orders { get; set; }
    }
}
