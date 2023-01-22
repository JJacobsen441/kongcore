using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.DTOs;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    public class BIZ_ShopItem
    {
        public DTO_ShopItem GetProduct(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();


            DTO_ShopItem dto = new DTO_ShopItem(current);
            dto.product_name = helper.GetValue(current, "productName");
            dto.product_price = helper.GetValue(current, "productPrice");
            dto.id = helper.GetID(current);

            int index = dto.product_price.IndexOf(".");
            if (index < 0)
                index = dto.product_price.IndexOf(",");
            if (index > 0)
                dto.product_price = dto.product_price.Substring(0, index);
            //if (price == "0")
            //    price = "0.00";

            //if (index < 0)
            //    dto.product_price += ".00";

            //if (dto.product_price.Length < index + 1)
            //    dto.product_price += "0";

            return dto;
        }

        public DTO_ShopItem GetProductByID(ContentHelper helper, int id)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent _this = helper.GetByID(id);


            DTO_ShopItem dto = new DTO_ShopItem(_this);
            dto.product_name = helper.GetValue(_this, "productName");
            dto.product_price = helper.GetValue(_this, "productPrice");
            dto.id = "" + _this.Id;



            int index = dto.product_price.IndexOf(".");
            if (index < 0)
                index = dto.product_price.IndexOf(",");
            if (index > 0)
                dto.product_price = dto.product_price.Substring(0, index);

            //if (price == "0")
            //    price = "0.00";

            //if (index < 0)
            //    dto.product_price += "";

            //if (dto.product_price.Length < index + 1)
            //    dto.product_price += "";

            return dto;
        }

        public DTO_ShopItem ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();

            DTO_ShopItem dto = GetProduct(helper);

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

        public DTO_ShopItem ToDTO(ContentHelper helper, int id)
        {
            IPublishedContent root = helper.Root();
            //IPublishedContent current = helper.RootCurrent();

            DTO_ShopItem dto = GetProductByID(helper, id);

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

