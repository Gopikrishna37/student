﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopView
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
              );

            routes.MapRoute(
                name: "Users",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Users", action = "Login" }
               );

            routes.MapRoute(
                name: "Seller",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Seller", action = "Index" }
               );
        }
    }
}
