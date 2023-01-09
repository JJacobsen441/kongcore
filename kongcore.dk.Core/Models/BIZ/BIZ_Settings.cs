using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    public class BIZ_Settings
    {
        public DTO_Settings ToDTO(ViewDataDictionary view, ContentHelper helper)
        {
            DTO_Settings dto = new DTO_Settings(helper.Root());

            switch ((string)view["page"])
            {
                case "skillsmain":
                    dto.alt = "alt2";
                    break;
                case "casesmain":
                    dto.alt = "alt4";
                    break;
                case "blogmain":
                    dto.alt = "alt3";
                    break;
                default:
                    dto.alt = "alt1";
                    break;
            }
                        
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();

            //var selection = root.Nodes(site);
            //var campNode = helper.NodeType(root, "campaignMain");
            var settingsNode = helper.NodeType(root, "settings");
            //var masterNode = helper.NodeType(root, "master");

            if (settingsNode.IsNull())
                throw new Exception();

            //campaign
            string camp = helper.GetPropertyValue(settingsNode, "campaignText");
            string link = helper.GetPropertyValue(settingsNode, "campaignLink");
            string text = helper.GetPropertyValue(settingsNode, "campaignLinkText");
            dto.camp = camp;
            dto.camp_link = link;
            dto.camp_text = text.Replace(" ", "&nbsp;");

            //settings
            dto.sitename = helper.GetPropertyValue(settingsNode, "siteName");
            dto.slogan1 = helper.GetPropertyValue(settingsNode, "siteSlogan");
            dto.slogan2 = helper.GetPropertyValue(settingsNode, "siteSlogan2");

            //footer
            dto.footerTextContact = helper.GetValueFallback(root, "footerTextContact").Replace(" ", " ");
            dto.footerTextContact2 = helper.GetValueFallback(root, "footerTextContact2").FormatEmailSimple();
            dto.footerTextContact3 = helper.GetValueFallback(root, "footerTextContact2");

            dto.footerText = helper.GetValueFallback(root, "footerText").RichStrip();
            dto.year = DateTime.Now.Year.ToString();
            dto.footerText2 = helper.GetValueFallback(root, "footerText2");

            //master
            dto.title = helper.GetPropertyValue(settingsNode, "title");
            dto.description = helper.GetPropertyValue(settingsNode, "description");

            return dto;
        }
    }
}
