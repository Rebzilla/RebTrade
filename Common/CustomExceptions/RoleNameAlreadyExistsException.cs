using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class RoleNameAlreadyExistsException: Exception
    {
        public RoleNameAlreadyExistsException()
            : base("Rolename already exists. Try another one")
        {
        }

        public RoleNameAlreadyExistsException(string message)
            : base(message)
        {
        }

        public RoleNameAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
