using System.Web.Routing;
using System.Web.Mvc;


namespace ImageServiceWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            //Sets routes to ignore
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Sets map route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "First", action = "ImageWebModel", id = UrlParameter.Optional }
            );
        }
    }
}
