using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBcontext.ServiceModels
{
    public class Seller
    {

        public int SellerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<System.TimeSpan> OTPSendTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}