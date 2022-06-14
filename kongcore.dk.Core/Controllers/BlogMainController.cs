using System.Web.Mvc;

namespace kongcore.dk.Core.Controllers
{
    public class BlogMainController : Umbraco.Web.Mvc.RenderMvcController
    {
        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult BlogMain()
        {
            // Create AMP specific content here...
            return View("BlogMain", CurrentPage);
        }

        //public override ActionResult Index()
        //{
        //    // you are in control here!

        //    // return a 'model' to the selected template/view for this page.
        //    return CurrentTemplate(CurrentPage);
        //}
    }
}