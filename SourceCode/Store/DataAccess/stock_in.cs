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
    
    public partial class stock_in
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public stock_in()
        {
            this.stock_in_detail = new HashSet<stock_in_detail>();
        }
    
        public System.Guid id { get; set; }
        public string code { get; set; }
        public System.Guid branch_id { get; set; }
        public System.Guid supplier_id { get; set; }
        public System.DateTime stock_in_date { get; set; }
        public decimal total_money { get; set; }
        public decimal discount { get; set; }
        public decimal payable { get; set; }
        public decimal dept { get; set; }
        public System.Guid empl_id { get; set; }
        public string reason { get; set; }
        public string notes { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
    
        public virtual branch branch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stock_in_detail> stock_in_detail { get; set; }
        public virtual supplier supplier { get; set; }
        public virtual user user { get; set; }
    }
}
