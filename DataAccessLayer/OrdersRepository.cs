using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;

namespace DataAccessLayer
{
    public class OrdersRepository: ConnectionClass
    {
        public OrdersRepository() : base() { }

        public void AddOrder(Order o)
        {
            using (TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities())
            {
                tm.Orders.Add(o);
                tm.SaveChanges();
            }
        }

        public void AddOrderDetails(OrderDetail od)
        {
            using (TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities())
            {
                tm.OrderDetails.Add(od);
                tm.SaveChanges();
            }
        }

        public IQueryable<OrdersView> GetOrdersForSeller(string seller)
        {
            return (
                from p in Entity.Products
                join od in Entity.OrderDetails
                on p.ProductID equals od.ProductID
                join o in Entity.Orders
                on od.OrderID equals o.OrderID
                join os in Entity.OrderStatus
                on o.OrderStatusID equals os.StatusID

                where p.SellerUsername == seller

                select new OrdersView
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    Buyer = o.Username,
                    OrderStatusID = o.OrderStatusID,
                    OrderStatus = os.Status
                }).Distinct();
        }

        public IQueryable<OrdersView> GetOrderDetails(Guid orderID, string seller)
        {
            return (
                from p in Entity.Products
                join od in Entity.OrderDetails
                on p.ProductID equals od.ProductID
                join o in Entity.Orders
                on od.OrderID equals o.OrderID

                where o.OrderID == orderID
                && p.SellerUsername == seller

                select new OrdersView
                {
                    OrderID = orderID,
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    Quantity = od.ProductQty,
                    ImageLink = p.ImageLink,
                    Price = Math.Round(p.Price * od.ProductQty, 2),
                });
        }

        public IEnumerable<OrderStatu> GetOrderStatuses()
        {
            return Entity.OrderStatus;
        }

        public void UpdateOrderStatusByOrderID(Order original)
        {
            Order o = Entity.Orders.Where(x => x.OrderID == original.OrderID).SingleOrDefault();
            o.OrderID = original.OrderID;
            o.OrderStatusID = original.OrderStatusID;
            o.Username = original.Username;
            o.OrderDate = original.OrderDate;
            Entity.SaveChanges();
        }

        public Order GetOrderByID(Guid orderID)
        {
            return Entity.Orders.Where(o => o.OrderID == orderID).SingleOrDefault();
        }

    }
}
