﻿using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.BIZ;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

/// <summary>
/// Summary description for HomePage
/// </summary>
namespace kongcore.dk.Core.Controllers.Render
{
    public class SubmitSuccessController : Umbraco.Web.Mvc.RenderMvcController
    {
        ContentHelper helper;

        public SubmitSuccessController()
        {
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult SubmitSuccess(ContentModel model)
        {
            try
            {
                //throw new Exception();
                // Create AMP specific content here...

                //throw new Exception();


                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper.Root();
                IPublishedContent current = helper.RootCurrent();

                DTO_HomePage dto = new DTO_HomePage(CurrentPage);


                ViewBag.title = "Mere End Bare Kodeaber";
                ViewBag.page = "success";
                ViewBag.bodytext = "Yay!";// helper.GetValue(current, "bodyText");

                if (TempData["MSG"] != null)
                    ViewBag.MSG = TempData["MSG"];

                BIZ_Settings biz = new BIZ_Settings();
                DTO_Settings master = new DTO_Settings(CurrentPage);
                master = biz.ToDTO(ViewData, helper);
                ViewBag.master = master;

                return CurrentTemplate(CurrentPage);
            }
            catch (Exception _e)
            {
                return new HttpNotFoundResult("some error");
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