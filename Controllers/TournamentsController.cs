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
    public class TournamentsController : Controller
    {
        private TournamentContext db = new TournamentContext();

        //
        // GET: /Tournaments/

        public ViewResult Index()
        {
            return View(db.Tournaments.ToList());
        }

        //
        // GET: /Tournaments/Details/5

        public ViewResult Details(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            return View(tournament);
        }

        //
        // GET: /Tournaments/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tournaments/Create

        [HttpPost]
        public ActionResult Create(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                db.Tournaments.Add(tournament);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tournament);
        }
        
        //
        // GET: /Tournaments/Edit/5
 
        public ActionResult Edit(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            return View(tournament);
        }

        //
        // POST: /Tournaments/Edit/5

        [HttpPost]
        public ActionResult Edit(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

        //
        // GET: /Tournaments/Delete/5
 
        public ActionResult Delete(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            return View(tournament);
        }

        //
        // POST: /Tournaments/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Tournament tournament = db.Tournaments.Find(id);
            db.Tournaments.Remove(tournament);
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