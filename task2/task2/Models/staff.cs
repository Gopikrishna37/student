//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace task2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class staff
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> DateOfJoin { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public int ID { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
    }
}