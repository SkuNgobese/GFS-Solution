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
    public class JoiningFeesController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: JoiningFees
        public ActionResult Index()
        {
            return View(db.JoiningFees.ToList());
        }

        // GET: JoiningFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JoiningFee joiningFee = db.JoiningFees.Find(id);
            if (joiningFee == null)
            {
                return HttpNotFound();
            }
            return View(joiningFee);
        }

        // GET: JoiningFees/Create
        public ActionResult Create()
        {
            var branchList = new List<SelectListItem>();
            var branQuery = from e in db.Branches orderby e.branchN select e;
            foreach (var m in branQuery)
            {
                branchList.Add(new SelectListItem { Value = m.branchN, Text = m.branchN });
            }
            ViewBag.branchlist = branchList;
            return View();
        }

        // POST: JoiningFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "refNo,policyNo,CustomerName,AmountRendered,change,Fee,date,cashierName,branch")] JoiningFee joiningFee)
        {
            var d = db.NewMembers.ToList().Find(r => r.policyNo == joiningFee.policyNo);
            if (d == null)
            {
                Session["resp"] = "Sorry, Member does not exist in the database! please add the Member first.";
                return View("Create");
            }
            else if (ModelState.IsValid)
            {
                joiningFee.Fee = 50.00;
                joiningFee.CustomerName = d.fName + " " + d.lName;
                joiningFee.change = joiningFee.AmountRendered- joiningFee.Fee;
                joiningFee.date = DateTime.Now;
                joiningFee.cashierName = User.Identity.Name;
                db.JoiningFees.Add(joiningFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(joiningFee);
        }

        // GET: JoiningFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JoiningFee joiningFee = db.JoiningFees.Find(id);
            if (joiningFee == null)
            {
                return HttpNotFound();
            }
            return View(joiningFee);
        }

        // POST: JoiningFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "refNo,policyNo,CustomerName,AmountRendered,change,Fee,date,cashierName,branch")] JoiningFee joiningFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(joiningFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(joiningFee);
        }

        // GET: JoiningFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JoiningFee joiningFee = db.JoiningFees.Find(id);
            if (joiningFee == null)
            {
                return HttpNotFound();
            }
            return View(joiningFee);
        }

        // POST: JoiningFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JoiningFee joiningFee = db.JoiningFees.Find(id);
            db.JoiningFees.Remove(joiningFee);
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
