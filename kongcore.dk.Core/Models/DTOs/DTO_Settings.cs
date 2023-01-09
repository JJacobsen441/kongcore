using kongcore.dk.Core.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_Settings : Umbraco.Web.PublishedModels.HomePage
    {
        public DTO_Settings(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        //public DTO_HomePage(IPublishedContent _c, List<Site> _s) : base(_c)
        //{
        //    this._content = _c;
        //    this.sites = _s;
        //}

        public IPublishedContent _content { get; set; }

        public string alt { get; set; }
        public string camp { get; set; }
        public string camp_link { get; set; }
        public string camp_text { get; set; }
        
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
