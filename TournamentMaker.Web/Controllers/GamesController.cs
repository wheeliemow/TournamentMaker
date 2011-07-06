using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TournamentReport.Models;

namespace TournamentReport.Controllers {
    public class GamesController : Controller {
        private TournamentContext db = new TournamentContext();

        //
        // GET: /Games/

        public ViewResult Index() {
            var games = db.Games.Include(g => g.Round).Include(g => g.Teams);
            return View(games.ToList().OrderBy(g => g.Round.Id));
        }


        //
        // GET: /Games/Create

        public ActionResult Create() {
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Id");
            return View();
        }

        //
        // POST: /Games/Create

        [HttpPost]
        public ActionResult Create(Game game) {
            if (ModelState.IsValid) {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Id", game.RoundId);
            return View(game);
        }

        //
        // GET: /Games/Edit/5

        public ActionResult Edit(int id) {
            Game game = db.Games.Include(g => g.Teams).FirstOrDefault(g => g.Id == id);
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Id", game.RoundId);
            return View(game);
        }

        //
        // POST: /Games/Edit/5

        [HttpPost]
        public ActionResult Edit(Game game) {
            if (ModelState.IsValid) {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Id", game.RoundId);
            return View(game);
        }

        //
        // GET: /Games/Delete/5

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