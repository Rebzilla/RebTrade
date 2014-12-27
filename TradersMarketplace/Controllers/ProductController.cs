using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using TradersMarketplace.Models;

namespace TradersMarketplace.Controllers
{
    public class ProductController : MenusController
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            ProductModel pm = new ProductModel();
            return View(pm);
        }

        [HttpPost]
        public ActionResult Index(ProductModel data, HttpPostedFileBase file)
        {
            if(ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string absolutePathOfImagesFolder = Server.MapPath("\\Images");
                    string filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                    file.SaveAs(absolutePathOfImagesFolder + "\\" + filename);

                    ProductsBL pbl = new ProductsBL();
                    pbl.AddProduct(data.ProductName, HttpContext.User.Identity.Name, data.Description, data.Price, "\\Images\\" + filename, data.StockQty, data.CategoryID);
                    return RedirectToAction("Index", "ViewSellerProduct"); //redirect to view products page
                }
            }
            return View();
        }
    }
}
