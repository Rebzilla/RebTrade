using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using Common.CustomExceptions;
using TradersMarketplace.Models;

namespace TradersMarketplace.Controllers
{
    public class AddRoleController : MenusController
    {
        //
        // GET: /AddRole/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RoleModel data)
        {
            try
            { 
                new RolesBL().AddRole(data.RoleName);
                return RedirectToAction("Index", "ViewRoles");
            }
            catch(RoleNameAlreadyExistsException e)
            {
                ViewBag.Message = e.Message.ToString();
                return View();
            }
        }

    }
}
