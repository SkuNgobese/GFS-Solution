using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GFS.Models;
using System.IO;

namespace GFS.Controllers
{
    public class StockFilesController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: StockFiles
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "stockCode")
            {
                return View(db.StockFiles.Where(x => x.stockCode == search || search == null).ToList());
            }
            else
               if (searchBy == "status")
            {
                return View(db.StockFiles.Where(x => x.status == search || search == null).ToList());

            }
            else
            {
                return View(db.StockFiles.Where(x => x.category == search || search == null).ToList());
            }
        }

        // GET: StockFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockFile stockFile = db.StockFiles.Find(id);
            if (stockFile == null)
            {
                return HttpNotFound();
            }
            return View(stockFile);
        }
        public ActionResult CreateStock(StockFile stock, IEnumerable<HttpPostedFileBase> file)
        {
            //if (ModelState.IsValid)
            //{

            foreach (var upload in file)
            {
                if (upload.ContentLength > 0)
                {
                    var uploaddocs = Path.GetFileName(upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Files"), uploaddocs);
                    stock.albumArtUrl = uploaddocs;
                    upload.SaveAs(path);
                    db.StockFiles.Add(stock);
                    db.SaveChanges();
                    Session["Category"].ToString();
                    Session["Description"].ToString();
                    Session["Status"].ToString();
                    Session["Quantity"].ToString();
                    Session["Cost Price"].ToString();
                    Session["Selling Price"].ToString();
                    return View(stock);
                }
            }
            //}
            return RedirectToAction("Index");
        }

        // GET: StockFiles/Create
        public ActionResult Create()
        {
            var catList = new List<SelectListItem>();
            var DirQuery = from e in db.StockCategories select e;
            foreach (var m in DirQuery)
            {
                catList.Add(new SelectListItem { Value = m.category, Text = m.category });
            }
            ViewBag.catlist = catList;
            return View();
        }

        // POST: StockFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stockNumber,stockCode,category,description,albumArtUrl,status,quantity,pricePerItem,costPrice,sellingPrice")] StockFile stockFile)
        {
            if (ModelState.IsValid)
            {
                stockFile.pricePerItem = stockFile.costPrice / stockFile.quantity;
                db.StockFiles.Add(stockFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockFile);
        }

        // GET: StockFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            var catList = new List<SelectListItem>();
            var DirQuery = from e in db.StockCategories select e;
            foreach (var m in DirQuery)
            {
                catList.Add(new SelectListItem { Value = m.category, Text = m.category });
            }
            ViewBag.catelist = catList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockFile stockFile = db.StockFiles.Find(id);
            if (stockFile == null)
            {
                return HttpNotFound();
            }
            return View(stockFile);
        }

        // POST: StockFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stockNumber,stockCode,category,description,albumArtUrl,status,quantity,pricePerItem,costPrice,sellingPrice")] StockFile stockFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockFile);
        }

        // GET: StockFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockFile stockFile = db.StockFiles.Find(id);
            if (stockFile == null)
            {
                return HttpNotFound();
            }
            return View(stockFile);
        }

        // POST: StockFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockFile stockFile = db.StockFiles.Find(id);
            db.StockFiles.Remove(stockFile);
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
