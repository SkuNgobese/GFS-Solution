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
    public class Plan_TypeController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Plan_Type
        public ActionResult Index()
        {
            return View(db.Plan_Type.ToList());
        }

        // GET: Plan_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan_Type plan_Type = db.Plan_Type.Find(id);
            if (plan_Type == null)
            {
                return HttpNotFound();
            }
            return View(plan_Type);
        }

        // GET: Plan_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plan_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "planId,plan")] Plan_Type plan_Type)
        {
            if (ModelState.IsValid)
            {
                db.Plan_Type.Add(plan_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plan_Type);
        }

        // GET: Plan_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan_Type plan_Type = db.Plan_Type.Find(id);
            if (plan_Type == null)
            {
                return HttpNotFound();
            }
            return View(plan_Type);
        }

        // POST: Plan_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "planId,plan")] Plan_Type plan_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plan_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plan_Type);
        }

        // GET: Plan_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan_Type plan_Type = db.Plan_Type.Find(id);
            if (plan_Type == null)
            {
                return HttpNotFound();
            }
            return View(plan_Type);
        }

        // POST: Plan_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plan_Type plan_Type = db.Plan_Type.Find(id);
            db.Plan_Type.Remove(plan_Type);
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
