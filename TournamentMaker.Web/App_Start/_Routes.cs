using System.Web.Mvc;
using System.Web.Routing;
using DynamicDataEFCodeFirst;

namespace TournamentReport.App_Start
{
    public static class Routes
    {
        public static void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Registration.Register(routes); // Dynamic Data

            routes.MapRoute(
                "Account", // Route name
                "Account/{action}", // URL with parameters
                new {controller = "Account"} // Parameter defaults
                );

            routes.MapRoute(
                "TournamentCreate",
                "tournaments/{tournamentSlug}/{Controller}/Create",
                new {action = "Create"});

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
    }
}