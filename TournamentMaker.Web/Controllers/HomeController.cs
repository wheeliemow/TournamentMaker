using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TournamentReport.Models;


namespace TournamentReport.Controllers {
    public class HomeController : Controller {
        private TournamentContext db = new TournamentContext();

        public ActionResult Index() {
            var tournaments = db.Tournaments.ToList();
            return View(tournaments);
        }

        public ActionResult Standings(string id) {
            var tournament = db.Tournaments.
                Include(t => t.Teams).
                Include(t => t.Rounds).
                First(t => t.Slug == id);
            
            var rounds = db.Rounds.Include(r => r.Games).Where(r => r.TournamentId == tournament.Id);
            
            var standings = tournament.Teams.ToList().OrderByDescending(t => t.Points);
            
            return View(new StandingsViewModel {
                Rounds = tournament.Rounds,
                Teams = standings
            });
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
