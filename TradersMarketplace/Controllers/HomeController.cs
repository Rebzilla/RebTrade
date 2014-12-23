using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradersMarketplace.Controllers
{
    public class HomeController : MenusController //inherits from MenusController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Trader's Marketplace";
            //HomeModel hm = new HomeModel();
            //return View(hm);
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
