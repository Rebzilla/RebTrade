using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;

namespace TradersMarketplace.Controllers
{
    public class MenusController : Controller
    {
        //
        // GET: /Menus/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.Menus = new MenusBL().GetMainMenusByUsername(HttpContext.User.Identity.Name);
            }
            else
            {
                ViewBag.Menus = new MenusBL().GetMainMenusByID(1); //guest
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
