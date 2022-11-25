using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_SkillsMain : Umbraco.Web.PublishedModels.SkillsMain
    {
        public DTO_SkillsMain(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        //public DTO_HomePage(IPublishedContent _c, List<Site> _s) : base(_c)
        //{
        //    this._content = _c;
        //    this.sites = _s;
        //}

        public IPublishedContent _content { get; set; }
        public string skillsTitle { get; set; }
        public string skillsBodyText { get; set; }
        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block2header { get; set; }
        public string block2text { get; set; }
        public string block2buttontext { get; set; }

        public List<Item> skills { get; set; }
    }
}
