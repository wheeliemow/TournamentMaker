using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TournamentReport.Models;

namespace TournamentReport.Controllers {
    public class GamesController : Controller {
        private TournamentContext db = new TournamentContext();

        public ActionResult Create() {
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Id");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Game game, string tournamentSlug) {
            if (ModelState.IsValid) {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Standings", "Home", new { tournamentSlug });
            }

            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Id", game.RoundId);
            return View(game);
        }

        public ActionResult ReportScores(int id, string tournamentSlug) {
            Game game = db.Games.Include(g => g.Teams).FirstOrDefault(g => g.Id == id);
            return View(game);
        }

        [HttpPost]
        public ActionResult ReportScores(Game game, string tournamentSlug) {
            if (ModelState.IsValid) {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Standings", "Home", new { tournamentSlug });
            }

            return View(game);
        }

        public ActionResult Edit(int id, string tournamentSlug) {
            Game game = db.Games.Include(g => g.Teams).FirstOrDefault(g => g.Id == id);
            var teams = db.Teams.Where(t => t.Tournament.Slug == tournamentSlug);
            ViewBag.HomeTeamId = new SelectList(teams, "Id", "Name");
            ViewBag.AwayTeamId = new SelectList(teams, "Id", "Name");

            var model = new GameEditModel {
                Id = game.Id
            };

            if (game.HomeTeam != null) {
                model.HomeTeamId = game.HomeTeam.Id;
            }
            if (game.AwayTeam != null) {
                model.AwayTeamId = game.AwayTeam.Id;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(GameEditModel game, string tournamentSlug) {
            if (game.HomeTeamId == game.AwayTeamId) {
                ModelState.AddModelError("AwayTeamId", "A team cannot play itself");
            }

            if (ModelState.IsValid) {
                var dbGame = db.Games.Find(game.Id);
                db.Teams.Find(game.HomeTeamId).Games.Add(dbGame);
                db.Teams.Find(game.AwayTeamId).Games.Add(dbGame);

                db.SaveChanges();
                return RedirectToAction("Standings", "Home", new { tournamentSlug });
            }
            var teams = db.Teams.Where(t => t.Tournament.Slug == tournamentSlug);
            ViewBag.HomeTeamId = new SelectList(teams, "Id", "Name");
            ViewBag.AwayTeamId = new SelectList(teams, "Id", "Name");

            return View(game);
        }


        public ActionResult Delete(int id) {
            Game game = db.Games.Find(id);
            return View(game);
        }

        //
        // POST: /Games/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}