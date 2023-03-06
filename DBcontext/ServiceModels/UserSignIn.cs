using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBcontext.ServiceModels
{
    public class UserSignIn
    {

        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string Email { get; set; }
        public Nullable<int> Phone { get; set; }
    }
}