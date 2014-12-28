using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using Common.Views;

namespace TradersMarketplace.Models
{
    public class ProductsModel
    {
        public List<CartView> ProductsList { get; set; }
        public Guid OrderID { get; set; }

        public ProductsModel() { }

        public ProductsModel(string username)
        {
            ProductsList = (List<CartView>)new ProductsBL().GetProductsInShoppingCart(username).ToList();
        }
    }
}