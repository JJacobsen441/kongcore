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
    public class ArticlesController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;
        
        public ArticlesController() : base()
        {
            //root = new Root(CurrentPage);
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult ArticlesMain(ContentModel model)
        {
            try
            {
                // Create AMP specific content here...

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper.Root();
                IPublishedContent current = helper.RootCurrent();

                BIZ_ArticlesMain biz_articles = new BIZ_ArticlesMain();
                DTO_ArticlesMain dto = biz_articles.ToDTO(helper);

                ViewBag.title = "Kodegorillaens Cases";
                ViewBag.page = "casesmain";
                ViewBag.bodytext = helper.GetValue(current, "articlesTitle");

                BIZ_Settings biz = new BIZ_Settings();
                DTO_Settings master = new DTO_Settings(CurrentPage);
                master = biz.ToDTO(ViewData, helper);
                ViewBag.master = master;

                return CurrentTemplate(dto);
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");

                if (helper.IsNull())
                    helper = new ContentHelper(Umbraco, CurrentPage);

                var fail = helper.NodeName(helper.Root(), "Fail"); ;
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