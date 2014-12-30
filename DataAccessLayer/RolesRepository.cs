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

        public void AddRole(Role r)
        {
            Entity.Roles.Add(r);
            Entity.SaveChanges();
        }

        public bool DoesRoleNameExist(string roleName)
        {
            if (Entity.Roles.Count(r => r.RoleName.ToLower() == roleName.ToLower()) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DeleteRole(Role r)
        {
            Role ro = Entity.Roles.Find(r.RoleID);
            Entity.Roles.Remove(ro);
            Entity.SaveChanges();
        }

        public void UpdateRole(Role or)
        {
            Role r = Entity.Roles.SingleOrDefault(x => x.RoleID == or.RoleID);
            r.RoleName = or.RoleName;
            Entity.SaveChanges();
        }

        public Role GetRoleByRoleName(string roleName)
        {
            using(TradersMarketPlaceEntities tm = new TradersMarketPlaceEntities())
            {
               return tm.Roles.SingleOrDefault(r => r.RoleName == roleName);
            }
        }

        public Role GetRole(int roleId)
        {
            return Entity.Roles.SingleOrDefault(r => r.RoleID == roleId);
        }

        public IEnumerable<Role> GetAllRoles()
        {
                return Entity.Roles;
        }


        public IQueryable<Role> GetUserRoles(string username)
        {
            User u = Entity.Users.SingleOrDefault(x => x.Username == username);
            return Entity.Roles.Where(r => r.RoleID == u.RoleID).AsQueryable();
        }

        public IQueryable<User> GetUsers(int roleId)
        {
            return Entity.Users.Where(u => u.RoleID == roleId).AsQueryable();
        }

        public bool IsUserInRole(string username, int roleId)
        {
            User u = new UsersRepository().GetUser(username);
            if (u.RoleID == roleId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
