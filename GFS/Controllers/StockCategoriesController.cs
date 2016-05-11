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
    public class StockCategoriesController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: StockCategories
        public ActionResult Index()
        {
            return View(db.StockCategories.ToList());
        }

        // GET: StockCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockCategory stockCategory = db.StockCategories.Find(id);
            if (stockCategory == null)
            {
                return HttpNotFound();
            }
            return View(stockCategory);
        }

        // GET: StockCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stockCatNo,category")] StockCategory stockCategory)
        {
            if (ModelState.IsValid)
            {
                db.StockCategories.Add(stockCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockCategory);
        }

        // GET: StockCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockCategory stockCategory = db.StockCategories.Find(id);
            if (stockCategory == null)
            {
                return HttpNotFound();
            }
            return View(stockCategory);
        }

        // POST: StockCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stockCatNo,category")] StockCategory stockCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockCategory);
        }

        // GET: StockCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockCategory stockCategory = db.StockCategories.Find(id);
            if (stockCategory == null)
            {
                return HttpNotFound();
            }
            return View(stockCategory);
        }

        // POST: StockCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockCategory stockCategory = db.StockCategories.Find(id);
            db.StockCategories.Remove(stockCategory);
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
