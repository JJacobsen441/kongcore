using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

/// <summary>
/// Summary description for HomePage
/// </summary>
namespace kongcore.dk.Core.Controllers
{
    public class SubmitSuccessController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;

        public SubmitSuccessController()
        {
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult SubmitSuccess(ContentModel model)
        {
            try
            {
                //throw new Exception();
                // Create AMP specific content here...

                //throw new Exception();

                DTO_HomePage dto = new DTO_HomePage(CurrentPage);

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();


                ViewBag.title = "Mere End Bare Kodeaber";
                ViewBag.page = "success";
                ViewBag.bodytext = helper.GetValue(current, "bodyText");

                DTO_Master master = new DTO_Master(CurrentPage);
                master.ToDTO(ViewData, helper);
                ViewBag.master = master;

                return CurrentTemplate(CurrentPage);
            }
            catch (Exception _e)
            {
                return new HttpNotFoundResult("some error");
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