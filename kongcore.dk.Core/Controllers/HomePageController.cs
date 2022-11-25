using kongcore.dk.Core.Common;
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
                //throw new Exception();
                // Create AMP specific content here...

                //throw new Exception();

                DTO_HomePage dto = new DTO_HomePage(CurrentPage);

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                dto.aboutTitle = helper.GetValue(current, "aboutTitle");
                dto.aboutText = helper.GetValue(current, "aboutText").FormatParagraph();

                dto.conclusionTitle = helper.GetValue(current, "conclusionTitle");
                dto.conclusionText = helper.GetValue(current, "conclusionText").FormatParagraph();

                dto.bodyText1Header = helper.GetValue(current, "bodyText1Header");
                dto.bodyText1 = helper.GetValue(current, "bodyText1").FormatParagraph();
                dto.bodyText2Header = helper.GetValue(current, "bodyText2Header");
                dto.bodyText2 = helper.GetValue(current, "bodyText2").FormatParagraph();
                dto.bodyText3Header = helper.GetValue(current, "bodyText3Header");
                dto.bodyText3 = helper.GetValue(current, "bodyText3").FormatParagraph();
                dto.bodyText4Header = helper.GetValue(current, "bodyText4Header");
                dto.bodyText4 = helper.GetValue(current, "bodyText4").FormatParagraph();

                dto.quote1 = helper.GetValue(current, "quote1");
                dto.quote2 = helper.GetValue(current, "quote2");
                dto.quote3 = helper.GetValue(current, "quote3");

                IPublishedContent block1Node = helper.NodeType(root, "block1");
                dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
                dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
                dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");
            
                IPublishedContent block3Node = helper.NodeType(root, "block3");
                dto.block3header = helper.GetPropertyValue(block3Node, "block3Header");
                dto.block3text = helper.GetPropertyValue(block3Node, "block3Text").FormatParagraph();
                dto.block3buttontext = helper.GetPropertyValue(block3Node, "block3ButtonText");
            
                IPublishedContent article = helper.NodeType(root, "articles");
                List<IPublishedContent> articles = helper.NodesType(article, "articlesItem").OrderByDescending(x => x.CreateDate).ToList();
                dto.sites = helper.GetItems(articles.ToList(), "articleImageBW", null, null, "articleLink");
                            
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