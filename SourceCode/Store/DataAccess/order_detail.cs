//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class order_detail
    {
        public System.Guid id { get; set; }
        public System.Guid order_id { get; set; }
        public System.Guid product_id { get; set; }
        public byte number { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
    
        public virtual order order { get; set; }
        public virtual product product { get; set; }
    }
}
