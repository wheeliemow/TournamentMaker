using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TournamentReport.Models;
using TournamentReport;

namespace TournamentReport.Controllers
{ 
    public class RoundsController : Controller
    {
        private TournamentContext db = new TournamentContext();

        //
        // GET: /Rounds/

        public ViewResult Index()
        {
            var rounds = db.Rounds.Include(r => r.Tournament);
            return View(rounds.ToList());
        }

        //
        // GET: /Rounds/Details/5

        public ViewResult Details(int id)
        {
            Round round = db.Rounds.Find(id);
            return View(round);
        }

        //
        // GET: /Rounds/Create

        public ActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Name");
            return View();
        } 

        //
        // POST: /Rounds/Create

        [HttpPost]
        public ActionResult Create(Round round)
        {
            if (ModelState.IsValid)
            {
                db.Rounds.Add(round);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Name", round.TournamentId);
            return View(round);
        }
        
        //
        // GET: /Rounds/Edit/5
 
        public ActionResult Edit(int id)
        {
            Round round = db.Rounds.Find(id);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Name", round.TournamentId);
            return View(round);
        }

        //
        // POST: /Rounds/Edit/5

        [HttpPost]
        public ActionResult Edit(Round round)
        {
            if (ModelState.IsValid)
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Name", round.TournamentId);
            return View(round);
        }

        //
        // GET: /Rounds/Delete/5
 
        public ActionResult Delete(int id)
        {
            Round round = db.Rounds.Find(id);
            return View(round);
        }

        //
        // POST: /Rounds/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Round round = db.Rounds.Find(id);
            db.Rounds.Remove(round);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}