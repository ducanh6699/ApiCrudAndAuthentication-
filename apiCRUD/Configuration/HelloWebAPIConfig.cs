using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace apiCRUD.Configuration
{
    public class HelloWebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            
            
            // school route
            config.Routes.MapHttpRoute(
                name: "Student",
                routeTemplate: "api/student/{id}/{name}/{Class}",
                defaults: new { controller = "student", id = RouteParameter.Optional, name = RouteParameter.Optional, Class = RouteParameter.Optional }
                //constraints: new { id = "/d+", name = "/d+", Class = "/d+" }
            );


            // Add default route using convention-based routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            // configure json formatter
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain"));
            
        }
    }
}