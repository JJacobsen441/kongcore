using kongcore.dk.Core.Common;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using www.kongcore.dk.Common;

/// <summary>
/// Summary description for HomePage
/// </summary>
namespace kongcore.dk.Core.Controllers
{
    public class HomePageController : Umbraco.Web.Mvc.RenderMvcController
    {
        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult HomePage(ContentModel model)
        {
            // Create AMP specific content here...
            return CurrentTemplate(model);
        }

        /*
         * these below -> does not belong here
         * */

        //public ActionResult BlogMain(ContentModel model)
        //{
        //    // Create AMP specific content here...
        //    return CurrentTemplate(model);
        //}

        //public ActionResult SkillsMain()
        //{
        //    // Create AMP specific content here...
        //    return CurrentTemplate(CurrentPage);
        //}

        // All other request, eg the ProductPage template will be handled by the default 'Index' action
        public override ActionResult Index(ContentModel model)
        {
            // you are in control here!

            // return a 'model' to the selected template/view for this page.
            return CurrentTemplate(model);
        }
    }
}