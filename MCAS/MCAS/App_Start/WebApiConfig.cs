using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MCAS
{
    public static class WebApiConfig
    {
        public static string _WebApiExecutionPath = "api";
        public static void Register(HttpConfiguration config)
        {
            var basicRouteTemplate = string.Format("{0}/{1}", _WebApiExecutionPath, "{controller}");

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ApiControllerAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ControllerAndId",
                routeTemplate: string.Format("{0}/{1}", basicRouteTemplate, "{id}"),
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers 
            );
        }
    }
}
