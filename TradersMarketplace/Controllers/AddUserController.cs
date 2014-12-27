using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using BusinessLayer;


namespace TradersMarketplace.Controllers
{
    public class AddUserController : Controller
    {
        //
        // GET: /AddUser/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel data)
        {
            if (ModelState.IsValid)
            {
                UsersBL ubl = new UsersBL();
                if ((ubl.DoesUserNameExist(data.Username)) == true)
                {
                    ViewBag.Message += "\n Username already exists";
                    return View();
                }
                if ((ubl.DoesEmailExist(data.Email)) == true)
                {
                    ViewBag.Message += "\n E-mail address already exists";
                    return View();
                }
                else
                {
                    ViewBag.Message = "";
                    ubl.AddUser(data.Role, data.Username, data.Password, data.Name, data.Surname, data.Email, data.Residence,
                        data.Street, data.Town, data.Country);

                    return RedirectToAction("Index", "Users");
                }
            }
            return View();
        }

    }
}
