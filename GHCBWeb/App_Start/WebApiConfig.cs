using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GHCBWeb.Filters;

namespace GHCBWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API 路由
            config.MapHttpAttributeRoutes();

            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web API 配置和服务
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Filters.Add(new ValidateModelAttribute());

        }
    }
}
