using ChatCode.DAL.Entities.Tables;
using ChatCode.Portal.BL.CO;
using ChatCode.Portal.BL.DeveloperBL;
using ChatCode.Portal.BL.TemplateBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCode.Portal.Controllers
{
    public class DeveloperController : Controller
    {
        // GET: Developer
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                var result = GetTemplates.getTemplates((int)Session["id"]);
                var statistic = new Statistic();
                var Statitic = new StatisticCO {
                    TotalTemplate = statistic.GetAllTemplateNumber(),
                    AddedToday = statistic.GetAddedTodayTemplate(),
                    YourTemplate = statistic.getSessionTemplate((int)Session["id"]),
                    YourDownloadedTemplate = statistic.getSessionDownloadedTemplate((int)Session["id"])
                };

                return View(Tuple.Create<List<Website>, StatisticCO>(result, Statitic));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult New()
        {
            if (Session["id"] != null)
            {
                var sta = new Statistic();
                int total = sta.getSessionTemplate((int)Session["id"]);
                return View(total);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(Website model)
        {
            if (Session["id"] != null)
            {
                var sta = new Statistic();
                int total = sta.getSessionTemplate((int)Session["id"]);
                if (AddTemplate.Add(model, (int)Session["id"]))
                {
                    ViewBag.success = 1;
                    return View(total);
                }
                ViewBag.error = 1;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        

        public ActionResult UpdateTemplate(int id)
        {
            if(Session["id"] != null)
            {
                var result = GetTemplates.getOneTemplate(id, (int)Session["id"]);
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateTemplates(Website model)
        {
            if (Session["id"] != null)
            {
                //var result = GetTemplates.getOneTemplate(model.Id, (int)Session["id"]);
                //result.TemplateName = model.TemplateName;
                //result.SourceCode = model.SourceCode;
                //result.Price = model.Price;
                //result.Types = model.Types;
                //result.Style1 = model.Style1;
                //result.Style2 = model.Style2;
                //result.Decsription = model.Decsription;
                //result.Sex = model.Sex;
                //result.PublishedDate = DateTime.Now;

                GetTemplates.UpdateWesite(model, (int)Session["id"]);
                return RedirectToAction("Index","Developer");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}