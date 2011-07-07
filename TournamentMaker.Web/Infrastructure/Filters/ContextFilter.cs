using System.Web.Mvc;

namespace TournamentReport.Infrastructure.Filters {
    public class ContextFilter : IActionFilter {

        public void OnActionExecuted(ActionExecutedContext filterContext) {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewBag.HasEditAccess = filterContext.HttpContext.Request.IsAuthenticated;
        }
    }
}