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
    public class AccTypesController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: AccTypes
        public ActionResult Index()
        {
            return View(db.AccTypes.ToList());
        }

        // GET: AccTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccType accType = db.AccTypes.Find(id);
            if (accType == null)
            {
                return HttpNotFound();
            }
            return View(accType);
        }

        // GET: AccTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "typeId,accNType")] AccType accType)
        {
            if (ModelState.IsValid)
            {
                db.AccTypes.Add(accType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accType);
        }

        // GET: AccTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccType accType = db.AccTypes.Find(id);
            if (accType == null)
            {
                return HttpNotFound();
            }
            return View(accType);
        }

        // POST: AccTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "typeId,accNType")] AccType accType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accType);
        }

        // GET: AccTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccType accType = db.AccTypes.Find(id);
            if (accType == null)
            {
                return HttpNotFound();
            }
            return View(accType);
        }

        // POST: AccTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccType accType = db.AccTypes.Find(id);
            db.AccTypes.Remove(accType);
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
