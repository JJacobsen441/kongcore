using System.Web.Mvc;
using Umbraco.Web.Models;

/// <summary>
/// Summary description for HomePage
/// </summary>
namespace kongcore.dk.Core.Controllers
{
    public class ArticlesMainController : Umbraco.Web.Mvc.RenderMvcController
    {
        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult ArticlesMain(ContentModel model)
        {
            // Create AMP specific content here...
            return CurrentTemplate(model);
        }
        // All other request, eg the ProductPage template will be handled by the default 'Index' action
        public override ActionResult Index(ContentModel model)
        {
            // you are in control here!

            // return a 'model' to the selected template/view for this page.
            return CurrentTemplate(model);
        }
    }

}