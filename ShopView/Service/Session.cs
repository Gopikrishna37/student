using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopView.Service
{
    public class Session : ActionFilterAttribute

    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;
                if (HttpContext.Current.Session["Sellerlogin"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Seller/SellerLogin");
                    return;
            }
          /*  else if(HttpContext.Current.Session["Userlogin"] == null)
            {
                filterContext.Result = new RedirectResult("~/Users/Login");
                return;

            }*/
                base.OnActionExecuting(filterContext);
            }
        
    }
}