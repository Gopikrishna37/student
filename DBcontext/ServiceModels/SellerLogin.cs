using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBcontext.ServiceModels
{
    public class SellerLogin
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public Nullable<bool> IsVerified { get; set; }
    }
}