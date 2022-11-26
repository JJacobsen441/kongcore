using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.BIZ;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

/// <summary>
/// Summary description for HomePage
/// </summary>
namespace kongcore.dk.Core.Controllers
{
    public class SimpleContentPageController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;

        public SimpleContentPageController() : base()
        {
            //root = new Root(CurrentPage);
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult SimpleContentPage(ContentModel model)
        {
            try
            {
                // Create AMP specific content here...

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                DTO_SimpleContentPage dto = new DTO_SimpleContentPage(CurrentPage);

                dto.bodyHeader = helper.GetValue(current, "bodyHeader");
                dto.bodyText = helper.GetValue(current, "bodyText").RichStrip();

                dto.contactEmployee1 = helper.GetValue(current, "contactEmployee1").FormatEmailAdvanced();
                dto.contactEmployee2 = helper.GetValue(current, "contactEmployee2").FormatEmailAdvanced();

                ViewBag.title = "Kontakt En Kodegorilla";
                ViewBag.page = "contact";
                ViewBag.bodytext = helper.GetValue(current, "pageTitle");

                BIZ_Master biz = new BIZ_Master();
                DTO_Master master = new DTO_Master(CurrentPage);
                master = biz.ToDTO(ViewData, helper);
                ViewBag.master = master;

                StaticsHelper.Visitor();

                return CurrentTemplate(dto);
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");

                if (helper.IsNull())
                    helper = new ContentHelper(Umbraco, CurrentPage);

                ViewBag.title = "Fail";
                ViewBag.page = "submitfail";
                ViewBag.bodytext = "Ups";

                BIZ_Master biz = new BIZ_Master();
                DTO_Master master = new DTO_Master(CurrentPage);
                master = biz.ToDTO(ViewData, helper);
                ViewBag.master = master;


                var fail = helper.NodeName(helper._Root(), "Fail"); ;
                int failPageId = fail.Id;

                var redirectPage = Umbraco.Content(failPageId); //page id here

                return Redirect(redirectPage.Url());
            }
        }
        // All other request, eg the ProductPage template will be handled by the default 'Index' action
        //public override ActionResult Index(ContentModel model)
        //{
        //    // you are in control here!

        //    // return a 'model' to the selected template/view for this page.
        //    return CurrentTemplate(model);
        //}
    }

}