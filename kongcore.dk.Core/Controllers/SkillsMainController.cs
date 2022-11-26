using kongcore.dk.Core.Common;
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
    public class SkillsMainController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;
        
        public SkillsMainController() : base()
        {
            //root = new Root(CurrentPage);
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult SkillsMain(ContentModel model)
        {
            try
            {
                // Create AMP specific content here...
                DTO_SkillsMain dto = new DTO_SkillsMain(CurrentPage);

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                dto.skillsTitle = helper.GetValue(current, "skillsTitle");
                dto.skillsBodyText = helper.GetValue(current, "skillsBodyText").FormatParagraph();
                var selection = helper.NodesType(current, "skillsItem");
                dto.skills = helper.GetItems(selection, null, "skillTitle", "skillContent", null);

                IPublishedContent block1Node = helper.NodeType(root, "block1");
                dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
                dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
                dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

                IPublishedContent block3Node = helper.NodeType(root, "block2");
                dto.block2header = helper.GetPropertyValue(block3Node, "block2Header");
                dto.block2text = helper.GetPropertyValue(block3Node, "block2Text").FormatParagraph();
                dto.block2buttontext = helper.GetPropertyValue(block3Node, "block2ButtonText");

                ViewBag.title = "Vi Er Kodegorillaer";
                ViewBag.page = "skillsmain";
                ViewBag.bodytext = helper.GetValue(current, "skillsTitle");

                DTO_Master master = new DTO_Master(CurrentPage);
                master.Setup(ViewData, helper);
                ViewBag.master = master;

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

                DTO_Master master = new DTO_Master(CurrentPage);
                master.Setup(ViewData, helper);
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