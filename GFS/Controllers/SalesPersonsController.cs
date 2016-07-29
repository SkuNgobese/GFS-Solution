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
    public class SalesPersonsController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: SalesPersons
        public ActionResult Index()
        {
            return View(db.SalesPersons.ToList());
        }

        // GET: SalesPersons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesPerson salesPerson = db.SalesPersons.Find(id);
            if (salesPerson == null)
            {
                return HttpNotFound();
            }
            return View(salesPerson);
        }

        // GET: SalesPersons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesPersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,fName,lName,IdNo,telNo,cellNo,Email")] SalesPerson salesPerson)
        {
            if (ModelState.IsValid)
            {
                db.SalesPersons.Add(salesPerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salesPerson);
        }

        // GET: SalesPersons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesPerson salesPerson = db.SalesPersons.Find(id);
            if (salesPerson == null)
            {
                return HttpNotFound();
            }
            return View(salesPerson);
        }

        // POST: SalesPersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,fName,lName,IdNo,telNo,cellNo,Email")] SalesPerson salesPerson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesPerson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesPerson);
        }

        // GET: SalesPersons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesPerson salesPerson = db.SalesPersons.Find(id);
            if (salesPerson == null)
            {
                return HttpNotFound();
            }
            return View(salesPerson);
        }

        // POST: SalesPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesPerson salesPerson = db.SalesPersons.Find(id);
            db.SalesPersons.Remove(salesPerson);
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
