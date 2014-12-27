using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccessLayer
{
    public class MenusRepository: ConnectionClass
    {
        public MenusRepository() : base() { }

        public IQueryable<Menu> GetMainMenus(int roleId)
        {
            Role r = new RolesRepository().GetRole(roleId);
            return r.Menus.AsQueryable().OrderBy(m => m.Position);
        }

        public IQueryable<Menu> GetMainMenusByUsername(string username)
        {
            //var list = (
            //        from u in Entity.Users
            //        from r in u.Roles
            //        from m in r.Menus
            //        where u.Username == username
            //        orderby m.Position
            //        select m
            //    ).Distinct();
            //return list.AsQueryable();
            Role r = new RolesRepository().GetUserRoles(username).First();
            return (
                    from m in r.Menus
                    orderby m.Position
                    select m
                ).AsQueryable();
        }

    }
}
