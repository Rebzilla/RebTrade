using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;
using Common.Views;
using System.Net;

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

        //[HttpPost]
        public ActionResult UpdateOrderStatus(Guid OrderID, int statusID) //pass the selected statusID
        {
            Order originalOrder = new OrdersBL().GetOrderByID(OrderID);
            new OrdersBL().UpdateOrderStatusByOrderID(originalOrder.OrderID, statusID, originalOrder.Username, originalOrder.OrderDate);

            //Populate the dropdown list
            List<SelectListItem> orderStatuses = new List<SelectListItem>();
            IList<OrderStatu> os = new OrdersBL().GetOrderStatuses().ToList();
            foreach (OrderStatu o in os)
            {
                orderStatuses.Add(new SelectListItem { Text = o.Status, Value = o.StatusID.ToString() });
            }
            ViewData["statusList"] = orderStatuses;

            //Get the orders to display
            List<OrdersView> orders = (List<OrdersView>)new OrdersBL().GetOrdersForSeller(HttpContext.User.Identity.Name).ToList();

            return View("Index", orders);
        }

        public ActionResult DeleteOrder(Guid id)
        {
            Order originalOrder = new OrdersBL().GetOrderByID(id);
            new OrdersBL().UpdateOrderStatusByOrderID(originalOrder.OrderID, 3, originalOrder.Username, originalOrder.OrderDate); //set status to complete id = 3

            //Populate the dropdown list
            List<SelectListItem> orderStatuses = new List<SelectListItem>();
            IList<OrderStatu> os = new OrdersBL().GetOrderStatuses().ToList();
            foreach (OrderStatu o in os)
            {
                orderStatuses.Add(new SelectListItem { Text = o.Status, Value = o.StatusID.ToString() });
            }
            ViewData["statusList"] = orderStatuses;

            List<OrdersView> orders = (List<OrdersView>)new OrdersBL().GetOrdersForSeller(HttpContext.User.Identity.Name).ToList();
            return View("Index", orders);
        }

        
    }
}
