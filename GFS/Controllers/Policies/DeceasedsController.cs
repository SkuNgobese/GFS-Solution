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
    public class DeceasedsController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Deceaseds
        public ActionResult Index()
        {
            return View(db.Deceaseds.ToList());
        }

        // GET: Deceaseds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deceased deceased = db.Deceaseds.Find(id);
            if (deceased == null)
            {
                return HttpNotFound();
            }
            return View(deceased);
        }

        // GET: Deceaseds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deceaseds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deceasedNo,firstName,lastName,idNo,age,gender,causeOfDeath,DateOfDeath,policyNo")] Deceased deceased)
        {
            //if (ModelState.IsValid)
            //{
                deceased.firstName = Session["First Name"].ToString();
                deceased.lastName = Session["Last Name"].ToString();
                deceased.idNo = Session["ID Number"].ToString();
                deceased.age = Convert.ToInt32(Session["Age"].ToString());
                deceased.gender = Session["Gender"].ToString();
                deceased.policyNo = Convert.ToInt32(Session["PolicyNo"].ToString());

                db.Deceaseds.Add(deceased);
                db.SaveChanges();
                Session.RemoveAll();
                ModelState.Clear();
                return RedirectToAction("Index");
            //}

            //return View(deceased);
        }

        // GET: Deceaseds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deceased deceased = db.Deceaseds.Find(id);
            if (deceased == null)
            {
                return HttpNotFound();
            }
            return View(deceased);
        }

        // POST: Deceaseds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deceasedNo,firstName,lastName,idNo,age,gender,causeOfDeath,DateOfDeath,policyNo")] Deceased deceased)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deceased).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deceased);
        }

        // GET: Deceaseds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deceased deceased = db.Deceaseds.Find(id);
            if (deceased == null)
            {
                return HttpNotFound();
            }
            return View(deceased);
        }

        // POST: Deceaseds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deceased deceased = db.Deceaseds.Find(id);
            db.Deceaseds.Remove(deceased);
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
