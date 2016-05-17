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
    public class BeneficiariesController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Beneficiaries
        public ActionResult Index()
        {
            var beneficiaries = db.Beneficiaries.Include(b => b.NewMembers);
            return View(beneficiaries.ToList());
        }

        // GET: Beneficiaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // GET: Beneficiaries/Create
        public ActionResult Create()
        {
            var relList = new List<SelectListItem>();
            var DirQuery = from e in db.Relations select e;
            foreach (var m in DirQuery)
            {
                relList.Add(new SelectListItem { Value = m.relationsh, Text = m.relationsh });
            }
            ViewBag.relist = relList;
            return View();
        }

        // POST: Beneficiaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "beneficiaryNo,coveredby,idNo,firstName,lastName,relation,split,policyPlan,addAnotherBen,policyNo")] Beneficiary beneficiary)
        {
            //if (ModelState.IsValid)
            //{
            if (Session["owner"]!=null)
            {
                beneficiary.coveredby = (Session["owner"]).ToString();
            }
            if (Session["finame"]!=null)
            {
                beneficiary.firstName = (Session["finame"]).ToString();
            }
            if (Session["laname"]!=null)
            {
                beneficiary.lastName = (Session["laname"]).ToString();
            }
            if (Session["Id"]!=null)
            {
                beneficiary.idNo = (Session["Id"]).ToString();
            }
            if (Session["relation"]!=null)
            {
                beneficiary.relation = (Session["relation"]).ToString();
            }               
                beneficiary.policyPlan = Session["polplan"].ToString();
                beneficiary.policyNo = (Session["PolicyNo"]).ToString();
                db.Beneficiaries.Add(beneficiary);
                db.SaveChanges();
                Session["finame"] = null;
                Session["laname"] = null;
                Session["Id"] = null;
                Session["relation"] = null;
                if (beneficiary.addAnotherBen == true)
                {
                    Session["PolicyNo"] = beneficiary.policyNo;
                }
                if (beneficiary != null)
                {
                    Session["Ben"] = beneficiary;
                }
                if (beneficiary.addAnotherBen == true)
                {
                    return RedirectToAction("Create", "Beneficiaries");
                }
                else if (beneficiary.addAnotherBen == true)
                {
                    return RedirectToAction("Create", "Payers");
                }
                    
            //}

              return RedirectToAction("Create", "Payers");
        }

        // GET: Beneficiaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // POST: Beneficiaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "beneficiaryNo,coveredby,idNo,firstName,lastName,relation,split,policyPlan,addAnotherBen,policyNo")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beneficiary);
        }

        // GET: Beneficiaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // POST: Beneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            db.Beneficiaries.Remove(beneficiary);
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
