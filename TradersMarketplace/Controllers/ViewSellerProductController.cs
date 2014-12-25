using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Views;
using BusinessLayer;

namespace TradersMarketplace.Controllers
{
    public class ViewSellerProductController : MenusController
    {
        //
        // GET: /ViewSellerProduct/

        [Authorize]
        public ActionResult Index()
        {
            List<ProductsView> products = new ProductsBL().GetProductsForSeller(HttpContext.User.Identity.Name).ToList();
            return View("Index", products);
        }

        //delete by product id
        public void DeleteProductByID(int pID)
        {
            new ProductsBL().DeleteProduct(pID);
        }

        //edit by product id, partial view

    }
}
