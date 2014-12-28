﻿using System;
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
                   od.OrderStatusID = 1; //Paid Status

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
                   mm.Body += "You have received 10% of the order total as a commission: <br/>";
                   mm.Body += "&#8364;" + Math.Round(commission * (totalPrice), 2);
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
    }
}
