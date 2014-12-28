﻿using System;
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
