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
    public class PolicyPlansController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: PolicyPlans
        public ActionResult Index()
        {
            return View(db.PolicyPlans.ToList());
        }

        // GET: PolicyPlans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PolicyPlan policyPlan = db.PolicyPlans.Find(id);
            if (policyPlan == null)
            {
                return HttpNotFound();
            }
            return View(policyPlan);
        }

        // GET: PolicyPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PolicyPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PolicyPlanNo,category,policyID,policyType,minAge,maxAge,PlanPremium,benefit,payout,dependantPrem,TandCs")] PolicyPlan policyPlan)
        {
            if (ModelState.IsValid)
            {
                if(policyPlan.policyType=="Plan A")
                {
                    policyPlan.policyID = 1;
                }
                else if(policyPlan.policyType=="Plan B")
                {
                    policyPlan.policyID = 2;
                }
                else if (policyPlan.policyType == "Plan C1")
                {
                    policyPlan.policyID = 3;
                }
                else if (policyPlan.policyType == "Plan C2")
                {
                    policyPlan.policyID = 4;
                }
                else if (policyPlan.policyType == "Plan C3")
                {
                    policyPlan.policyID = 5;
                }
                db.PolicyPlans.Add(policyPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(policyPlan);
        }

        // GET: PolicyPlans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PolicyPlan policyPlan = db.PolicyPlans.Find(id);
            if (policyPlan == null)
            {
                return HttpNotFound();
            }
            return View(policyPlan);
        }

        // POST: PolicyPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PolicyPlanNo,category,policyID,policyType,minAge,maxAge,PlanPremium,benefit,payout,dependantPrem,TandCs")] PolicyPlan policyPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policyPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(policyPlan);
        }

        // GET: PolicyPlans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PolicyPlan policyPlan = db.PolicyPlans.Find(id);
            if (policyPlan == null)
            {
                return HttpNotFound();
            }
            return View(policyPlan);
        }

        // POST: PolicyPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PolicyPlan policyPlan = db.PolicyPlans.Find(id);
            db.PolicyPlans.Remove(policyPlan);
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
