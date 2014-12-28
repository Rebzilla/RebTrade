using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using BusinessLayer;
using Common;
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

        [Authorize]
        public string AddToCart(int pId, int quantity)
        {
            string value = new ProductsBL().AddToCart(HttpContext.User.Identity.Name, pId, quantity);
            return value;
        }

        [Authorize]
        public ActionResult ViewCart()
        {
            ProductsModel pm = new ProductsModel(HttpContext.User.Identity.Name);
            List<CartView> products = pm.ProductsList;

            decimal totalPrice = 0;

            foreach (CartView p in products)
            {
                totalPrice += (p.ProductPrice * p.ProductQuantity);
            }

            ViewBag.TotalPrice = totalPrice;
            return View("ViewCart", products);
        }

        [Authorize]
        public void RemoveFromCart(int pID)
        {
            new ProductsBL().RemoveFromCart(pID, HttpContext.User.Identity.Name);
        }
    }
}
