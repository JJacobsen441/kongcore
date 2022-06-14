using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers
{
    public class KongController : SurfaceController
    {

        // GET: Kong
        [HttpPost]
        public void Submit(ContactFormViewModel model)
        {
            //return CurrentUmbracoPage();

            int successPageId = 1094;// Int32.Parse((string)CurrentPage.GetProperty("fail").GetValue());
            int failPageId = 1095;// Int32.Parse((string)CurrentPage.GetProperty("fail").GetValue());

            if (!ModelState.IsValid)
                Response.Redirect("/fail");

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Subject) || string.IsNullOrEmpty(model.Message))
                Response.Redirect("/fail");

            if (!Statics.IsValidEmail(model.Email)) { Response.Redirect("/fail"); return; }

            bool ok;
            string name = StringHelper.OnlyAlphanumeric(model.Name, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string mail = StringHelper.OnlyAlphanumeric(model.Email, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string sub = StringHelper.OnlyAlphanumeric(model.Subject, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string mess = StringHelper.OnlyAlphanumeric(model.Message, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok) { Response.Redirect("/fail"); return; }

            string message =
                name + "<br />" +
                mail + "<br />" +
                mess;


            Statics.Notification.Run(model.Email, "info@kongcore.dk", "info@kongcore.dk", model.Subject, Extensions.StringWithBreaksFor(message));

            Response.Redirect("/success");
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            int failPageId = 1095;// Int32.Parse((string)CurrentPage.GetProperty("fail").GetValue());
            

            if (!ModelState.IsValid)
                //Response.Redirect("/fail");
                return RedirectToUmbracoPage(failPageId);

            if (string.IsNullOrEmpty(model.SearchString))
                //Response.Redirect("/fail");
                return RedirectToUmbracoPage(failPageId);

            if (model.SearchString.Length > 20)
                //Response.Redirect("/fail");
                return RedirectToUmbracoPage(failPageId);

            model.SearchString = HttpUtility.UrlDecode(model.SearchString);
            model.SearchString = model.SearchString.ToLower();

            bool ok;
            model.SearchString = StringHelper.OnlyAlphanumeric(model.SearchString, false, false, "no_tag", Statics.Characters.VeryLimited(), out ok);
            if (!ok)
                //Response.Redirect("/fail");
                return RedirectToUmbracoPage(failPageId);

            //string id = "1071";
            var root = Umbraco.ContentAtRoot().First();//.Content(Int32.Parse(id));
            var blog_main = root.Children.Where(x => x.ContentType.Alias == "blogMain").First();
            var blog_items = blog_main.Children.Where(x => x.ContentType.Alias == "blogItem");
            var res = blog_items.Where(x =>
                ((string)x.GetProperty("BlogItemTitle").GetValue().ToString().ToLower()).Contains(model.SearchString) ||
                ((string)x.GetProperty("BlogItemContent").GetValue().ToString().ToLower()).Contains(model.SearchString)
                ).ToList();

            if(res == null)
                //Response.Redirect("/fail");
                return RedirectToUmbracoPage(failPageId);

            ViewBag.Items = res.Count() >= 0 ? res : null;

            return View("BlogMain", CurrentPage);
        }
    }
}
