﻿using BulletinBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BulletinBoard
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdvertisementsList",
                url: "Advertisements/List/",
                defaults: new { controller = "Advertisements", action = "List" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Advertisements", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}