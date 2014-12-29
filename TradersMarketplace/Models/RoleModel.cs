using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradersMarketplace.Models
{
    public class RoleModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}