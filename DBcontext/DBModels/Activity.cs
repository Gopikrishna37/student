//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBcontext.DBModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string Activity1 { get; set; }
        public Nullable<int> SellerID { get; set; }
        public Nullable<bool> IsSuccess { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
        public string Exception { get; set; }
        public int ID { get; set; }
    }
}