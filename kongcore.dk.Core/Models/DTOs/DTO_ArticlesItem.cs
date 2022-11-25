using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.DTOs
{
    public class DTO_ArticleItem : Umbraco.Web.PublishedModels.ArticlesItem
    {
        public DTO_ArticleItem(IPublishedContent _c) : base(_c)
        {
            this._content = _c;
        }

        public DTO_ArticleItem(IPublishedContent _c, Img _i) : base(_c)
        {
            this._content = _c;
            this.img = _i;
        }

        public IPublishedContent _content { get; set; }
        public string articleTitle { get; set; }
        public string articleLink { get; set; }
        public string articleContent { get; set; }

        public string articleAboutHeader { get; set; }
        public string articleAboutText { get; set; }
        public string articleTaskHeader { get; set; }
        public string articleTaskText { get; set; }

        public string block1header { get; set; }
        public string block1text { get; set; }
        public string block1buttontext { get; set; }

        public string block2header { get; set; }
        public string block2text { get; set; }
        public string block2buttontext { get; set; }


        public Img img { get; set; }
        public Img img1 { get; set; }
        public Img img2 { get; set; }
        public Img img3 { get; set; }
    }
}
