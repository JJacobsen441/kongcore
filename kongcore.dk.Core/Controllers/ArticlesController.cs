﻿using kongcore.dk.Core.Common;
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
                DTO_ArticlesMain dto = new DTO_ArticlesMain(CurrentPage);

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                dto.articlesTitle = helper.GetValue(current, "articlesTitle");
                dto.articlesBodyText = helper.GetValue(current, "articlesBodyText").FormatParagraph();

                IPublishedContent block1Node = helper.NodeType(root, "block1");
                dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
                dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
                dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

                IPublishedContent block3Node = helper.NodeType(root, "block2");
                dto.block2header = helper.GetPropertyValue(block3Node, "block2Header");
                dto.block2text = helper.GetPropertyValue(block3Node, "block2Text").FormatParagraph();
                dto.block2buttontext = helper.GetPropertyValue(block3Node, "block2ButtonText");

                var articles = helper.NodesType(current, "articlesItem").OrderByDescending(x => x.CreateDate);
                dto.articles = helper.GetItems(articles.ToList(), "articleImageMain", "articleTitle", "articleContent", "articleLink");

                return CurrentTemplate(dto);
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
        //    return CurrentTemplate(model);
        //}
    }

}