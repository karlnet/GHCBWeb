using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GHCBWeb.Filters;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Microsoft.Practices.Unity;
using GHCBWeb.Data;
using GHCBWeb.Resolver;
using GHCBWeb.Infrastructure;
using GHCBWeb.Data.Entities;

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
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;           
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //config.Filters.Add(new ValidateModelAttribute());

            //Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IGHCBRepository, GHCBRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
