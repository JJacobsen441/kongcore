using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers
{
    public class KongController : SurfaceController
    {
        
        // GET: Kong
        [HttpPost]
        public ActionResult Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Name)|| string.IsNullOrEmpty(model.Subject) || string.IsNullOrEmpty(model.Message))
                return CurrentUmbracoPage();

            if(!Statics.IsValidEmail(model.Email))
                return CurrentUmbracoPage();

            Statics.Notification.Run(model.Email, "admin@kongcore.dk", "admin@kongcore.dk", model.Subject, model.Message);

            return RedirectToCurrentUmbracoPage();
        }
    }
}
