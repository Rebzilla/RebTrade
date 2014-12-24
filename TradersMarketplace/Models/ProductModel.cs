using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BusinessLayer;
using Common;
using System.Web.Mvc;

namespace TradersMarketplace.Models
{
    public class ProductModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int StockQty { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public SelectList Categories { get; set; }
        IList<ProductCategory> categories { get; set; }

        public ProductModel()
        {
            categories = new ProductsBL().GetProductCategories().ToList();
            Categories = new SelectList(categories, "CategoryID", "CategoryName");
        }
    }
}