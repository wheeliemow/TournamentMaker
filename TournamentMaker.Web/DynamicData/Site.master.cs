using System;
using System.Net;
using System.Web.UI;
using TournamentReport.App_Start;

namespace DynamicDataEFCodeFirst
{
    public partial class Site : MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                Response.End();
            }

            if (!Request.IsLocal && !Page.User.IsInRole(Constants.AdministratorsRoleName))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Response.End();
            }

            base.OnInit(e);
        }
    }
}