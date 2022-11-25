﻿using kongcore.dk.Core.Common;
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
                DTO_SimpleContentPage dto = new DTO_SimpleContentPage(CurrentPage);

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                dto.bodyHeader = helper.GetValue(current, "bodyHeader");
                dto.bodyText = helper.GetValue(current, "bodyText").RichStrip();

                dto.contactEmployee1 = helper.GetValue(current, "contactEmployee1").FormatEmail();
                dto.contactEmployee2 = helper.GetValue(current, "contactEmployee2").FormatEmail();

                StaticsHelper.Visitor();

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