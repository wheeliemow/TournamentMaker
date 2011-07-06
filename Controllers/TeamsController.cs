using System.Data;
using System.Linq;
using System.Web.Mvc;
using TournamentReport.Models;

namespace TournamentReport.Controllers {
    public class TeamsController : Controller {
        private TournamentContext db = new TournamentContext();

        //
        // GET: /Teams/

        public ViewResult Index() {
            return View(db.Teams.ToList().OrderByDescending(t => t.Points));
        }

        //
        // GET: /Teams/Details/5

        public ViewResult Details(int id) {
            Team team = db.Teams.Find(id);
            return View(team);
        }

        //
        // GET: /Teams/Create

        public ActionResult Create() {
            return View();
        }

        //
        // POST: /Teams/Create

        [HttpPost]
        public ActionResult Create(Team team) {
            if (ModelState.IsValid) {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        //
        // GET: /Teams/Edit/5

        public ActionResult Edit(int id) {
            Team team = db.Teams.Find(id);
            return View(team);
        }

        //
        // POST: /Teams/Edit/5

        [HttpPost]
        public ActionResult Edit(Team team) {
            if (ModelState.IsValid) {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        //
        // GET: /Teams/Delete/5

        public ActionResult Delete(int id) {
            Team team = db.Teams.Find(id);
            return View(team);
        }

        //
        // POST: /Teams/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}