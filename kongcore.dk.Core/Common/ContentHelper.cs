using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace kongcore.dk.Core.Common
{
    public class ContentHelper
    {
        UmbracoHelper helper;
        IPublishedContent current;

        public ContentHelper(UmbracoHelper _h)
        {
            helper = _h;
        }

        public ContentHelper(IPublishedContent _c)
        {
            current = _c;
        }

        public ContentHelper(UmbracoHelper _h, IPublishedContent _c)
        {
            helper = _h;
            current = _c;
        }

        public IPublishedContent _Root()
        {
            return helper.ContentAtRoot().First(); ;
        }

        public IPublishedContent _CurrentRoot()
        {
            return current;
        }





        public string GetValue(IPublishedContent item, string elem)
        {
            if (item.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            string res = "" + item.Value(elem);

            return res;
        }

        public string GetValueFallback(IPublishedContent item, string elem)
        {
            if (item.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            string res = "" + item.Value(elem, fallback: Fallback.ToAncestors);

            return res;
        }

        public string GetPropertyValue(IPublishedContent item, string elem)
        {
            if (item.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            string res = "" + item.GetProperty(elem).GetValue();

            return res;
        }






        public IPublishedContent NodeType(IPublishedContent site, string elem)
        {
            if (site.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            IPublishedContent cont = site.ChildrenOfType(elem).Where(x => x.IsVisible()).FirstOrDefault();
            if (cont.IsNull())
                throw new Exception();

            return cont;
        }

        public List<IPublishedContent> Nodes(IPublishedContent site)
        {
            if (site.IsNull())
                throw new Exception();
            
            List<IPublishedContent> list = site.Children.Where(x => x.IsVisible()).ToList();
            if (list.IsNull())
                throw new Exception();

            return list;
        }

        public List<IPublishedContent> NodesType(IPublishedContent site, string elem)
        {
            if (site.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            List<IPublishedContent> list = site.ChildrenOfType(elem).Where(x => x.IsVisible()).ToList();
            if (list.IsNull())
                throw new Exception();

            return list;
        }

        public IPublishedContent NodeName(IPublishedContent site, string elem) 
        {
            if (site.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            IPublishedContent item = site.Children.Where(x => x.Name == elem).First();
            if (item.IsNull())
                throw new Exception();

            return item;
        }






        public IPublishedContent GetMedia1(IPublishedContent item, string elem)
        {
            if (item.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            if (!item.HasValue(elem))
                return null;

            List<IPublishedContent> v1 = (List<IPublishedContent>)item.GetProperty(elem)?.GetValue();
            if (v1.IsNull() || v1.Count <= 0)
                return null;
            var v3 = v1.FirstOrDefault();
            var v4 = v1.Skip(1).FirstOrDefault();
            if (v3.IsNull() && v4.IsNull())
                return null;

            var v5 = v3 is Folder ? v4 : v3;
            IPublishedContent mediaItem = helper.Media(v5.Id);
            if (mediaItem.IsNull())
                return null;

            return mediaItem;
        }

        public IPublishedContent GetMedia2(IPublishedContent item, string elem)
        {
            if (item.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();
                        
            List<IPublishedContent> v1 = item.Value<IEnumerable<IPublishedContent>>(elem).ToList();
            if (v1.IsNull() || v1.Count <= 0)
                return null;
            
            var v3 = v1.FirstOrDefault();
            var v4 = v1.Skip(1).FirstOrDefault();
            if (v3.IsNull() && v4.IsNull())
                return null;

            var v5 = v3 is Folder ? v4 : v3;
            IPublishedContent mediaItem = helper.Media(v5.Id);
            if (mediaItem.IsNull())
                return null;

            return mediaItem;
        }/**/
    }
}
