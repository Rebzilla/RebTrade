using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Common;
using Common.Decorator;
using BusinessLayer;

namespace TradersMarketplace.Controllers
{
    public class HomeController : MenusController
    {
        TradersMarketPlaceEntities db = new TradersMarketPlaceEntities();

        public ActionResult Index()
        {
            ViewBag.Message = "Trader's Marketplace";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public PartialViewResult SearchName (string keyword)
        {
            List<Product> products = db.Products.ToList();
            //using the Decorator Design Pattern by implementing it in Advanced Search
            SearchByProductName searchName = new SearchByProductName();
            searchName.ProductName = keyword;
            List<Product> searchResults = searchName.Search(products);

            return PartialView("_search", searchResults); //pass products here
        }

        public PartialViewResult SearchDescription(string keyword)
        {
            List<Product> products = db.Products.ToList();
            //using the Decorator Design Pattern by implementing it in Advanced Search
            SearchByProductDescription searchDescription = new SearchByProductDescription();
            searchDescription.Description = keyword;
            List<Product> searchResults = searchDescription.Search(products);

            return PartialView("_search", searchResults); //pass products here
        }

        public PartialViewResult SearchCategory(string keyword)
        {
            List<Product> products = db.Products.ToList();
            //using the Decorator Design Pattern by implementing it in Advanced Search
            SearchByProductCategory searchCategory = new SearchByProductCategory();
            searchCategory.CategoryName = keyword;
            List<Product> searchResults = searchCategory.Search(products);

            return PartialView("_search", searchResults); //pass products here
        }
    }
}
