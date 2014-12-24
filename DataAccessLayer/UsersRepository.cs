using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccessLayer
{
    public class UsersRepository: ConnectionClass
    {
        public UsersRepository(): base() { }

        //register
        public void AddUser(User myNewUser)
        {
            using (TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities())
            {
                tm.Users.Add(myNewUser);
                tm.SaveChanges();
            }
        }

        public bool DoesUserNameExist(string username)
        {
            if (Entity.Users.Count(u => u.Username.ToLower() == username.ToLower()) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DoesEmailExist(string email)
        {
            if (Entity.Users.Count(u => u.Email == email) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public User GetUser(string username)
        {
            return Entity.Users.SingleOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        //login
        public bool AuthenticateUser(string username, string password)
        {

            User u = GetUser(username.ToLower());
            if (u != null)
            {
                if ((u.Username.ToLower() == username.ToLower()) && (u.Password == password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



    }
}
