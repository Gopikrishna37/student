using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBcontext.ServiceModels
{
    public class CartwithProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public Nullable<int> Price { get; set; }
        public string ImageLink { get; set; }
        public int CartID { get; set; }
        public Nullable<int> CustomerID { get; set; }
    
    }
}