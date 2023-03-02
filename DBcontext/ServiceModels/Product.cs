using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBcontext.ServiceModels
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public Nullable<int> Price { get; set; }
        public string ImageLink { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<bool> IsSold { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> SellerID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}