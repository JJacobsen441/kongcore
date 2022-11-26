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

            string res = "" + item.Value(elem, fallback: Fallback.ToAncestors);

            return res;
        }

        public string GetValueFallback(IPublishedContent item, string elem)
        {
            if (item.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();

            string res = "" + item.Value(elem);

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






        public IPublishedContent GetMedia(IPublishedContent item, string elem)
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

        public Img GetImage(IPublishedContent item, string elem, string alt)
        {
            if (item.IsNull())
                throw new Exception();
            if (elem.IsNull())
                throw new Exception();
            if (alt.IsNull())
                throw new Exception();
            
            List<IPublishedContent> mediaItem = item.Value<IEnumerable<IPublishedContent>>(elem).ToList();
            if (mediaItem.IsNull() || mediaItem.Count <= 0)
                return null;
            
            var v3 = mediaItem.FirstOrDefault();
            var v4 = mediaItem.Skip(1).FirstOrDefault();
            if (v3.IsNull() && v4.IsNull())
                return null;

            var v5 = v3 is Folder ? v4 : v3;

            string url = v5.Url();
            string article_title = "" + _CurrentRoot().Value(alt);
            Img _i = new Img() { url = url, alt = article_title };

            return _i;
            
        }






        public List<Item> GetItems(List<IPublishedContent> items, string in_image_main, string in_title, string in_content, string in_link)
        {
            List<Item> list = new List<Item>();
            foreach (var item in items)
            {
                IPublishedContent mediaItem = null;
                if(!in_image_main.IsNull())
                {
                    mediaItem = GetMedia(item, in_image_main);

                    if (mediaItem.IsNull())
                        continue;
                }

                string title = in_title.IsNull() ? "" : "" + item.Value(in_title);
                string content = in_content.IsNull() ? "" : StaticsHelper.RichStrip("" + @item.Value(in_content));
                string link = in_link.IsNull() ? "" : "" + item.Value(in_link);

                string url = item.Url();
                string alt = item.Name;
                string media_url = mediaItem?.Url();
                string name = item.Name;
                string create_date = "" + item.CreateDate.ToString("dd/MM-yyyy");


                list.Add(new Item() { title = title, url = url, alt = alt, media_url = media_url, name = name, content = content, link = link, create_date = create_date });
            }

            return list;
        }
    }
}
