using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_Procedure : Umbraco.Web.PublishedModels.Articles
    {
        public DTO_Procedure(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        //public DTO_HomePage(IPublishedContent _c, List<Site> _s) : base(_c)
        //{
        //    this._content = _c;
        //    this.sites = _s;
        //}

        public IPublishedContent _content { get; set; }
        public string procedureHeader { get; set; }
        public string procedureBodyText { get; set; }
        public string procedureMail { get; set; }
        public string procedureText { get; set; }

        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block2header { get; set; }
        public string block2text { get; set; }
        public string block2buttontext { get; set; }
    }
}
