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
    public class NewMembersController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: NewMembers
        public ActionResult Index(string searchString)
        {
            var newmembers = from m in db.NewMembers
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                newmembers = newmembers.Where(s => s.policyNo.Contains(searchString));

                NewMember d = db.NewMembers.ToList().Find(r => r.policyNo == searchString);

                Session["First Name"] = d.fName;
                Session["Last Name"] = d.lName;
                Session["ID Number"] = d.IdNo;
                Session["Age"] = d.age;
                Session["Gender"] = d.gender;
                Session["PolicyNo"] = d.policyNo;

                RedirectToAction("Create", "Deceaseds");
            }
            return View(newmembers);

        }
        //public ActionResult Index()
        //{
        //    return View(db.NewMembers.ToList());
        //}

        // GET: NewMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMember newMember = db.NewMembers.Find(id);
            if (newMember == null)
            {
                return HttpNotFound();
            }
            return View(newMember);
        }

        // GET: NewMembers/Create
        public ActionResult Create()
        {
            var planList = new List<SelectListItem>();
            var PlanQuery = from e in db.PolicyPlans select e;
            foreach (var m in PlanQuery)
            {
                planList.Add(new SelectListItem { Value = m.policyType, Text = m.policyType });
            }
            ViewBag.polplanlist = planList;
            return View();
        }

        // POST: NewMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "policyNo,title,fName,lName,IdNo,dOb,age,gender,maritalStat,telNo,cellNo,CustEmail,fascimileNo,physicalAddress,postalAddress,dateAdded,Policyplan,Premium,Category,addDep,paying")] NewMember newMember)
        {
            NewMember polNo = db.NewMembers.ToList().Find(x => x.policyNo == newMember.policyNo);
            NewMember idno = db.NewMembers.ToList().Find(x => x.IdNo == newMember.IdNo);

            if (polNo != null)
            {
                Session["responce"] = "Member Exists, Check The Policy Number!";
                return RedirectToAction("Create");
            }
            if (idno != null)
            {
                Session["responce"] = "Member Exists, Check ID Number!";
                return RedirectToAction("Create");
            }
            else if (ModelState.IsValid)
            {
                if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan A")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 90;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 170;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 320;
                        }
                        else
                        {
                            Session["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                else if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan B")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 65;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 125;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 200;
                        }
                        else
                        {
                            newMember.Premium = 330;
                        }
                    }
                }
                else if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan C1")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 50;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 93;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 131;
                        }
                        else
                        {
                            newMember.Premium = 218;
                        }
                    }
                }
                else if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan C2")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 65;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 119;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 192;
                        }
                        else
                        {
                            newMember.Premium = 322;
                        }
                    }
                }
                else if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan C3")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 83;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 155;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 252;
                        }
                        else
                        {
                            Session["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan A")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 110;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 200;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 386;
                        }
                        else
                        {
                            Session["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                else if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan B")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 90;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 160;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 260;
                        }
                        else
                        {
                            newMember.Premium = 400;
                        }
                    }
                }
                else if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan C1")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 60;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 110;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 170;
                        }
                        else
                        {
                            newMember.Premium = 264;
                        }
                    }
                }
                else if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan C2")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 85;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 160;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 242;
                        }
                        else
                        {
                            newMember.Premium = 391;
                        }
                    }
                }
                else if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan C3")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 110;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 210;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 319;
                        }
                        else
                        {
                            Session["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan A")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 120;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 220;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 530;
                        }
                        else
                        {
                            Session["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                else if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan B")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 100;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 180;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 350;
                        }
                        else
                        {
                            newMember.Premium = 500;
                        }
                    }
                }
                else if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan C1")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 80;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 140;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 220;
                        }
                        else
                        {
                            newMember.Premium = 320;
                        }
                    }
                }
                else if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan C2")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 108;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 170;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 328;
                        }
                        else
                        {
                            newMember.Premium = 475;
                        }
                    }
                }
                else if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan C3")
                    {
                        if (newMember.age <= 64)
                        {
                            newMember.Premium = 140;
                        }
                        else if (newMember.age <= 74)
                        {
                            newMember.Premium = 200;
                        }
                        else if (newMember.age <= 84)
                        {
                            newMember.Premium = 434;
                        }
                        else
                        {
                            Session["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                //if(newMember.IdNo!=null)
                //{
                //    int gnum=Convert.ToInt16(newMember.IdNo.Substring(6, 1));
                //    if (gnum > 4)
                //    {
                //        newMember.gender = "Male";
                //    }
                //    else
                //        newMember.gender = "Female";
                //}
                newMember.dateAdded = DateTime.Now;
                db.NewMembers.Add(newMember);
                db.SaveChanges();
                Session["owner"] = newMember.fName + " " + newMember.lName;
                if (newMember.Policyplan != null)
                {
                    Session["polplan"] = newMember.Policyplan;
                }
                if (newMember.policyNo != null)
                {
                    Session["PolicyNo"] = newMember.policyNo;
                }
                if (newMember != null)
                {
                    Session["NMember"] = newMember;
                }                
                Session["prem"] = newMember.Premium;
                if(newMember.paying==true)
                {
                    Session["fname"] = newMember.fName;
                    Session["lname"] = newMember.lName;
                    Session["idnum"] = newMember.IdNo;
                    Session["pay"] = "Self Payer";
                }
                    
                if (newMember.addDep == true)
                {
                    return RedirectToAction("Create", "Dependants");
                }
                else if (newMember.Category == "Single Member")
                {
                    return RedirectToAction("Create", "Payers");
                }
                else if (newMember.Category == "Immediate Family")
                {
                    return RedirectToAction("Create", "Dependants");
                }
                else 
                    return RedirectToAction("Create", "Beneficiaries");
            }

            return View(newMember);
        }

        // GET: NewMembers/Edit/5
        public ActionResult Edit(string id)
        {
            var planList = new List<SelectListItem>();
            var PlanQuery = from e in db.PolicyPlans select e;
            foreach (var m in PlanQuery)
            {
                planList.Add(new SelectListItem { Value = m.policyType, Text = m.policyType });
            }
            ViewBag.plplanlist = planList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMember newMember = db.NewMembers.Find(id);
            if (newMember == null)
            {
                return HttpNotFound();
            }
            return View(newMember);
        }

        // POST: NewMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "policyNo,title,fName,lName,IdNo,dOb,age,gender,maritalStat,telNo,cellNo,CustEmail,fascimileNo,physicalAddress,postalAddress,dateAdded,Policyplan,Premium,Category,addDep,paying")] NewMember newMember)
        {
            if (ModelState.IsValid)
            {
                newMember.Premium = 252;
                newMember.dateAdded = DateTime.Now;
                db.Entry(newMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newMember);
        }

        // GET: NewMembers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMember newMember = db.NewMembers.Find(id);
            if (newMember == null)
            {
                return HttpNotFound();
            }
            Session["PolicyNo"] = newMember.policyNo;
            Session["title"] = newMember.title;
            Session["fName"] = newMember.fName;
            Session["lName"] = newMember.lName;
            Session["IdNo"] = newMember.IdNo;
            Session["dOb"] = newMember.dOb;
            Session["age"] = newMember.age;
            Session["gender"] = newMember.gender;
            Session["maritalStat"] = newMember.maritalStat;
            Session["telNo"] = newMember.telNo;
            Session["cellNo"] = newMember.cellNo;
            Session["CustEmail"] = newMember.CustEmail;
            Session["fascimileNo"] = newMember.fascimileNo;
            Session["physicalAddress"] = newMember.physicalAddress;
            Session["postalAddress"] = newMember.postalAddress;
            Session["dateAdded"] = newMember.dateAdded;
            Session["Policyplan"] = newMember.Policyplan;
            Session["Premium"] = newMember.Premium;
            Session["Category"] = newMember.Category;
            return View(newMember);

        }

        // POST: NewMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        { 
            NewMember newMember = db.NewMembers.Find(id);
            //Payer payer = db.Payers.ToList().Find(p => p.policyNo == id);
            //Dependant dependant = db.Dependants.ToList().Find(p => p.policyNo == id);
            //Beneficiary beneficiary = db.Beneficiaries.ToList().Find(p => p.policyNo == id);
            //DebitOrderAuthorization debit = db.DebitOrderAuthorizations.ToList().Find(p => p.policyNo == id);
            //if (payer != null)
            //{
                db.Payers.Where(p => p.policyNo == id).ToList().ForEach(p => db.Payers.Remove(p));
                db.SaveChanges();
            //}

            //if (dependant != null)
            //{
                db.Dependants.Where(p => p.policyNo == id).ToList().ForEach(p => db.Dependants.Remove(p));
                db.SaveChanges();
                //db.Dependants.Remove(dependant);
            //}
            //if (beneficiary != null)
            //{
                db.Beneficiaries.Where(p => p.policyNo == id).ToList().ForEach(p => db.Beneficiaries.Remove(p));
                db.SaveChanges();
                //db.Beneficiaries.Remove(beneficiary);
            //}
            //if (debit != null)
            //{
                db.DebitOrderAuthorizations.Where(p => p.policyNo == id).ToList().ForEach(p => db.DebitOrderAuthorizations.Remove(p));
                db.SaveChanges();
                //db.DebitOrderAuthorizations.Remove(debit);
            //}
            db.NewMembers.Remove(newMember);
            db.SaveChanges();
            return RedirectToAction("Create", "ArchivedMembers");
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
