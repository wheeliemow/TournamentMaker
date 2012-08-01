using System.Web.Mvc;
using System.Web.Routing;
using TournamentReport.Infrastructure.Filters;
using TournamentReport.Services;

namespace TournamentReport
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ContextFilter(DependencyResolver.Current.GetService<IWebSecurityService>()));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Account", // Route name
                "Account/{action}", // URL with parameters
                new {controller = "Account"} // Parameter defaults
                );

            routes.MapRoute(
                "TournamentSpecific", // Route name
                "tournaments/{tournamentSlug}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Standings", id = UrlParameter.Optional} // Parameter defaults
                );

            routes.MapRoute(
                "TournamentAction", // Route name
                "tournaments/{tournamentSlug}/{controller}/{action}/{id}", // URL with parameters
                new {id = UrlParameter.Optional} // Parameter defaults
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}" // URL with parameters
                );

            routes.MapRoute(
                "Home", // Route name
                "", // URL with parameters
                new {controller = "Home", action = "Index"}
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}