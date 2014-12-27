using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;

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

        public IQueryable<User> GetAllUsers()
        {
            return Entity.Users;
        }

        public IQueryable<UsersView> GetUsers()
        {
            return (
                from u in Entity.Users
                join r in Entity.Roles
                on u.RoleID equals r.RoleID

                select new UsersView
                {
                    Username = u.Username,
                    Password = u.Password,
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email,
                    Residence = u.Residence,
                    Street = u.Street,
                    Town = u.Town,
                    Country = u.Country,
                    RoleName = r.RoleName
                });
        }

        public void DeleteUser(User u)
        {
            User user = Entity.Users.Find(u.Username);
            Entity.Users.Remove(user);
            Entity.SaveChanges();
        }

    }
}
