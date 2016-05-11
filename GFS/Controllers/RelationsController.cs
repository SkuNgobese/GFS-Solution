using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GFS.Models;

namespace GFS.Controllers
{
    public class RelationsController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Relations
        public ActionResult Index()
        {
            return View(db.Relations.ToList());
        }

        // GET: Relations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relation relation = db.Relations.Find(id);
            if (relation == null)
            {
                return HttpNotFound();
            }
            return View(relation);
        }

        // GET: Relations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "relationNo,relationsh")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                db.Relations.Add(relation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(relation);
        }

        // GET: Relations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relation relation = db.Relations.Find(id);
            if (relation == null)
            {
                return HttpNotFound();
            }
            return View(relation);
        }

        // POST: Relations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "relationNo,relationsh")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relation);
        }

        // GET: Relations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relation relation = db.Relations.Find(id);
            if (relation == null)
            {
                return HttpNotFound();
            }
            return View(relation);
        }

        // POST: Relations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relation relation = db.Relations.Find(id);
            db.Relations.Remove(relation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
