using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.BIZ;
using kongcore.dk.Core.Models.DTOs;
using System;
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
    public class BlogMainController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;
        
        public BlogMainController() : base()
        {
            //root = new Root(CurrentPage);
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult BlogMain(ContentModel model)
        {
            try
            {
                // Create AMP specific content here...

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                BIZ_BlogMain biz_blogs = new BIZ_BlogMain();
                DTO_BlogMain dto = biz_blogs.ToDTO(helper, null);
                
                ViewBag.title = "Kodegorillaen Blogger";
                ViewBag.page = "blogmain";
                ViewBag.bodytext = helper.GetValue(current, "blogTitle");

                BIZ_Master biz_master = new BIZ_Master();
                DTO_Master master = new DTO_Master(CurrentPage);
                master = biz_master.ToDTO(ViewData, helper);
                ViewBag.master = master;

                return View("BlogMain", (DTO_BlogMain)dto);
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");

                if (helper.IsNull())
                    helper = new ContentHelper(Umbraco, CurrentPage);

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
        //    return CurrentTemplate(CurrentPage);
        //}
    }

}