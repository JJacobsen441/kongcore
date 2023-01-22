using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.BIZ;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace kongcore.dk.Core.Controllers.Render
{
    public class CheckOutController : Umbraco.Web.Mvc.RenderMvcController
    {
        SessionSingleton session;
        ContentHelper helper;
        
        public CheckOutController()
        {
            session = SessionSingleton.Current;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CheckOut()
        {
            try
            {
                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper.Root();
                IPublishedContent current = helper.RootCurrent();

                BIZ_CheckOut biz = new BIZ_CheckOut();
                DTO_CheckOut dto = biz.ToDTO(helper);

                //ViewBag.title = "Mere End Bare Kodeaber";
                ViewBag.page = "homepage";
                ViewBag.bodytext = helper.GetValue(current, "bodyText");

                BIZ_Settings _biz = new BIZ_Settings();
                DTO_Settings master = new DTO_Settings(CurrentPage);
                master = _biz.ToDTO(ViewData, helper);
                ViewBag.master = master;
                ViewBag.Session = session;



                return CurrentTemplate(dto);


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
    }
}
