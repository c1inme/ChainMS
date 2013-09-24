using System.Web.Mvc;

namespace CMS.WebMVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
        "ImageAdmin", // Route name
        "Admin/ImageAdmin/{action}/{Discriminator}/{IdBelong}", // URL with parameters
        new
        {
            controller = "ImageAdmin",
            action = UrlParameter.Optional,
            Discriminator = UrlParameter.Optional,
            IdBelong = UrlParameter.Optional
        }
    );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "News", action = "Index", id = UrlParameter.Optional },
                new string[] { "CMS.WebMVC.Areas.Admin.Controllers" }
            );
        }
    }
}
