using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    public class BIZ_Master
    {
        public DTO_Master ToDTO(ViewDataDictionary view, ContentHelper helper)
        {
            DTO_Master dto = new DTO_Master(helper._Root());

            switch ((string)view["page"])
            {
                case "skillsmain":
                    dto.alt = "alt2";
                    break;
                case "casesmain":
                    dto.alt = "alt1";
                    break;
                case "blogmain":
                    dto.alt = "alt3";
                    break;
                default:
                    dto.alt = "alt1";
                    break;
            }
                        
            IPublishedContent root = helper._Root();
            IPublishedContent current = helper._CurrentRoot();

            //var selection = root.Nodes(site);
            var campNode = helper.NodeType(root, "campaignMain");
            var settingsNode = helper.NodeType(root, "settings");
            var masterNode = helper.NodeType(root, "master");

            if (campNode.IsNull())
                throw new Exception();
            if (settingsNode.IsNull())
                throw new Exception();

            string camp = helper.GetPropertyValue(campNode, "campaignText");
            string link = helper.GetPropertyValue(campNode, "campaignLink");
            string text = helper.GetPropertyValue(campNode, "campaignLinkText");
            dto.camp = camp;
            dto.camp_link = link;
            dto.camp_text = text.Replace(" ", "&nbsp;");

            dto.sitename = helper.GetPropertyValue(settingsNode, "siteName");
            dto.slogan1 = helper.GetPropertyValue(settingsNode, "siteSlogan");
            dto.slogan2 = helper.GetPropertyValue(settingsNode, "siteSlogan2");

            dto.footerTextContact = helper.GetValueFallback(root, "footerTextContact").Replace(" ", " ");
            dto.footerTextContact2 = helper.GetValueFallback(root, "footerTextContact2").FormatEmailSimple();
            dto.footerTextContact3 = helper.GetValueFallback(root, "footerTextContact2");

            dto.footerText = helper.GetValueFallback(root, "footerText").RichStrip();
            dto.year = DateTime.Now.Year.ToString();
            dto.footerText2 = helper.GetValueFallback(root, "footerText2");

            dto.title = helper.GetPropertyValue(masterNode, "title");
            dto.description = helper.GetPropertyValue(masterNode, "description");

            return dto;
        }
    }
}
