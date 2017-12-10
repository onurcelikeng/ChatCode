using ChatCode.DAL.Entities.Tables;
using ChatCode.Portal.BL.AccountBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCode.Portal.Controllers
{
    public class AccountController : Controller
    {
       /// <summary>
       /// Login Page
       /// </summary>
       /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Developer model)
        {
            if (Account.isValid(model))
            {
                if (!Account.isNotExist(model))
                {
                    var result = Account.getDeveloperCredential(model);
                    if(model.Email == result.Email && model.Password == result.Password)
                    {
                        Session["id"] = result.Id;
                        Session["email"] = result.Email;
                        Session["username"] = result.Name;
                        return RedirectToAction("Index", "Developer");
                    }
                    ViewBag.wronguser = 1;
                    return View();
                }
                ViewBag.invaliduser = 1;
                return View();
            }
            else
            {
                return View();
            }
            
        }

        /// <summary>
        /// Register Page
        /// </summary>
        /// <returns></returns>
        /// [HttpPost]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Developer model)
        {
            if (Account.isValid(model))
            {
                if (Account.isNotExist(model))
                {
                    if (Account.RegisterDeveloper(model))
                    {
                        return RedirectToAction("Login","Account");
                    }
                    ViewBag.registererror = 1;
                    return View();
                }
                ViewBag.invaliduser = 1;
                return View();
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session["id"] = null;
            return RedirectToAction("Login","Account");
        }
    }
}