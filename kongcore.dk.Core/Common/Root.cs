using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Common
{
    public class Root
    {
        UmbracoHelper helper;
        public Root(UmbracoHelper _h)
        {
            helper = _h;
        }

        public IPublishedContent _Root()
        {
            return helper.ContentAtRoot().First(); ;
        }

        public IPublishedContent ChildType(string elem)
        {
            var site = _Root();
            IPublishedContent cont = site.ChildrenOfType(elem).Where(x => x.IsVisible()).FirstOrDefault();
            if (cont.IsNull())
                throw new Exception();

            return cont;
        }

        public List<IPublishedContent> ChildsType(IPublishedContent _parent, string elem)
        {
            List<IPublishedContent> list = _parent.ChildrenOfType(elem).Where(x => x.IsVisible()).ToList();
            if (list.IsNull())
                throw new Exception();

            return list;
        }

        public IPublishedContent ChildName(string elem) 
        {
            var site = _Root();
            IPublishedContent item = site.Children.Where(x => x.Name == elem).First();
            if (item.IsNull())
                throw new Exception();

            return item;
        }
    }
}
