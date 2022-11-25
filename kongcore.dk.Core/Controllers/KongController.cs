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

                string message =
                    model.name + "<br />" +
                    model.email + "<br />" +
                    model.message;

                NotificationHelper.Run(model.email, "info@kongcore.dk", "info@kongcore.dk", model.subject, Extensions.StringWithBreaksFor(message));

                Response.Redirect("/success");
            }
            catch (Exception _e)
            {
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

                List<Item> list = new List<Item>();
                BIZ_BlogMain biz = new BIZ_BlogMain();
                List<IPublishedContent> _res = biz.GetBlogs(helper, model);
                list = helper.GetItems(_res.ToList(), "imagePicker", "blogItemTitle", "blogItemContent", null);
                                
                DTO_BlogMain dto = new DTO_BlogMain(CurrentPage, list);

                dto.blogTitle = helper.GetValue(current, "blogTitle");
                dto.blogBodyText = helper.GetValue(current, "blogBodyText").FormatParagraph();

                IPublishedContent block1Node = helper.NodeType(root, "block1");
                dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
                dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
                dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

                IPublishedContent block2Node = helper.NodeType(root, "block2");
                dto.block2header = helper.GetPropertyValue(block2Node, "block2Header");
                dto.block2text = helper.GetPropertyValue(block2Node, "block2Text").FormatParagraph();
                dto.block2buttontext = helper.GetPropertyValue(block2Node, "block2ButtonText");

                return View("BlogMain", (DTO_BlogMain)dto);
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");
                if(helper.IsNull())
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
