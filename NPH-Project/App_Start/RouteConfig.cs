using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NPH_Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "About",
            url: "about",
            defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "NPH_Project.Controllers" }
        );
            routes.MapRoute(
               name: "Product Detail",
               url: "details/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "NPH_Project.Controllers" }
           );
           

            routes.MapRoute(
               name: "Product Category",
               url: "product/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
               namespaces: new[] { "NPH_Project.Controllers" }
           );
            routes.MapRoute(
               name: "Cart",
               url: "cart",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "NPH_Project.Controllers" }
           );
            routes.MapRoute(
                    name: "Checkout",
                    url: "checkout",
                    defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                    namespaces: new[] { "NPH_Project.Controllers" }
                );
            routes.MapRoute(
             name: "Add Cart",
             url: "add-to-cart",
             defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
             namespaces: new[] { "NPH_Project.Controllers" }
         );
            routes.MapRoute(
            name: "Blog Details",
            url: "blogs/{metatitle}-{id}",
            defaults: new { controller = "Blog", action = "Detail", id = UrlParameter.Optional },
            namespaces: new[] { "NPH_Project.Controllers" }
        );
            routes.MapRoute(
             name: "Blog",
             url: "blog",
             defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "NPH_Project.Controllers" }
         );

            routes.MapRoute(
          name: "Shopping Success",
          url: "shopping-success",
          defaults: new { controller = "Cart", action = "ShoppingSuccess", id = UrlParameter.Optional },
          namespaces: new[] { "NPH_Project.Controllers" }
      );
            routes.MapRoute(
           name: "Admin",
           url: "admin",
           defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "NPH_Project.Areas.Admin.Controllers" }
       );
            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "NPH_Project.Controllers" }
          );
        }
    }
}
