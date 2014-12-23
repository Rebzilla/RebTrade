using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccessLayer;

namespace BusinessLayer
{
    public class MenusBL
    {
        public IQueryable<Menu> GetMainMenusByUsername(string username)
        {
            return new MenusRepository().GetMainMenusByUsername(username);
        }

        public IQueryable<Menu> GetMainMenusByID(int roleID)
        {
            return new MenusRepository().GetMainMenus(roleID);
        }

    }
}
