using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace GHCBWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
          
            //System.Data.Entity.Database.SetInitializer(    new GHCBWeb.Models.GHCBContextInitializer());

        }
    }
}
