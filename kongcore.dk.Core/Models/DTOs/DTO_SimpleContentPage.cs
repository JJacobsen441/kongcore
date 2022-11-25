using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_SimpleContentPage : Umbraco.Web.PublishedModels.SimpleContentPage
    {
        public DTO_SimpleContentPage(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        //public DTO_HomePage(IPublishedContent _c, List<Site> _s) : base(_c)
        //{
        //    this._content = _c;
        //    this.sites = _s;
        //}

        public IPublishedContent _content { get; set; }
        public string bodyHeader { get; set; }
        public string bodyText { get; set; }

        public string contactEmployee1 { get; set; }
        public string contactEmployee2 { get; set; }
    }
}
