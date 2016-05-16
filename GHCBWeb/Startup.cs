using GHCBWeb.Data;
using GHCBWeb.Infrastructure;
using GHCBWeb.Providers;
using GHCBWeb.Resolver;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace GHCBWeb
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            //ConfigureOAuthTokenConsumption(app);

            //ConfigureWebApi(httpConfig);
            WebApiConfig.Register(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

        }

      

        //private void ConfigureWebApi(HttpConfiguration config)
        //{

        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //      name: "DefaultApi",
        //      routeTemplate: "api/{controller}/{id}",
        //      defaults: new { id = RouteParameter.Optional }
        //  );

        //    // Web API 配置和服务
        //    var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        //    jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        //    jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();



        //    //Web API configuration and services
        //    var container = new UnityContainer();
        //    container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());
        //    container.RegisterType<IGHCBRepository, GHCBRepository>(new HierarchicalLifetimeManager());
           
        //    config.DependencyResolver = new UnityResolver(container);
        //}

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = "http://hhnext.com";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://hhnext.com")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }
    }
}