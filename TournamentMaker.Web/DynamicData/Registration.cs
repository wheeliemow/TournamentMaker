using System.Web.DynamicData;
using System.Web.Routing;

namespace DynamicDataEFCodeFirst
{
    public class Registration
    {
        private static readonly MetaModel defaultModel = new MetaModel();

        public static MetaModel DefaultModel
        {
            get { return defaultModel; }
        }

        public static void Register(RouteCollection routes)
        {
            // This route must come first to prevent some other route from the site to take over
            routes.Insert(0, new DynamicDataRoute("dbadmin/{table}/{action}")
                             {
                                 Constraints = new RouteValueDictionary(new {action = "List|Details|Edit|Insert"}),
                                 Model = DefaultModel
                             });

            routes.MapPageRoute(
                "dd_default",
                "{dbadmin}",
                "~/DynamicData/Default.aspx",
                true,
                new RouteValueDictionary(),
                new RouteValueDictionary {{"dbadmin", "dbadmin"}});
        }
    }
}