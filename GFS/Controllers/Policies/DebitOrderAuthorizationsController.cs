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
        public ActionResult Index(string search)
        {
            var debitOrderAuthorizations = from b in db.DebitOrderAuthorizations.ToList()
                                           select b;
            if(!String.IsNullOrEmpty(search))
            {
                debitOrderAuthorizations = db.DebitOrderAuthorizations.Where(p => p.policyNo==(search));
            }
            return View(debitOrderAuthorizations);
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
            var bankList = new List<SelectListItem>();
            var DirQuery = from e in db.Banks select e;
            foreach (var m in DirQuery)
            {
                bankList.Add(new SelectListItem { Value = m.bankN, Text = m.bankN });
            }
            ViewBag.blist = bankList;
            var accTList = new List<SelectListItem>();
            var atQuery = from e in db.AccTypes select e;
            foreach (var m in atQuery)
            {
                accTList.Add(new SelectListItem { Value = m.accNType, Text = m.accNType });
            }
            ViewBag.atlist = accTList;
            return View();
        }

        // POST: DebitOrderAuthorizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "debitAuthNo,bankName,accNo,branchcode,branchName,accountType,commenceDate,amount,policyNo,payerNo")] DebitOrderAuthorization debitOrderAuthorization)
        {
            if (ModelState.IsValid)
            {
                if(debitOrderAuthorization.commenceDate<=DateTime.Now)
                {
                    Session["responce7"] = "Commence date should not be past date!";
                    return RedirectToAction("Create");
                }
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
                return RedirectToAction("Customer_Details", "NewMembers", new { id = debitOrderAuthorization.policyNo });
            }
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
            return View(debitOrderAuthorization);
        }

        // POST: DebitOrderAuthorizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "debitAuthNo,bankName,accNo,branchcode,branchName,accountType,commenceDate,amount,policyNo,payerNo")] DebitOrderAuthorization debitOrderAuthorization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debitOrderAuthorization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
