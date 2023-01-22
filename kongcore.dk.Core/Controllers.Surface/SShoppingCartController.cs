using kongcore.dk.Core._Common;
using kongcore.dk.Core._Statics;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers.Surface
{
    public class SShoppingCartController : SurfaceController
    {
        SessionSingleton session;
        ContentHelper helper;

        public SShoppingCartController()
        {
            session = SessionSingleton.Current;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Remove(string token)
        {
            try
            {
                if (token.IsNullOrEmpty())
                    return Json(new { success = false });

                ShoppingCart sc = new ShoppingCart();
                sc.RemoveItemByToken(token);
                
                return Json(new { success = true });
            }
            catch (Exception _e)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Add(string data)
        {
            try
            {
                if (data.IsNullOrEmpty())
                    return Json(new { success = false, res = "Beklager, der skete en fejl!" });

                helper = new ContentHelper(Umbraco);
                IPublishedContent root = helper.Root();
                //IPublishedContent current = helper.RootCurrent();

                int id;
                bool ok = int.TryParse(data, out id);
                if (!ok)
                    throw new Exception();

                ShoppingCart sc = new ShoppingCart();
                if (sc.AddItemByID(helper, id))
                    return Json(new { success = true });

                return Json(new { success = false });
            }
            catch (Exception e)
            {
                return Json(new { success = false, res = "Beklager, der skete en fejl!" });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangeNumbers(string token, string dir)
        {
            try
            {
                if (string.IsNullOrEmpty(dir))
                    throw new Exception();

                if (!(dir == "up" || dir == "down"))
                    throw new Exception();

                ShoppingCart sc = new ShoppingCart();
                sc.ChangeNumbers(token, dir);

                return Redirect("/shoppingcart");
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");

                if (helper.IsNull())
                    helper = new ContentHelper(Umbraco, CurrentPage);

                TempData["MSG"] = _e.Message + " : " + _e.StackTrace;

                var fail = helper.NodeName(helper.Root(), "Fail"); ;
                int failPageId = fail.Id;

                var redirectPage = Umbraco.Content(failPageId); //page id here

                return Redirect(redirectPage.Url());
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateAddress(string name, string phone, string email, string street, string area, string town, string country)
        {
            try
            {
                bool ok1, ok2, ok3, ok4, ok5, ok6, ok7;
                
                CheckHelper.IsValidPhonenumber(phone, out ok1);
                CheckHelper.IsValidEmail(email, out ok2);
                CheckHelper.IsValidAddress(street, out ok3);
                CheckHelper.IsValidAddress(area, out ok4);
                CheckHelper.IsValidAddress(town, out ok5);
                CheckHelper.IsValidAddress(country, out ok6);
                CheckHelper.IsValidFullName(name, out ok7);
                

                if (ok1 && ok2 && ok3 && ok4 && ok5 && ok6 && ok7)
                {
                    session.OrderFullName = name;
                    session.OrderPhone = phone;
                    session.OrderEmail = email;
                    session.OrderStreet = street;
                    session.OrderArea = area;
                    session.OrderTown = town;
                    session.OrderCountry = country;
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (Exception e)
            {
                return Json(new { success = false, res = "Beklager, der skete en fejl!" });
            }
        }
    }
}
