using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.BIZ;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

/// <summary>
/// Summary description for HomePage
/// </summary>
namespace kongcore.dk.Core.Controllers
{
    public class HomePageController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;
        
        public HomePageController()
        {
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult HomePage(ContentModel model)
        {
            try
            {
                // Create AMP specific content here...

                //throw new Exception();

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                BIZ_HomePage biz_home = new BIZ_HomePage();
                DTO_HomePage dto = biz_home.ToDTO(helper);

                ViewBag.title = "Mere End Bare Kodeaber";
                ViewBag.page = "homepage";
                ViewBag.bodytext = helper.GetValue(current, "bodyText");

                BIZ_Master biz = new BIZ_Master();
                DTO_Master master = new DTO_Master(CurrentPage);
                master = biz.ToDTO(ViewData, helper);
                ViewBag.master = master;

                return CurrentTemplate(dto);
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");

                if (helper.IsNull())
                    helper = new ContentHelper(Umbraco, CurrentPage);

                var fail = helper.NodeName(helper._Root(), "Fail");;
                int failPageId = fail.Id;

                var redirectPage = Umbraco.Content(failPageId); //page id here

                return Redirect(redirectPage.Url());
            }
        }        

        // All other request, eg the ProductPage template will be handled by the default 'Index' action
        /*public override ActionResult Index(ContentModel model)
        {
            // you are in control here!

            // return a 'model' to the selected template/view for this page.
            return CurrentTemplate(model);
        }*/
    }
}