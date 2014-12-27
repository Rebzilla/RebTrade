using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using Common.Views;

namespace TradersMarketplace.Controllers
{
    public class UsersController : MenusController
    {
        //
        // GET: /User/

        [Authorize]
        public ActionResult Index()
        {
           // List<User> users = new UsersBL().GetAllUsers().ToList();
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

        //edit method 1
        //edit method 2

    }
}
