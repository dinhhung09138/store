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
    
    public partial class stock_in_detail
    {
        public System.Guid id { get; set; }
        public System.Guid stock_in_id { get; set; }
        public System.Guid goods_id { get; set; }
        public decimal number { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
    
        public virtual good good { get; set; }
        public virtual stock_in stock_in { get; set; }
    }
}
