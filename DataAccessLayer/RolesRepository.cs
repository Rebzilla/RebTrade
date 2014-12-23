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

        public Role GetRole(int roleId)
        {
            return Entity.Roles.SingleOrDefault(r => r.RoleID == roleId);
        }

        public IQueryable<Role> GetAllRoles()
        {
            return Entity.Roles;
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

        public void DeallocateRole(User u, Role r)
        {
            u.Roles.Remove(r);
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
}
