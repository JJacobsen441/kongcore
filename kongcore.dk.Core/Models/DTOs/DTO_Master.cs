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

        public string footerTextContact { get; set; }
        public string footerTextContact2 { get; set; }
        public string footerTextContact3 { get; set; }
        public string footerText { get; set; }
        public string year { get; set; }
        public string footerText2 { get; set; }

        public string title { get; set; }
        public string description { get; set; }
    }
}
