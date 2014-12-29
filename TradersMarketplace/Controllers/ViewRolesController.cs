using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using TradersMarketplace.Models;
using System.Data;


namespace TradersMarketplace.Controllers
{
    public class ViewRolesController : MenusController
    {
        //
        // GET: /ViewRoles/

        TradersMarketPlaceEntities db = new TradersMarketPlaceEntities();

        public ActionResult Index()
        {
            List<Role> roles = (List<Role>)new RolesBL().GetAllRoles().ToList();
            return View("Index", roles);
        }


        public ActionResult DeleteRole(int roleID)
        {
            new RolesBL().DeleteRole(roleID);
            List<Role> roles = (List<Role>)new RolesBL().GetAllRoles().ToList();
            return View("Index", roles);
        }

        public ActionResult EditRole(int roleID)
        {
            Role r = db.Roles.Find(roleID);
            if (r == null)
            {
                return HttpNotFound();
            }
            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole([Bind(Include = "RoleID,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }
        

    }
}
