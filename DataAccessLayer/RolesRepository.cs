using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccessLayer
{
    public class RolesRepository: ConnectionClass
    {
        public RolesRepository() : base() { }

        public Role GetRoleByRoleName(string roleName)
        {
            //return Entity.Roles.SingleOrDefault(r => r.RoleName == roleName);
            using(TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities())
            {
               return tm.Roles.SingleOrDefault(r => r.RoleName == roleName);
            }
        }

        public Role GetRole(int roleId)
        {
            //using (TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities()) //this doesnt work well
            //{
                //return tm.Roles.SingleOrDefault(r => r.RoleID == roleId);
            //}

            return Entity.Roles.SingleOrDefault(r => r.RoleID == roleId);
        }

        public IQueryable<Role> GetAllRoles()
        {
            using (TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities())
            {
                return tm.Roles;
            }
        }


        public IQueryable<Role> GetUserRoles(string username)
        {
            return new UsersRepository().GetUser(username).Roles.AsQueryable();
        }

        public IQueryable<User> GetUsers(int roleId)
        {
            return GetRole(roleId).Users.AsQueryable();
        }

        public void AllocateRole(User user, Role role)
        {
            user.Roles.Add(role);
            Entity.SaveChanges();
        }

        public bool IsUserInRole(string username, int roleId)
        {
            User u = new UsersRepository().GetUser(username);
            if (u.Roles.SingleOrDefault(r => r.RoleID == roleId) == null)
            {

                return false;
            }
            else 
            {
                return true;
            }
        }
    }
}
