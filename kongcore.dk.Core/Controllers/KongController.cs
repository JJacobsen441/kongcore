using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models;
using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers
{
    public class KongController : SurfaceController
    {
        
        // GET: Kong
        [HttpPost]
        public /*ActionResult*/void Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                //return CurrentUmbracoPage();
                Response.Redirect("/");

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Name)|| string.IsNullOrEmpty(model.Subject) || string.IsNullOrEmpty(model.Message))
                //return CurrentUmbracoPage();
                Response.Redirect("/");

            if (!Statics.IsValidEmail(model.Email))
                //return CurrentUmbracoPage();
                Response.Redirect("/");

            bool ok;
            string name = StringHelper.OnlyAlphanumeric(model.Name, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok)
                //return CurrentUmbracoPage();
                Response.Redirect("/");
            string mail = StringHelper.OnlyAlphanumeric(model.Email, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok)
                //return CurrentUmbracoPage();
                Response.Redirect("/");
            string sub = StringHelper.OnlyAlphanumeric(model.Subject, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok)
                //return CurrentUmbracoPage();
                Response.Redirect("/");
            string mess = StringHelper.OnlyAlphanumeric(model.Message, true, true, "<br />", Statics.Characters.All(true), out ok);
            if (!ok)
                //return CurrentUmbracoPage();
                Response.Redirect("/");

            string message =
                name + "<br />" +
                mail + "<br />" +
                mess;


            Statics.Notification.Run(model.Email, "mail@kongcore.dk", "mail@kongcore.dk", model.Subject, Extensions.StringWithBreaksFor(message));

            /*return*/ Response.Redirect("/submit"); // RedirectToCurrentUmbracoPage();
            //int submitId = Int32.Parse((string)CurrentPage.GetProperty("submit").GetValue());
            //return RedirectToUmbracoPage(submitId);
        }
    }
}
