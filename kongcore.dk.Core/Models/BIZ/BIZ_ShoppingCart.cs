using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.DTOs;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    public class BIZ_ShoppingCart
    {
        SessionSingleton session;
        
        public BIZ_ShoppingCart()
        {
            session = SessionSingleton.Current;
        }

        public List<OrderItemOBJ> GetOrders()
        {
            List<OrderItemOBJ> list = session.ShoppingCart;
            if (list.IsNull())
                list = new List<OrderItemOBJ>();

            return list;
        }

        public DTO_ShoppingCart ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();
            DTO_ShoppingCart dto = new DTO_ShoppingCart(current);

            dto.header = helper.GetValue(current, "header");
            dto.text1 = helper.GetValue(current, "text1");
            
            dto.orders = GetOrders();

            IPublishedContent block1Node = helper.NodeType(root, "block1");
            dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
            dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
            dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

            IPublishedContent block3Node = helper.NodeType(root, "block3");
            dto.block3header = helper.GetPropertyValue(block3Node, "block3Header");
            dto.block3text = helper.GetPropertyValue(block3Node, "block3Text").FormatParagraph();
            dto.block3buttontext = helper.GetPropertyValue(block3Node, "block3ButtonText");


            return dto;
        }
    }
}
