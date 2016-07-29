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
    public class ArchivedMembersController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: ArchivedMembers
        public ActionResult Index()
        {
            return View(db.ArchivedMembers.ToList());
        }

        // GET: ArchivedMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivedMember archivedMember = db.ArchivedMembers.Find(id);
            if (archivedMember == null)
            {
                return HttpNotFound();
            }
            return View(archivedMember);
        }

        // GET: ArchivedMembers/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: ArchivedMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "policyNo,title,fName,lName,IdNo,dOb,age,gender,maritalStat,telNo,cellNo,CustEmail,fascimileNo,physicalAddress,postalAddress,dateAdded,Policyplan,Premium,Category,dateArchived,reason")] ArchivedMember archivedMember)
        {
            ArchivedMember polNo = db.ArchivedMembers.ToList().Find(x => x.policyNo == archivedMember.policyNo);
            ArchivedMember idno = db.ArchivedMembers.ToList().Find(x => x.IdNo == archivedMember.IdNo);
            if (polNo != null)
            {

                Session["responce1"] = "Member already archived, Check The Policy Number!";
                return RedirectToAction("Create");
            }
            if (idno != null)
            {
                Session["responce1"] = "Member already archived, Check ID Number!";
                return RedirectToAction("Create");
            }

            //else if(ModelState.IsValid)
            //{
                archivedMember.policyNo = Session["PolicyNo"].ToString();
                archivedMember.title = Session["title"].ToString();
                archivedMember.fName = Session["fName"].ToString();
                archivedMember.lName = Session["lName"].ToString();
                archivedMember.IdNo = Session["IdNo"].ToString();
                archivedMember.dOb = Convert.ToDateTime(Session["dOb"].ToString());
                //archivedMember.age = Convert.ToInt32(Session["age"].ToString());
                archivedMember.gender = Session["gender"].ToString();
                archivedMember.maritalStat = Session["maritalStat"].ToString();
                archivedMember.telNo = Session["telNo"].ToString();
                archivedMember.cellNo = Session["cellNo"].ToString();
                archivedMember.CustEmail = Session["CustEmail"].ToString();
                archivedMember.fascimileNo = Session["fascimileNo"].ToString();
                archivedMember.physicalAddress = Session["physicalAddress"].ToString();
                archivedMember.postalAddress = Session["postalAddress"].ToString();
                archivedMember.dateAdded = Convert.ToDateTime(Session["dateAdded"].ToString());
                archivedMember.Policyplan = Session["Policyplan"].ToString();
                archivedMember.Premium = Convert.ToDouble(Session["Premium"].ToString());
                archivedMember.Category = Session["Category"].ToString();
                archivedMember.dateArchived = DateTime.Now;

                db.ArchivedMembers.Add(archivedMember);
                db.SaveChanges();
                

            //}
            return RedirectToAction("Index");
                     
        }

        // GET: ArchivedMembers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivedMember archivedMember = db.ArchivedMembers.Find(id);
            if (archivedMember == null)
            {
                return HttpNotFound();
            }
            return View(archivedMember);
        }

        // POST: ArchivedMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "policyNo,title,fName,lName,IdNo,dOb,age,gender,maritalStat,telNo,cellNo,CustEmail,fascimileNo,physicalAddress,postalAddress,dateAdded,Policyplan,Premium,Category,dateArchived,reason")] ArchivedMember archivedMember)
        {
            if (ModelState.IsValid)
            {
                archivedMember.dateArchived = DateTime.Now;
                db.Entry(archivedMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(archivedMember);
        }

        // GET: ArchivedMembers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivedMember archivedMember = db.ArchivedMembers.Find(id);
            if (archivedMember == null)
            {
                return HttpNotFound();
            }
            return View(archivedMember);
        }

        // POST: ArchivedMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            
            ArchivedMember archivedMember = db.ArchivedMembers.Find(id);
            //int years = (DateTime.Now.Year) - (archivedMember.dateArchived.Year);
            //if (years > 5)
            //{
                db.ArchivedMembers.Remove(archivedMember);
                db.SaveChanges();
            //}
            //else
                //Session["responce2"] = "Cannot delete a record with less than 5 years in database!";
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
