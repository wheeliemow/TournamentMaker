using System;
using System.Web.Mvc;
using TournamentReport.Models;

namespace TournamentReport.Infrastructure.Filters {
    public class ContextFilter : IActionFilter {

        public void OnActionExecuted(ActionExecutedContext filterContext) {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewBag.HasEditAccess = filterContext.HttpContext.User.IsInRole("Administrators");
            filterContext.Controller.ViewBag.CanEditTournament = new Func<Tournament, bool>((t) => t.Owner.Name == filterContext.HttpContext.User.Identity.Name);
        }
    }
}