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
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.inventories = new HashSet<inventory>();
            this.order_detail = new HashSet<order_detail>();
            this.stock_in_detail = new HashSet<stock_in_detail>();
            this.stock_out_detail = new HashSet<stock_out_detail>();
            this.stock_tranfer_detail = new HashSet<stock_tranfer_detail>();
        }
    
        public System.Guid id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public System.Guid type_id { get; set; }
        public Nullable<System.Guid> unit_id { get; set; }
        public Nullable<System.Guid> group_id { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> original_price { get; set; }
        public Nullable<decimal> number_in_stock { get; set; }
        public Nullable<decimal> weight { get; set; }
        public bool allow_sale_direct { get; set; }
        public string pic01 { get; set; }
        public string pic02 { get; set; }
        public string pic03 { get; set; }
        public string pic04 { get; set; }
        public string pic05 { get; set; }
        public Nullable<decimal> min_in_stock { get; set; }
        public Nullable<decimal> max_in_stock { get; set; }
        public string description { get; set; }
        public string note_in_order { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inventory> inventories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_detail> order_detail { get; set; }
        public virtual product_group product_group { get; set; }
        public virtual product_type product_type { get; set; }
        public virtual unit unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stock_in_detail> stock_in_detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stock_out_detail> stock_out_detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stock_tranfer_detail> stock_tranfer_detail { get; set; }
    }
}
