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
    
    public partial class fanpage_post
    {
        public string post_id { get; set; }
        public string fanpage_id { get; set; }
        public System.DateTime post_date { get; set; }
        public int likes { get; set; }
        public int reaction_viewer { get; set; }
        public int reaction_total { get; set; }
        public int comment { get; set; }
        public int sharepost { get; set; }
    
        public virtual fanpage fanpage { get; set; }
    }
}
