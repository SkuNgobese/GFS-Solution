using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GFS.Models;
using GFS.Models.Policies;

namespace GFS.Controllers.Policies
{
    public class DebitOrderAuthorizationsController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: DebitOrderAuthorizations
        public ActionResult Index()
        {
            var debitOrderAuthorizations = db.DebitOrderAuthorizations.Include(d => d.NewMembers).Include(d => d.Payers);
            return View(debitOrderAuthorizations.ToList());
        }

        // GET: DebitOrderAuthorizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DebitOrderAuthorization debitOrderAuthorization = db.DebitOrderAuthorizations.Find(id);
            if (debitOrderAuthorization == null)
            {
                return HttpNotFound();
            }
            return View(debitOrderAuthorization);
        }

        // GET: DebitOrderAuthorizations/Create
        public ActionResult Create()
        {
            //ViewBag.policyNo = new SelectList(db.NewMembers, "policyNo", "title");
            //ViewBag.payerNo = new SelectList(db.Payers, "payerNo", "paymentType");
            return View();
        }

        // POST: DebitOrderAuthorizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "debitAuthNo,commenceDate,amount,policyNo,payerNo")] DebitOrderAuthorization debitOrderAuthorization)
        {
            if (ModelState.IsValid)
            {
                if (Session["policyNo"]!=null)
                {
                    debitOrderAuthorization.policyNo = (Session["policyNo"]).ToString();
                }
                if (Session["payerNo"] != null)
                {
                    debitOrderAuthorization.payerNo = Convert.ToInt32(Session["payerNo"]);
                }
                if (Session["addamount"] != null)
                {
                    debitOrderAuthorization.amount = Convert.ToDouble(Session["addamount"]);
                }
                else if (Session["addamount"] == null && Session["prem"] != null)
                {
                    debitOrderAuthorization.amount = Convert.ToDouble(Session["prem"]);
                }
                db.DebitOrderAuthorizations.Add(debitOrderAuthorization);
                db.SaveChanges();
                if(debitOrderAuthorization!=null)
                {
                    Session["Debit"] = debitOrderAuthorization;
                }                
                return RedirectToAction("Details", new { id = debitOrderAuthorization.debitAuthNo });
            }

            //ViewBag.policyNo = new SelectList(db.NewMembers, "policyNo", "title", debitOrderAuthorization.policyNo);
            //ViewBag.payerNo = new SelectList(db.Payers, "payerNo", "paymentType", debitOrderAuthorization.payerNo);
            return View(debitOrderAuthorization);
        }

        // GET: DebitOrderAuthorizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DebitOrderAuthorization debitOrderAuthorization = db.DebitOrderAuthorizations.Find(id);
            if (debitOrderAuthorization == null)
            {
                return HttpNotFound();
            }
            //ViewBag.policyNo = new SelectList(db.NewMembers, "policyNo", "title", debitOrderAuthorization.policyNo);
            //ViewBag.payerNo = new SelectList(db.Payers, "payerNo", "paymentType", debitOrderAuthorization.payerNo);
            return View(debitOrderAuthorization);
        }

        // POST: DebitOrderAuthorizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "debitAuthNo,commenceDate,amount,policyNo,payerNo")] DebitOrderAuthorization debitOrderAuthorization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debitOrderAuthorization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.policyNo = new SelectList(db.NewMembers, "policyNo", "title", debitOrderAuthorization.policyNo);
            //ViewBag.payerNo = new SelectList(db.Payers, "payerNo", "paymentType", debitOrderAuthorization.payerNo);
            return View(debitOrderAuthorization);
        }

        // GET: DebitOrderAuthorizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DebitOrderAuthorization debitOrderAuthorization = db.DebitOrderAuthorizations.Find(id);
            if (debitOrderAuthorization == null)
            {
                return HttpNotFound();
            }
            return View(debitOrderAuthorization);
        }

        // POST: DebitOrderAuthorizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DebitOrderAuthorization debitOrderAuthorization = db.DebitOrderAuthorizations.Find(id);
            db.DebitOrderAuthorizations.Remove(debitOrderAuthorization);
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
