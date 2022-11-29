using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_Master
    {
        public DTO_Master ToDTO(ViewDataDictionary view, ContentHelper helper)
        {
            DTO_Master dto = new DTO_Master(helper._Root());

            dto.css_line2 = (string)view["page"] == "blogmain" ?
                "alt1" :
                (string)view["page"] == "skillsmain" ?
                "alt2" :
                "alt3";
            dto.css_camp = (string)view["page"] == "blogmain" ?
                "color-gray-1" :
                (string)view["page"] == "skillsmain" ?
                "color-gray-1" :
                "color-orange";
            dto.css_menu = (string)view["page"] == "blogmain" ?
                "bold color-black text-shadow-black " :
                (string)view["page"] == "skillsmain" ?
                "color-black" :
                "color-white";
            dto.css_brand = (string)view["page"] == "blogmain" ?
                "color-white text-shadow-black " :
                (string)view["page"] == "skillsmain" ?
                "color-black" :
                "color-orange";
            dto.css_brand_sub_bg = (string)view["page"] == "blogmain" ?
                "padding-all background-color-gray box-shadow-black " :
                (string)view["page"] == "skillsmain" ?
                "padding-all background-color-gray" :
                "";
            dto.css_brand_sub_col = (string)view["page"] == "blogmain" ?
                "color-white" :
                (string)view["page"] == "skillsmain" ?
                "color-white" :
                "color-orange";
            dto.css_font = (string)view["page"] == "blogmain" ?
                "caviardreams" :
                (string)view["page"] == "skillsmain" ?
                "caviardreams" :
                "caviardreams";

            IPublishedContent root = helper._Root();
            IPublishedContent current = helper._CurrentRoot();

            //var selection = root.Nodes(site);
            var campNode = helper.NodeType(root, "campaignMain");
            var settingsNode = helper.NodeType(root, "settings");

            if (campNode.IsNull())
                throw new Exception();
            if (settingsNode.IsNull())
                throw new Exception();

            string camp = helper.GetPropertyValue(campNode, "campaignText");
            string link = helper.GetPropertyValue(campNode, "campaignLink");
            string link_text = helper.GetPropertyValue(campNode, "campaignLinkText");
            dto.campaign = "<span class=\"bold\">" + camp + "</span>" + (string.IsNullOrEmpty("" + link) ? "" : "&nbsp;<a class=\"display-inline bold " + dto.css_camp + "\" href=\"" + link + "\">" + link_text.Replace(" ", "&nbsp;") + "</a>");

            dto.sitename = helper.GetPropertyValue(settingsNode, "siteName");
            dto.slogan1 = helper.GetPropertyValue(settingsNode, "siteSlogan");
            dto.slogan2 = helper.GetPropertyValue(settingsNode, "siteSlogan2");

            dto.footerTextContact = helper.GetValueFallback(root, "footerTextContact").Replace(" ", " ");
            dto.footerTextContact2 = helper.GetValueFallback(root, "footerTextContact2").FormatEmailSimple();
            dto.footerTextContact3 = helper.GetValueFallback(root, "footerTextContact2");

            dto.footerText = helper.GetValueFallback(root, "footerText").RichStrip();
            dto.year = DateTime.Now.Year.ToString();
            dto.footerText2 = helper.GetValueFallback(root, "footerText2");

            return dto;
        }
    }
}
