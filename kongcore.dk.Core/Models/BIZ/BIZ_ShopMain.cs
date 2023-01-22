using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_ShopMain
    {
        public List<Product> GetProducts(ContentHelper helper)
        {
            List<IPublishedContent> products = new List<IPublishedContent>();

            products = helper.NodesType(helper.RootCurrent(), "shopItem").OrderByDescending(x => x.CreateDate).ToList();
            //dto.articles = helper.GetItems(articles.ToList(), "articleImageMain", "articleTitle", "articleContent", "articleLink");

            if (products.IsNull())
                throw new Exception();

            List<Product> items = new List<Product>();
            foreach (var item in products.OrderBy(x=>x.CreateDate))
            {
                string id = "" + item.Id;
                string item_url = item.Url();
                string name = helper.GetValue(item, "productName");
                string price = "" + helper.GetValue(item, "productPrice");

                int index = price.IndexOf(".");
                if (index < 0)
                    index = price.IndexOf(",");
                if (index > 0)
                    price = price.Substring(0, index);

                //if (price == "0")
                //    price = "0.00";

                //if (index < 0)
                //    price += ".00";

                //if (price.Length < index + 1)
                //    price += "0";

                items.Add(new Product() { id = id, item_url = item_url, product_name = name, product_price = price });
            }

            return items;
        }

        public DTO_ShopMain ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();

            DTO_ShopMain dto = new DTO_ShopMain(current);
            dto.title = helper.GetValue(current, "header");
            dto.text1 = helper.GetValue(current, "text1").FormatParagraph();

            dto.products = GetProducts(helper);

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

