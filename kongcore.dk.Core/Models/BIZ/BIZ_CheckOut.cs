using kongcore.dk.Core._Common;
using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    public class BIZ_CheckOut
    {
        SessionSingleton session;

        public BIZ_CheckOut()
        {
            session = SessionSingleton.Current;
        }

        public OrderItemOBJ GetOrders()
        {
            string token = session.Tokens[0];
            if (token.IsNullOrEmpty())
                throw new Exception("A-Ok, Handled.");

            ShoppingCart sc = new ShoppingCart();
            OrderItemOBJ model = sc.GetOrderByToken(token);
            if (model.IsNull())
                throw new Exception("A-Ok, Handled.");

            if (!CheckHelper.CheckOrderItemOBJ2(model))
                throw new Exception("A-Ok, Handled.");

            List<string> tokens = session.Tokens;
            model.tokenexists = tokens.Contains(model.token);

            return model;
        }

        public DTO_CheckOut ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();
            DTO_CheckOut dto = new DTO_CheckOut(current);

            dto.header = helper.GetValue(current, "header");
            dto.text1 = helper.GetValue(current, "text1");

            dto.order = GetOrders();

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
