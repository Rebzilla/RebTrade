using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using Common.Views;
using System.Data;

namespace TradersMarketplace.Controllers
{
    public class UsersController : MenusController
    {
        //
        // GET: /User/
        public TradersMarketPlaceEntities db = new TradersMarketPlaceEntities();

        [Authorize]
        public ActionResult Index()
        {
            List<UsersView> users = new UsersBL().GetUsers().ToList();
            return View("Index", users);
        }

        //delete by username
        public ActionResult DeleteUserByUsername(string username)
        {
            new UsersBL().DeleteUser(username);
            List<UsersView> users = new UsersBL().GetUsers().ToList();
            return View("Index", users);
        }

        //get by user by username
        public ActionResult EditUser(string username)
        {
            User user = db.Users.Find(username);
            if (user == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> usertype = new List<SelectListItem>();
            IList<Role> userTypes = new RolesBL().GetAllRoles().ToList();
            foreach (Role r in userTypes)
            {
                if (r.RoleID != 1)
                {
                    usertype.Add(new SelectListItem { Text = r.RoleName, Value = r.RoleID.ToString() });
                }
            }
            ViewData["usersList"] = usertype;
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "Username,Password,Name,Surname,Email,Residence,Street,Town,Country,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}
