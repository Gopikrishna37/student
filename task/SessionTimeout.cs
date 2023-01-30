using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["userId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Student/signin");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}