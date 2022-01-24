﻿using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers
{
    public class AdminController : UmbracoAuthorizedController
    {
        public ActionResult MyDashboard()
        {
            return PartialView("mydashboard");
        }
        public ActionResult MyPanel()
        {
            return PartialView("mypanel");
        }
    }
}