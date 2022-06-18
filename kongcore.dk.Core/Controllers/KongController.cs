using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers
{
    public class KongController : SurfaceController
    {
        [HttpPost]
        public void Submit(ContactFormViewModel model)
        {
            //return CurrentUmbracoPage();


            int successPageId = 1094;// Int32.Parse((string)CurrentPage.GetProperty("fail").GetValue());
            int failPageId = 1095;// Int32.Parse((string)CurrentPage.GetProperty("fail").GetValue());

            if (!ModelState.IsValid)
            { Response.Redirect("/fail"); return; }

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Subject) || string.IsNullOrEmpty(model.Message))
            { Response.Redirect("/fail"); return; }

            if (!Statics.IsValidEmail(model.Email)) { Response.Redirect("/fail"); return; }

            bool ok;
            string name = StringHelper.OnlyAlphanumeric(model.Name, true, true, "<br />", Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string mail = StringHelper.OnlyAlphanumeric(model.Email, true, true, "<br />", Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string sub = StringHelper.OnlyAlphanumeric(model.Subject, true, true, "<br />", Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string mess = StringHelper.OnlyAlphanumeric(model.Message, true, true, "<br />", Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string message =
                name + "<br />" +
                mail + "<br />" +
                mess;


            Notification.Run(model.Email, "info@kongcore.dk", "info@kongcore.dk", model.Subject, Extensions.StringWithBreaksFor(message));

            Response.Redirect("/success");
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {

            //int failPageId = Statics.IsDebug(Request) ? 1095 : 1103;// Int32.Parse((string)CurrentPage.GetProperty("fail").GetValue());
            var _root = Umbraco.ContentAtRoot().First();
            var fail = _root.Children.Where(x => x.ContentType.Alias == "submitFail").First();
            int failPageId = fail.Id;


            if (!ModelState.IsValid)
                return RedirectToUmbracoPage(failPageId);

            if (string.IsNullOrEmpty(model.SearchString))
                return RedirectToUmbracoPage(failPageId);

            if (model.SearchString.Length > 20)
                return RedirectToUmbracoPage(failPageId);

            model.SearchString = HttpUtility.UrlDecode(model.SearchString);
            model.SearchString = model.SearchString.ToLower();

            bool ok;
            model.SearchString = StringHelper.OnlyAlphanumeric(model.SearchString, false, false, "no_tag", Characters.VeryLimited(), out ok);
            if (!ok)
                return RedirectToUmbracoPage(failPageId);

            //string id = "1071";
            var root = Umbraco.ContentAtRoot().First();//.Content(Int32.Parse(id));
            var blog_main = root.Children.Where(x => x.ContentType.Alias == "blogMain").First();
            var blog_items = blog_main.Children.Where(x => x.ContentType.Alias == "blogItem");
            IEnumerable<IPublishedContent> res = blog_items.Where(x =>
                ((string)x.GetProperty("BlogItemTitle").GetValue().ToString().ToLower()).Contains(model.SearchString) ||
                ((string)x.GetProperty("BlogItemContent").GetValue().ToString().ToLower()).Contains(model.SearchString)
                ).ToList();

            if(res == null)
                return RedirectToUmbracoPage(failPageId);

            //ViewBag.Items = res.Count() >= 0 ? res : null;
            BlogViewModel _m = new BlogViewModel(CurrentPage);
            _m.content = CurrentPage;
            _m.data = res.Count() >= 0 ? res : null;

            return View("BlogMain", (BlogViewModel)_m);
        }
    }
}
