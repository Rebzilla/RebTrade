using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Data.Common;
using Common;

namespace DataAccessLayer
{
    public class ConnectionClass
    {
        public TradersMarketPlaceEntities Entity { get; set; }

        public DbTransaction Transaction { get; set; }

        public ConnectionClass()
        {
            Entity = new TradersMarketPlaceEntities();
        }
    }
}
