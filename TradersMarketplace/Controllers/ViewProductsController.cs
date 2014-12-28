using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
        public TradersMarketPlaceEntities db = new TradersMarketPlaceEntities();

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

        public ActionResult EditDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product p = db.Products.Find(id);
            CartView cv = new ProductsBL().GetCartDetails(HttpContext.User.Identity.Name, p.ProductID);

            return View(cv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetails([Bind(Include = "ProductID,ProductName,ProductQuantity,ProductPrice")] CartView cart)
        {
            Cart c = db.Carts.Find(HttpContext.User.Identity.Name, cart.ProductID);
            c.Quantity = cart.ProductQuantity;

            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewCart");
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult ViewCart(ProductsModel data)
        {
            List<CartView> products = (List<CartView>)new ProductsBL().GetProductsInShoppingCart(HttpContext.User.Identity.Name).ToList();
            try
            {
                if (products.Count() != 0)
                {
                    data.OrderID = Guid.NewGuid();
                    new OrdersBL().PlaceOrder(HttpContext.User.Identity.Name, data.OrderID, products);
                    ViewBag.Message = "Your order has been successfully placed!";
                    return View("ViewCart", null);
                }
                else
                {
                    ViewBag.Message = "There are no products in your shopping cart!";
                    return View("ViewCart", null);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("ViewCart", null);
            }
        }
    }
}
