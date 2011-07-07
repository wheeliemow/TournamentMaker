using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TournamentReport.Models;


namespace TournamentReport.Controllers {
    public class HomeController : Controller {
        private TournamentContext db = new TournamentContext();

        public ActionResult Index() {
            var tournaments = db.Tournaments.Include(t => t.Owner).ToList();
            return View(tournaments);
        }

        public ActionResult Standings(string tournamentSlug) {
            var tournament = db.Tournaments.
                Include(t => t.Teams).
                Include(t => t.Rounds).
                First(t => t.Slug == tournamentSlug);

            var rounds = db.Rounds.Include(r => r.Games).Where(r => r.TournamentId == tournament.Id);

            var standings = tournament.Teams.ToList().OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalsScored - t.GoalsAgainst)
                .ThenBy(t => t.GoalsAgainst);

            // Temporary hack
            ViewBag.TournamentSlug = tournament.Slug;

            return View(new StandingsViewModel {
                Title = tournament.Name,
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
