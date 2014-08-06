using System;
using System.Web.Mvc;
using TournamentReport.App_Start;
using TournamentReport.Models;
using TournamentReport.Services;

namespace TournamentReport.Infrastructure.Filters
{
    public class ContextFilter : IActionFilter
    {
        private readonly IWebSecurityService webSecurity;

        public ContextFilter(IWebSecurityService webSecurity)
        {
            Ensure.ArgumentNotNull(webSecurity, "webSecurity");
            this.webSecurity = webSecurity;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (webSecurity.UserExists(filterContext.HttpContext.User.Identity.Name))
            {
                bool isAdmin = filterContext.HttpContext.User.IsInRole(Constants.AdministratorsRoleName);
                filterContext.Controller.ViewBag.HasEditAccess = isAdmin;
                filterContext.Controller.ViewBag.CanEditTournament =
                    new Func<Tournament, bool>(t => isAdmin 
                        || t.Owner.Name == filterContext.HttpContext.User.Identity.Name);
            }
            else
            {
                filterContext.Controller.ViewBag.HasEditAccess = false;
                filterContext.Controller.ViewBag.CanEditTournament =
                    new Func<Tournament, bool>(t => false);
            }
        }
    }
}