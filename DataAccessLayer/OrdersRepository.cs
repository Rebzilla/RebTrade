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
    }
}
