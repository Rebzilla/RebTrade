using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class CoreRoleException: Exception
    {
         public CoreRoleException()
            : base("The Guest, Buyer, Seller and Administrator Roles cannot be modified or deleted. Try another one")
        {
        }

        public CoreRoleException(string message)
            : base(message)
        {
        }

        public CoreRoleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
