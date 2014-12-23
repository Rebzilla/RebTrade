using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer;
using Common;
using TradersMarketplace.Models;

namespace TradersMarketplace.Controllers
{
    public class LoginController : MenusController //MenusController
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel data)
        {
            try
            {
                if(new UsersBL().AuthenticateUser(data.Username, data.Password))
                {
                    FormsAuthentication.RedirectFromLoginPage(data.Username, true);
                    return RedirectToAction("Index", "Home");
                }
                else //does not exist
                {
                    ViewBag.Message = "Incorrect Login Details";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Incorrect Login Details");
                return View(data);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
