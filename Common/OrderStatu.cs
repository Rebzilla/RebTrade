//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderStatu
    {
        public OrderStatu()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int StatusID { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
