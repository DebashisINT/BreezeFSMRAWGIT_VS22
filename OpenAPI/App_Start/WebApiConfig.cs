using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OpenAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                 //routeTemplate: "api/{controller}/{action}/{id}",
                 //Rev For Multi function add in one controller start
                 routeTemplate: "api/{controller}/{action}/{id}",
                //Rev For Multi function add in one controller end
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
