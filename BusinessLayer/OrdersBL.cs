using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;
using DataAccessLayer;
using System.Net.Mail;
using System.Net;


namespace BusinessLayer
{
   public class OrdersBL
    {
       public void PlaceOrder(string username, Guid orderID, List<CartView> products)
       {
           OrdersRepository or = new OrdersRepository();
           ProductsRepository pr = new ProductsRepository();
           TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities();
           or.Entity = pr.Entity = tm;

           Order o = new Order();
           o.OrderID = orderID;
           o.Username = username;
           o.OrderDate = DateTime.Now;
           o.OrderStatusID = 1; //Paid Status

           OrderDetail od;
           decimal totalPrice = 0M;
           try
           {
               or.Entity.Database.Connection.Open();
               or.Transaction = pr.Transaction = or.Entity.Database.Connection.BeginTransaction();

               or.AddOrder(o);

               foreach (CartView p in products)
               {
                   od = new OrderDetail();
                   od.OrderID = orderID;
                   od.ProductID = p.ProductID;
                   od.ProductQty = p.ProductQuantity;
                   
                   totalPrice += (p.ProductPrice * p.ProductQuantity);

                   or.AddOrderDetails(od);
                   pr.DecreaseStock(p.ProductID, p.ProductQuantity);
               }

               foreach (Cart sc in pr.GetCartForUser(username))
               {
                   pr.RemoveShoppingCart(sc);
               }

               try
               {
                   //Send an email to the administrator to notify them about the commission
                   string adminEmail = new UsersRepository().GetAdminEmail();
                   decimal commission = 0.1M;
                   MailMessage mm = new MailMessage();
                   mm.To.Add(adminEmail);
                   mm.From = new MailAddress("fabulousfashion00@gmail.com", "Trader's Marketplace");
                   mm.Subject = "Order Commission";
                   mm.Body = "Dear Admin, <br/><br/>An order with ID " + orderID + " has been placed.<br/>";
                   mm.Body += "You have received 10% of the order total as a commission: &#8364;"+Math.Round(commission * (totalPrice), 2);
                   mm.Body += "<br/><br/>Regards, <br/>Traders MarketPlace";
                   mm.IsBodyHtml = true;

                   SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                   client.EnableSsl = true;
                   client.DeliveryMethod = SmtpDeliveryMethod.Network;
                   client.Credentials = new NetworkCredential("fabulousfashion00@gmail.com", "fabfashadmin"); 
                   client.Send(mm);

                   or.Transaction.Commit();

               }
               catch (SmtpException e)
               {
                   or.Transaction.Rollback();
               }

           }
           catch (Exception ex)
           {
               or.Transaction.Rollback();
           }
           finally
           {
               or.Entity.Database.Connection.Close();
           }
       }

       public IQueryable<OrdersView> GetOrdersForSeller(string seller)
       {
           return new OrdersRepository().GetOrdersForSeller(seller);
       }

       public IQueryable<OrdersView> GetOrderDetails(Guid orderID, string seller)
       {
           return new OrdersRepository().GetOrderDetails(orderID, seller);
       }

       public IEnumerable<OrderStatu> GetOrderStatuses()
       {
           return new OrdersRepository().GetOrderStatuses();
       }

       public void UpdateOrderStatusByOrderID(Guid orderID, int statusID, string buyer, DateTime orderDate)
       {
           Order o = new Order();
           o.OrderID = orderID;
           o.OrderStatusID = statusID;
           o.Username = buyer;
           o.OrderDate = orderDate;
           new OrdersRepository().UpdateOrderStatusByOrderID(o);
       }

       public Order GetOrderByID(Guid orderID)
       {
           return new OrdersRepository().GetOrderByID(orderID);
       }

    }
}
