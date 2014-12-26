using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using Common;
using Common.Views;
using BusinessLayer;
using System.Net;
using System.Data;


namespace TradersMarketplace.Controllers
{
    public class ViewSellerProductController : MenusController
    {
        //
        // GET: /ViewSellerProduct/
        public TradersMarketPlaceEntities db = new TradersMarketPlaceEntities();

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

        //get by product id
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> categories = new List<SelectListItem>();
            IList<ProductCategory> category = new ProductsBL().GetProductCategories().ToList();
            foreach (ProductCategory pc in category)
            {
                categories.Add(new SelectListItem { Text = pc.CategoryName, Value = pc.CategoryID.ToString() });
            }
            ViewData["categoriesList"] = categories;
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ProductID,SellerUsername,ProductName,Description,Price,StockQty,CategoryID")] Product product, HttpPostedFileBase file)
        {
            string prevImageLink = new ProductsBL().GetProductByID(product.ProductID).ImageLink;
            string filename = "";
            if (file != null && file.ContentLength > 0)
            {
                string absolutePathOfImagesFolder = Server.MapPath("\\Images");
                filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                file.SaveAs(absolutePathOfImagesFolder + "\\" + filename);
                product.ImageLink = ("\\Images\\" + filename).ToString();
            }
            else
            {
                product.ImageLink = prevImageLink;
            }
            product.SellerUsername = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Username = new SelectList(db.Users, "Username", "Password", product.SellerUsername);
            return View(product);
        }


    }
}
