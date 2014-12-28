using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using Common.Views;

namespace TradersMarketplace.Controllers
{
    public class OrdersController : MenusController
    {
        //
        // GET: /Orders/

        public ActionResult Index()
        {
            List<OrdersView> orders = (List<OrdersView>)new OrdersBL().GetOrdersForSeller(HttpContext.User.Identity.Name).ToList();

            List<SelectListItem> orderStatuses = new List<SelectListItem>();
            IList<OrderStatu> os = new OrdersBL().GetOrderStatuses().ToList();
            foreach (OrderStatu o in os)
            { 
                orderStatuses.Add(new SelectListItem { Text = o.Status, Value = o.StatusID.ToString() });
            }
            ViewData["statusList"] = orderStatuses;

            return View("Index", orders);
        }

        public ActionResult Details(Guid id)
        {
            List<OrdersView> orderDetails = (List<OrdersView>)new OrdersBL().GetOrderDetails(id, HttpContext.User.Identity.Name).ToList();
            decimal totalPrice = 0;

            foreach (OrdersView o in orderDetails)
            {
                totalPrice += (o.Price);
            }

            ViewBag.TotalPrice = "Total Price: €" + totalPrice;
            ViewBag.OrderID = id;
            return View(orderDetails);
        }

        public ActionResult EditOrder()
        {
            //update the status
            return View();
        }

        //Delete Method here
        public ActionResult DeleteOrder()
        {
            //set status to complete
            return View();
        }

    }
}
