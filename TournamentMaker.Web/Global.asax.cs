using System.Web.Mvc;
using System.Web.Routing;
using TournamentReport.App_Start;
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

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            Routes.Register(RouteTable.Routes);
        }
    }
}