using System;
using System.Web.Mvc;
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
                filterContext.Controller.ViewBag.HasEditAccess =
                    filterContext.HttpContext.User.IsInRole("Administrators");
                filterContext.Controller.ViewBag.CanEditTournament =
                    new Func<Tournament, bool>(t => t.Owner.Name == filterContext.HttpContext.User.Identity.Name);
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