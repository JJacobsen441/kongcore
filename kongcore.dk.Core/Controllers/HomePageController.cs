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
        Root root;
        public HomePageController()
        {
            root = new Root(Umbraco);
        }

        // Any request for the 'ProductAmpPage' template will be handled by this Action
        public ActionResult HomePage(ContentModel model)
        {
            try
            {
                //throw new Exception();
                // Create AMP specific content here...
                

                DTO_HomePage dto = new DTO_HomePage(CurrentPage);

                IPublishedContent site = root._Root();

                dto.quote1 = "" + site.Value("quote1");
                dto.quote2 = "" + site.Value("quote2");
                dto.quote3 = "" + site.Value("quote3");

                dto.aboutTitle = "" + site.Value("aboutTitle");
                dto.aboutText = ("" + site.Value("aboutText")).FormatParagraph();

                dto.bodyText1Header = "" + site.Value("bodyText1Header");
                dto.bodyText1 = ("" + site.Value("bodyText1")).FormatParagraph();
                dto.bodyText2Header = "" + site.Value("bodyText2Header");
                dto.bodyText2 = ("" + site.Value("bodyText2")).FormatParagraph();
                dto.bodyText3Header = "" + site.Value("bodyText3Header");
                dto.bodyText3 = ("" + site.Value("bodyText3")).FormatParagraph();
                dto.bodyText4Header = "" + site.Value("bodyText4Header");
                dto.bodyText4 = ("" + site.Value("bodyText4")).FormatParagraph();

                dto.conclusionTitle = "" + site.Value("conclusionTitle");
                dto.conclusionText = ("" + site.Value("conclusionText")).FormatParagraph();

                IPublishedContent block1Node = root.ChildType("block1");
                dto.block1header = "" + block1Node.GetProperty("block1Header").GetValue();
                dto.block1text = ("" + block1Node.GetProperty("block1Text").GetValue()).FormatParagraph();
                dto.block1buttontext = "" + block1Node.GetProperty("block1ButtonText").GetValue();
            
                IPublishedContent block3Node = root.ChildType("block3");
                dto.block3header = "" + block3Node.GetProperty("block3Header").GetValue();
                dto.block3text = ("" + block3Node.GetProperty("block3Text").GetValue()).FormatParagraph();
                dto.block3buttontext = "" + block3Node.GetProperty("block3ButtonText").GetValue();
            



                List<Site> list = new List<Site>();

                IPublishedContent article = root.ChildType("articles");
                List<IPublishedContent> articles = root.ChildsType(article, "articlesItem").OrderByDescending(x => x.CreateDate).ToList();
            
                foreach (var item in articles)
                {
                    var v1 = item.GetProperty("articleImageBW")?.GetValue();
                    var v3 = ((List<IPublishedContent>)v1)?.FirstOrDefault();
                    if (!v3.IsNull())
                    {
                        IPublishedContent mediaItem = Umbraco.Media(v3.Id);
                        if (!mediaItem.IsNull())
                        {
                            string link = "" + item.GetProperty("articleLink").GetValue();
                            string url = mediaItem.Url();
                            string alt = @item.Name;

                            list.Add(new Site() { link = link, url = url, alt = alt });
                        }
                    }
                }
            
                dto.sites = list;
            
                return CurrentTemplate(dto);
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");

                var fail = root.ChildName("Fail");;
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