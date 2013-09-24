using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.WebMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            // BackOfficeWSD.BackOfficeWcfClient client = new BackOfficeWSD.BackOfficeWcfClient();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            AreaRegistration.RegisterAllAreas();
            //routes.MapRoute(
            //    "Default", // Route name
            //    "{codeLang}", // URL with parameters
            //    new { controller = "Home", action = "Index", codeLang = "vi" } // Parameter defaults
            //);
            routes.MapRoute(
                "Default", // Route name
               "{codeLang}/{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", codeLang = "vi", id = UrlParameter.Optional } // Parameter defaults
            );


        }


    }
}