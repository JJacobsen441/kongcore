using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models
{
    public class BlogViewModel : Umbraco.Web.PublishedModels.BlogMain
    {
        public BlogViewModel(IPublishedContent content) : base(content)
        {

        }

        public IPublishedContent content { get; set; }
        public IEnumerable<IPublishedContent> data { get; set; }
    }
}
