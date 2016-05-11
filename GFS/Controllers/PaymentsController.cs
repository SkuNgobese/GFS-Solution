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
    public class PaymentsController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Payments
        //public ActionResult Index()
        //{
        //    return View(db.Payments.ToList());
        //}
        public ActionResult Index(string searchString)
        {
            var pay = from m in db.Payments
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                pay = pay.Where(s => s.policyNo.Contains(searchString));
            }
            return View(pay);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        public ActionResult Search()
        {
            Session["polNo"] = null;
            Session["fullname"] = null;
            Session["plan"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchStr)
        {
            var payment = from m in db.NewMembers
                          select m;

            if (!String.IsNullOrEmpty(searchStr))
            {
                payment = payment.Where(s => s.policyNo.Contains(searchStr));

                var d = db.NewMembers.ToList().Find(r => r.policyNo == searchStr);
                var du = db.Payers.ToList().Find(r => r.policyNo == searchStr);
                var stand = db.Payments.ToList().Find(r => r.policyNo == searchStr);
                Session["polNo"] = d.policyNo;
                Session["fullname"] = d.fName + " " + d.lName;
                Session["plan"] = d.Policyplan;
                Session["iniPrem"] = du.initialPremium + stand.outstandingAmount;
                return RedirectToAction("Create");
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            var planList = new List<SelectListItem>();
            var PlanQuery = from e in db.PolicyPlans select e;
            foreach (var m in PlanQuery)
            {
                planList.Add(new SelectListItem { Value = m.policyType, Text = m.policyType });
            }
            ViewBag.plnlist = planList;
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "referenceNo,policyNo,CustomerName,plan,dueAmount,amount,outstandingAmount,datePayed,cashierName,branch")] Payment payment)
        {
            //if (ModelState.IsValid)
            //{
            
                if (Session["polNo"]!=null)
                {
                    payment.policyNo = Session["polNo"].ToString();
                }
                if (Session["fullname"]!=null)
                {
                    payment.CustomerName = Session["fullname"].ToString();
                }
                if (Session["plan"]!=null)
                {
                    payment.plan = Session["plan"].ToString();
                }
                if (Session["iniPrem"] != null)
                {
                    payment.dueAmount = Convert.ToDouble(Session["iniPrem"]);
                }
                else if (Session["iniPrem"] == null)
                {
                    payment.dueAmount = 0;
                }
                //var init = db.Payers.ToList().Find(p => p.policyNo == payment.policyNo);

                //Payment outstand = db.Payments.ToList().Find(p => p.policyNo == payment.policyNo);
                //if(init!=null)
                //{
                //    double due = Convert.ToDouble(init.initialPremium) + outstand.outstandingAmount - payment.amount;
                //    payment.dueAmount = due;
                //}
                
                double outst = payment.dueAmount - payment.amount;
                payment.outstandingAmount = outst;
                payment.datePayed = DateTime.Now;

                db.Payments.Add(payment);
                db.SaveChanges();
                //Session["due"] = payment.dueAmount;
                //Session["outstand"] = payment.outstandingAmount;
                
            //}
                return RedirectToAction("Details", new { id = payment.referenceNo });
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "referenceNo,policyNo,CustomerName,plan,dueAmount,amount,outstandingAmount,datePayed,cashierName,branch")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
