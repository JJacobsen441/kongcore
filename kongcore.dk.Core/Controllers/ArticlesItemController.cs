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
    public class ArticlesItemController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;
        
        public ArticlesItemController() : base()
        {
            //root = new Root(CurrentPage);
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult ArticlesItem(ContentModel model)
        {
            try
            {
                // Create AMP specific content here...

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                DTO_ArticleItem dto = new DTO_ArticleItem(CurrentPage);

                dto.articleTitle = helper.GetValue(current, "articleTitle");
                dto.articleLink = helper.GetValue(current, "articleLink");
                dto.articleContent = helper.GetValue(current, "articleContent").FormatParagraph();

                dto.articleAboutHeader = helper.GetValue(current, "articleAboutHeader");
                dto.articleAboutText = helper.GetValue(current, "articleAboutText").FormatParagraph();
                dto.articleTaskHeader = helper.GetValue(current, "articleTaskHeader");
                dto.articleTaskText = helper.GetValue(current, "articleTaskText").FormatParagraph();

                IPublishedContent block1Node = helper.NodeType(root, "block1");
                dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
                dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
                dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

                IPublishedContent block2Node = helper.NodeType(root, "block2");
                dto.block2header = helper.GetPropertyValue(block2Node, "block2Header");
                dto.block2text = helper.GetPropertyValue(block2Node, "block2Text").FormatParagraph();
                dto.block2buttontext = helper.GetPropertyValue(block2Node, "block2ButtonText");

                BIZ_ArticleItem biz_article = new BIZ_ArticleItem();
                dto.img = biz_article.GetImage(helper, "articleImageMain", "articleTitle");
                dto.img1 = biz_article.GetImage(helper, "articleImageMob1", "articleTitle");
                dto.img2 = biz_article.GetImage(helper, "articleImageMob2", "articleTitle");
                dto.img3 = biz_article.GetImage(helper, "articleImageMob3", "articleTitle");

                ViewBag.title = "KongCore Case";
                ViewBag.page = "case";
                ViewBag.bodytext = "Case";

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