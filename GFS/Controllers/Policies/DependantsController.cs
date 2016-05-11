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
    public class DependantsController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Dependants
        public ActionResult Index()
        {
            var dependants = db.Dependants.Include(b => b.NewMembers);
            return View(dependants.ToList());
        }

        // GET: Dependants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = db.Dependants.Find(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            return View(dependant);
        }

        // GET: Dependants/Create
        public ActionResult Create()
        {
            var relList = new List<SelectListItem>();
            var DirQuery = from e in db.Relations select e;
            foreach (var m in DirQuery)
            {
                relList.Add(new SelectListItem { Value = m.relationsh, Text = m.relationsh });
            }
            ViewBag.rlist = relList;
            return View();
        }

        // POST: Dependants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "depNo,coveredby,fName,lName,IdNo,dOb,age,relationship,amount,policyPlan,policyNo")] Dependant dependant,bool asBeneficiary, bool addAnotherDep)
        {
            //if (ModelState.IsValid)
            //{ 
                if (Session["owner"]!=null)
                {
                    dependant.coveredby = (Session["owner"]).ToString();
                }
                if (Session["polplan"]!=null)
                {
                    dependant.policyPlan = (Session["polplan"]).ToString();
                }
                if (Session["PolicyNo"]!=null)
                {
                    dependant.policyNo = (Session["PolicyNo"]).ToString(); 
                }                                
                db.Dependants.Add(dependant);                
                db.SaveChanges();
                if (dependant != null)
                {
                    Session["Dep"] = dependant;
                }
                if ((Session["prem"])!=null)
                {
                    Session["addamount"] = dependant.amount + Convert.ToDouble(Session["prem"]);
                }
                if (addAnotherDep == true)
                {
                    //Session["owner"] = dependant.coveredby;
                    //Session["polplan"] = dependant.policyPlan;
                    //Session["PolicyNo"] = dependant.policyNo;
                    return RedirectToAction("Create", "Dependants");
                }
                else if(addAnotherDep == false)
                {
                    return RedirectToAction("Create", "Beneficiaries");
                }
                if (asBeneficiary == true)
                {
                    Session["finame"] = dependant.fName;
                    Session["laname"] = dependant.lName;
                    Session["Id"] = dependant.IdNo;
                    Session["relation"] = dependant.relationship;
                    //Session["plan"] = dependant.policyPlan;
                    //Session["PolicyNo"] = dependant.policyNo;
                }
                
                
            //}

              return RedirectToAction("Create", "Beneficiaries");
        }

        // GET: Dependants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = db.Dependants.Find(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            return View(dependant);
        }

        // POST: Dependants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "depNo,coveredby,fName,lName,IdNo,dOb,age,relationship,amount,policyPlan,policyNo")] Dependant dependant)
        {
            if (ModelState.IsValid)
            {
                //dependant.addAnotherDep = false;
                db.Entry(dependant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dependant);
        }

        // GET: Dependants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = db.Dependants.Find(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            return View(dependant);
        }

        // POST: Dependants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dependant dependant = db.Dependants.Find(id);
            db.Dependants.Remove(dependant);
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
