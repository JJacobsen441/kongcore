using kongcore.dk.Core.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_Master : Umbraco.Web.PublishedModels.HomePage
    {
        public DTO_Master(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        //public DTO_HomePage(IPublishedContent _c, List<Site> _s) : base(_c)
        //{
        //    this._content = _c;
        //    this.sites = _s;
        //}

        public void Setup(ViewDataDictionary view, ContentHelper root)
        {
            css_line2 = (string)view["page"] == "blogmain" ?
                "alt1" :
                (string)view["page"] == "skillsmain" ?
                "alt2" :
                "alt3";
            css_camp = (string)view["page"] == "blogmain" ?
                "color-gray-1" :
                (string)view["page"] == "skillsmain" ?
                "color-gray-1" :
                "color-orange";
            css_menu = (string)view["page"] == "blogmain" ?
                "bold color-black" :
                (string)view["page"] == "skillsmain" ?
                "color-black" :
                "color-white";
            css_brand = (string)view["page"] == "blogmain" ?
                "color-white" :
                (string)view["page"] == "skillsmain" ?
                "color-black" :
                "color-orange";
            css_brand_sub_bg = (string)view["page"] == "blogmain" ?
                "padding-all background-color-gray" :
                (string)view["page"] == "skillsmain" ?
                "padding-all background-color-gray" :
                "";
            css_brand_sub_col = (string)view["page"] == "blogmain" ?
                "color-white" :
                (string)view["page"] == "skillsmain" ?
                "color-white" :
                "color-orange";
            css_font = (string)view["page"] == "blogmain" ?
                "caviardreams" :
                (string)view["page"] == "skillsmain" ?
                "caviardreams" :
                "caviardreams";
            
            var site = root._Root();
            //var selection = root.Nodes(site);
            var campNode = root.NodeType(site, "campaignMain");
            var settingsNode = root.NodeType(site, "settings");

            if (campNode.IsNull())
                throw new Exception();
            if (settingsNode.IsNull())
                throw new Exception();

            string camp = root.GetPropertyValue(campNode, "campaignText");
            string link = root.GetPropertyValue(campNode, "campaignLink");
            string link_text = root.GetPropertyValue(campNode, "campaignLinkText");
            campaign = "<span class=\"bold\">" + camp + "</span>" + (string.IsNullOrEmpty("" + link) ? "" : "&nbsp;<a class=\"display-inline bold " + css_camp + "\" href=\"" + link + "\">" + link_text.Replace(" ", "&nbsp;") + "</a>");

            sitename = root.GetPropertyValue(settingsNode, "siteName");
            slogan1 = root.GetPropertyValue(settingsNode, "siteSlogan");
            slogan2 = root.GetPropertyValue(settingsNode, "siteSlogan2");
        }

        public IPublishedContent _content { get; set; }

        public string css_line2 { get; set; }
        public string css_camp { get; set; }
        public string css_menu { get; set; }
        public string css_brand { get; set; }
        public string css_brand_sub_bg { get; set; }
        public string css_brand_sub_col { get; set; }
        public string css_font { get; set; }
        public string campaign { get; set; }
        public string sitename { get; set; }
        public string slogan1 { get; set; }
        public string slogan2 { get; set; }
    }
}
