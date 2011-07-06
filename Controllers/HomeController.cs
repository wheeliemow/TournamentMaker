using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TournamentReport.Models;


namespace TournamentReport.Controllers {
    public class HomeController : Controller {
        private TournamentContext db = new TournamentContext();

        public ActionResult Index() {
            var standings = db.Teams.ToList().OrderByDescending(t => t.Points);
            var games = db.Games.Include(g => g.Round).Include(g => g.Teams);
            return View(new StandingsViewModel {
                Games = games,
                Teams = standings
            });
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
