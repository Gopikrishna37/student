using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task2.Models
{
    public class LoginDetails
    {
        public int id { get; set; }
        public Nullable<int> staff_id { get; set; }
        public Nullable<System.DateTime> login_time { get; set; }
        public Nullable<bool> is_deleted { get; set; }
        public string token { get; set; }
    }
}