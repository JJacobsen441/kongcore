using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_HomePage : Umbraco.Web.PublishedModels.HomePage
    {
        public DTO_HomePage(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        public DTO_HomePage(IPublishedContent _c, List<Site> _s) : base(_c)
        {
            this._content = _c;
            this.sites = _s;
        }

        public IPublishedContent _content { get; set; }
        public string quote1 { get; set; }
        public string quote2 { get; set; }
        public string quote3 { get; set; }
        public string aboutTitle { get; set; }
        public string aboutText { get; set; }
        public string bodyText1Header { get; set; }
        public string bodyText1 { get; set; }
        public string bodyText2Header { get; set; }
        public string bodyText2 { get; set; }
        public string bodyText3Header { get; set; }
        public string bodyText3 { get; set; }
        public string bodyText4Header { get; set; }
        public string bodyText4 { get; set; }
        public string bodyText5Header { get; set; }
        public string bodyText5 { get; set; }
        public string bodyText6Header { get; set; }
        public string bodyText6 { get; set; }
        public string conclusionTitle { get; set; }
        public string conclusionText { get; set; }

        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block3header { get; set; }
        public string block3text { get; set; }
        public string block3buttontext { get; set; }


        public List<Site> sites = new List<Site>();
    }
}
