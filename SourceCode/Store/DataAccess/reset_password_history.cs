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
    
    public partial class reset_password_history
    {
        public System.Guid id { get; set; }
        public System.Guid user_require { get; set; }
        public System.DateTime require_date { get; set; }
        public Nullable<byte> status { get; set; }
        public string code { get; set; }
        public string token { get; set; }
        public Nullable<System.DateTime> reset_date { get; set; }
        public Nullable<System.DateTime> expried_date { get; set; }
    
        public virtual user user { get; set; }
    }
}
