using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;
using Common.Views;

namespace TradersMarketplace.Controllers
{
    public class ViewProductsController : MenusController
    {
        //
        // GET: /ViewProducts/

        [Authorize]
        public ActionResult Index()
        {
            List<ProductsView> products = new ProductsBL().GetAllProducts().ToList();
            return View(products);
        }

    }
}
