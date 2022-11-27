using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_ArticleItem
    {
        public Img GetImage(ContentHelper helper, string elem, string alt)
        {
            if (alt.IsNull())
                throw new Exception();

            IPublishedContent item = helper.GetMedia2(helper._CurrentRoot(), elem);
            if (item.IsNull())
                throw new Exception();

            string url = item.Url();
            string article_title = "" + helper._CurrentRoot().Value(alt);
            Img _i = new Img() { url = url, alt = article_title };

            return _i;
        }

        public DTO_ArticleItem ToDTO(ContentHelper helper) 
        {
            IPublishedContent root = helper._Root();
            IPublishedContent current = helper._CurrentRoot();

            DTO_ArticleItem dto = new DTO_ArticleItem(helper._CurrentRoot());

            dto.articleTitle = helper.GetValue(current, "articleTitle");
            dto.articleLink = helper.GetValue(current, "articleLink");
            dto.articleContent = helper.GetValue(current, "articleContent").FormatParagraph();

            dto.articleAboutHeader = helper.GetValue(current, "articleAboutHeader");
            dto.articleAboutText = helper.GetValue(current, "articleAboutText").FormatParagraph();
            dto.articleTaskHeader = helper.GetValue(current, "articleTaskHeader");
            dto.articleTaskText = helper.GetValue(current, "articleTaskText").FormatParagraph();

            IPublishedContent block1Node = helper.NodeType(root, "block1");
            dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
            dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
            dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

            IPublishedContent block2Node = helper.NodeType(root, "block2");
            dto.block2header = helper.GetPropertyValue(block2Node, "block2Header");
            dto.block2text = helper.GetPropertyValue(block2Node, "block2Text").FormatParagraph();
            dto.block2buttontext = helper.GetPropertyValue(block2Node, "block2ButtonText");
                        
            dto.img = GetImage(helper, "articleImageMain", "articleTitle");
            dto.img1 = GetImage(helper, "articleImageMob1", "articleTitle");
            dto.img2 = GetImage(helper, "articleImageMob2", "articleTitle");
            dto.img3 = GetImage(helper, "articleImageMob3", "articleTitle");

            return dto;
        }
    }
}
