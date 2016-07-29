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
        public ActionResult Index(string searchBy, string search)
        {
            var dependant = from d in db.Dependants.ToList()
                            select d;
            if (searchBy == "policyNo")
            {
                dependant=(db.Dependants.Where(x => x.policyNo == search).ToList());
            }
            else if (searchBy == "fName")
            {
                dependant=(db.Dependants.Where(x => x.fName == search).ToList());
            }
            else if (searchBy == "idnumber")
            {              
                dependant=(db.Dependants.Where(x => x.IdNo == search).ToList());
            }
            else if(searchBy == "coveredby")
            {
                dependant=(db.Dependants.Where(x => x.coveredby == search).ToList());
            }
            return View(dependant);
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
        public ActionResult Create([Bind(Include = "depNo,coveredby,fName,lName,IdNo,dOb,relationship,amount,policyPlan,asBeneficiary,addAnotherDep,policyNo")] Dependant dependant)
        {
            Dependant idno = db.Dependants.ToList().Find(x => x.IdNo == dependant.IdNo);
            if (idno != null)
            {
                TempData["responce4"] = "Dependant Already Exists, Check ID Number! Covered under: "+idno.coveredby;
                return RedirectToAction("Create");
            }
            //if (ModelState.IsValid)
            //{
            
                else if (Session["owner"] != null)
                {
                    dependant.coveredby = (Session["owner"]).ToString();
                }
                if (Session["polplan"] != null)
                {
                    dependant.policyPlan = (Session["polplan"]).ToString();
                }
                if (Session["PolicyNo"] != null)
                {
                    dependant.policyNo = (Session["PolicyNo"]).ToString();
                }
                if (dependant.IdNo != null)
                {
                    int year = Convert.ToInt32(dependant.IdNo.Substring(0, 2));
                    int month = Convert.ToInt16(dependant.IdNo.Substring(2, 2));
                    int day = Convert.ToInt16(dependant.IdNo.Substring(4, 2));

                    var d = (year + "-" + month + "-" + day);
                    DateTime d1 = DateTime.Parse(d);
                    dependant.dOb = d1;
                }
                int age = new DateTime(DateTime.Now.Subtract(dependant.dOb).Ticks).Year - 1;
                if (dependant.policyPlan == "Plan A")
                {
                    if (age <= 64)
                    {
                        dependant.amount = 80;
                    }
                    else if (age <= 74)
                    {
                        dependant.amount = 160;
                    }
                    else if (age <= 84)
                    {
                        dependant.amount = 310;
                    }
                    else
                    {
                        TempData["responce4"] = "Cannot add person over 84 years from this plan!";
                        return View("Create");
                    }
                }
                if (dependant.policyPlan == "Plan B")
                {
                    if (age <= 64)
                    {
                        dependant.amount = 60;
                    }
                    else if (age <= 74)
                    {
                        dependant.amount = 130;
                    }
                    else if (age <= 84)
                    {
                        dependant.amount = 170;
                    }
                    else
                    {
                        dependant.amount = 280;
                    }
                }
                if (dependant.policyPlan == "Plan C1")
                {
                    if (age <= 64)
                    {
                        dependant.amount = 46;
                    }
                    else if (age <= 74)
                    {
                        dependant.amount = 82;
                    }
                    else if (age <= 84)
                    {
                        dependant.amount = 109;
                    }
                    else
                    {
                        dependant.amount = 200;
                    }
                }
                if (dependant.policyPlan == "Plan C2")
                {
                    if (age <= 64)
                    {
                        dependant.amount = 64;
                    }
                    else if (age <= 74)
                    {
                        dependant.amount = 117;
                    }
                    else if (age <= 84)
                    {
                        dependant.amount = 158;
                    }
                    else
                    {
                        dependant.amount = 234;
                    }
                }
                if (dependant.policyPlan == "Plan C3")
                {
                    if (age <= 64)
                    {
                        dependant.amount = 82;
                    }
                    else if (age <= 74)
                    {
                        dependant.amount = 153;
                    }
                    else if (age <= 84)
                    {
                        dependant.amount = 207;
                    }
                    else
                    {
                        TempData["responce4"] = "Cannot add person over 84 years from this plan!";
                        return View("Create");
                    }
                }
                else if (age <= 18 || dependant.relationship == "Spouse")
                {
                    dependant.amount = 0;
                }
                db.Dependants.Add(dependant);
                db.SaveChanges();
                var deps = db.Dependants.ToList().Where(p => p.policyNo == dependant.policyNo);
                foreach(Dependant d in deps)
                {
                    if (dependant != null)
                    {
                        Session["Dep"] = d;
                    }
                }                
                if (dependant.asBeneficiary == true)
                {
                    Session["finame"] = dependant.fName;
                    Session["laname"] = dependant.lName;
                    Session["Id"] = dependant.IdNo;
                    Session["relation"] = dependant.relationship;
                }
                if (dependant.addAnotherDep == true)
                {
                    return RedirectToAction("Create", "Dependants");
                }
                else if (dependant.addAnotherDep == false)
                {
                    return RedirectToAction("Create", "Beneficiaries");
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
        public ActionResult Edit([Bind(Include = "depNo,coveredby,fName,lName,IdNo,dOb,relationship,amount,policyPlan,asBeneficiary,addAnotherDep,policyNo")] Dependant dependant)
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
