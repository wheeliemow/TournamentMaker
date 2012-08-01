using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TournamentReport.Models;

namespace TournamentReport.Controllers
{
    public class GamesController : Controller
    {
        private readonly TournamentContext db = new TournamentContext();

        public ActionResult Create(string tournamentSlug, int id)
        {
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Name", id);
            var teams = db.Teams.Where(t => t.Tournament.Slug == tournamentSlug);
            ViewBag.HomeTeamId = new SelectList(teams, "Id", "Name");
            ViewBag.AwayTeamId = new SelectList(teams, "Id", "Name");

            var model = new GameCreateModel
                        {
                            RoundId = id
                        };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(GameCreateModel gameModel, string tournamentSlug)
        {
            if (gameModel.HomeTeamId == gameModel.AwayTeamId)
            {
                ModelState.AddModelError("AwayTeamId", "A team cannot play itself");
            }
            if (ModelState.IsValid)
            {
                var game = new Game {RoundId = gameModel.RoundId.Value};
                db.Games.Add(game);
                db.SaveChanges();

                var model = new GameEditModel
                            {Id = game.Id, HomeTeamId = gameModel.HomeTeamId, AwayTeamId = gameModel.AwayTeamId};
                return Edit(model, tournamentSlug);
            }

            var teams = db.Teams.Where(t => t.Tournament.Slug == tournamentSlug);
            ViewBag.HomeTeamId = new SelectList(teams, "Id", "Name");
            ViewBag.AwayTeamId = new SelectList(teams, "Id", "Name");

            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Name", gameModel.RoundId);
            return View(gameModel);
        }

        public ActionResult ReportScores(int id, string tournamentSlug)
        {
            Game game = db.Games.Include(g => g.Teams).FirstOrDefault(g => g.Id == id);
            return View(game);
        }

        [HttpPost]
        public ActionResult ReportScores(Game game, string tournamentSlug)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Standings", "Home", new {tournamentSlug});
            }

            return View(game);
        }

        public ActionResult Edit(int id, string tournamentSlug)
        {
            Game game = db.Games.Include(g => g.Teams).FirstOrDefault(g => g.Id == id);

            var model = new GameEditModel
                        {
                            Id = game.Id
                        };

            if (game.HomeTeam != null)
            {
                model.HomeTeamId = game.HomeTeam.Id;
            }
            if (game.AwayTeam != null)
            {
                model.AwayTeamId = game.AwayTeam.Id;
            }

            var teams = db.Teams.Where(t => t.Tournament.Slug == tournamentSlug);
            ViewBag.HomeTeamId = new SelectList(teams, "Id", "Name", model.HomeTeamId);
            ViewBag.AwayTeamId = new SelectList(teams, "Id", "Name", model.AwayTeamId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(GameEditModel game, string tournamentSlug)
        {
            if (game.HomeTeamId == game.AwayTeamId)
            {
                ModelState.AddModelError("AwayTeamId", "A team cannot play itself");
            }

            if (ModelState.IsValid)
            {
                var dbGame = db.Games.Include(g => g.Teams).FirstOrDefault(g => g.Id == game.Id);
                dbGame.AddTeams(db.Teams.Find(game.HomeTeamId), db.Teams.Find(game.AwayTeamId));

                db.SaveChanges();
                return RedirectToAction("Standings", "Home", new {tournamentSlug});
            }
            var teams = db.Teams.Where(t => t.Tournament.Slug == tournamentSlug);
            ViewBag.HomeTeamId = new SelectList(teams, "Id", "Name", game.HomeTeamId);
            ViewBag.AwayTeamId = new SelectList(teams, "Id", "Name", game.AwayTeamId);

            return View(game);
        }


        public ActionResult Delete(int id)
        {
            Game game = db.Games.Find(id);
            return View(game);
        }

        //
        // POST: /Games/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string tournamentSlug)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Standings", "Home", new {tournamentSlug});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult ResetScores(string tournamentSlug)
        {
            return View();
        }

        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public ActionResult ResetScores(string tournamentSlug, string submit)
        {
            var games = (from team in db.Teams.Include(t => t.Games).Include(t => t.Tournament)
                         where team.Tournament.Slug == tournamentSlug
                         select team).SelectMany(t => t.Games).ToList();

            foreach (var game in games)
            {
                game.HomeTeamScore = null;
                game.AwayTeamScore = null;
            }
            db.SaveChanges();

            return RedirectToAction("Standings", "Home", new {tournamentSlug});
        }
    }
}