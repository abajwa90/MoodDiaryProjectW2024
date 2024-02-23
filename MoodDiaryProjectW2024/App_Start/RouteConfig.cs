using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MoodDiaryProjectW2024
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
                name: "DiaryDetails",
                url: "Diary/Details/{id}",
                defaults: new { controller = "Diary", action = "Details", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
                //name: "CreateDiary",
                //url: "Diary/Create",
                //defaults: new { controller = "Diary", action = "Create", id = UrlParameter.Optional }
            //);
        }
    }
}
