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
    
    public partial class customer
    {
        public System.Guid id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
        public Nullable<System.Guid> location_id { get; set; }
        public string maps { get; set; }
        public string taxcode { get; set; }
        public bool is_company { get; set; }
        public bool gender { get; set; }
        public Nullable<System.Guid> group_id { get; set; }
        public string notes { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
    
        public virtual customer_group customer_group { get; set; }
        public virtual location location { get; set; }
    }
}
