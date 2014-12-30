using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using Common.CustomExceptions;
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
            try
            {
                new RolesBL().DeleteRole(roleID);
            }
            catch (CoreRoleException e)
            {
                ViewBag.Message = e.Message.ToString();
            }
            
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
                try
                {
                    new RolesBL().UpdateRole(role.RoleID, role.RoleName);
                    return RedirectToAction("Index");
                }
                catch(CoreRoleException e)
                {
                    ViewBag.Message = e.Message.ToString();
                }
                catch(RoleNameAlreadyExistsException ex)
                {
                    ViewBag.Message = ex.Message.ToString();
                }
            }
            return View(role);
        }
    }
}
