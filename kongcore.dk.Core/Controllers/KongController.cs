using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.BIZ;
using kongcore.dk.Core.Models.DB;
using kongcore.dk.Core.Models.DTOs;
using kongcore.dk.Core.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers
{
    public class KongController : SurfaceController
    {
        ContentHelper helper;
        
        [HttpPost]
        public void Submit(ContactFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception();

                bool ok = CheckHelper.CheckContactForm(model);
                if (!ok) 
                    throw new Exception();

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                string message =
                    model.name + "<br />" +
                    model.email + "<br />" +
                    model.message;

                NotificationHelper.Run(model.email, "info@kongcore.dk", "info@kongcore.dk", model.subject, Extensions.StringWithBreaksFor(message));

                ViewBag.title = "Success";
                ViewBag.page = "submitsuccess";
                ViewBag.bodytext = "Besked sendt";

                BIZ_Master biz = new BIZ_Master();
                DTO_Master master = new DTO_Master(CurrentPage);
                master = biz.ToDTO(ViewData, helper);
                ViewBag.master = master;

                Response.Redirect("/success");
            }
            catch (Exception _e)
            {
                if (helper.IsNull())
                    helper = new ContentHelper(Umbraco, CurrentPage);


                Response.Redirect("/fail"); 
                return;
            }
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            try
            {
                RunTest();
                RunTest2();

                if (!ModelState.IsValid)
                    throw new Exception();
                
                bool ok = CheckHelper.CheckSearch(model);
                if (!ok)
                    throw new Exception();

                helper = new ContentHelper(Umbraco, CurrentPage);
                IPublishedContent root = helper._Root();
                IPublishedContent current = helper._CurrentRoot();

                BIZ_BlogMain biz_blog = new BIZ_BlogMain();
                DTO_BlogMain dto = biz_blog.ToDTO(helper, model);

                ViewBag.title = "Kodegorillaen Blogger";
                ViewBag.page = "blogmain";
                ViewBag.bodytext = helper.GetValue(current, "blogTitle");

                BIZ_Master biz_master = new BIZ_Master();
                DTO_Master master = new DTO_Master(CurrentPage);
                master = biz_master.ToDTO(ViewData, helper);
                ViewBag.master = master;

                return View("BlogMain", (DTO_BlogMain)dto);
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

        private void RunTest()
        {
            if (true)
                return;
            /*
             * remember to publish -> _root["test"].Children
             * */

            var _root = Umbraco.ContentAtRoot().First();
            var _test = _root.Children.Where(x => x.ContentType.Alias == "test").First();
            int testId = _test.Id;

            var contentService = Services.ContentService;
            var newNode = contentService.Create("dette er testen", testId, "test");
            newNode.SetValue("t_name", "Jokke");
            contentService.Save(newNode);
                        
            var tests = _root.Children.Where(x => x.ContentType.Alias == "test").ToList();
            var testsub = _root.Children.Where(x => x.ContentType.Alias == "dette er testen").ToList();
            ;
        }

        private void RunTest2() 
        {
            if (true)
                return;

            using (DBContext db = new DBContext())
            {
                db.myuser.Add(new MyUser() { Name = "Jokke", Email = "admin@kongcore.dk" });
                db.SaveChanges();

                List<MyUser> _u = db.myuser.ToList();
                ;
            }
        }
    }
}
