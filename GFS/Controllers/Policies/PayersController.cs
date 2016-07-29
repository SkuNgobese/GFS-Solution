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
    public class PayersController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Payers
        public ActionResult Index(string searchBy, string search)
        {
            var pay = from d in db.Payers.ToList()
                        select d;
            if (searchBy == "policyNo")
            {
                pay = (db.Payers.Where(x => x.policyNo == search).ToList());
            }
            else if (searchBy == "fName")
            {
                pay = (db.Payers.Where(x => x.firstName == search).ToList());
            }
            else if (searchBy == "idnumber")
            {
                pay = (db.Payers.Where(x => x.idNo == search).ToList());
            }
            else if (searchBy == "coveredby")
            {
                pay = (db.Payers.Where(x => x.payingFor == search).ToList());
            }
            return View(pay);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payer payer = db.Payers.Find(id);
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // GET: Payers/Create
        public ActionResult Create()
        {
            

            var relList = new List<SelectListItem>();
            var reQuery = from e in db.Relations select e;
            foreach (var m in reQuery)
            {
                relList.Add(new SelectListItem { Value = m.relationsh, Text = m.relationsh });
            }
            ViewBag.relist = relList;

            
            ViewBag.policyNo = new SelectList(db.NewMembers, "policyNo", "title");
            if ((Session["prem"]) != null && Session["PolicyNo"] != null)
            {
                double addamount = 0;
                var pol = Session["PolicyNo"].ToString();
                var dep = db.Dependants.ToList().FindAll(p => p.policyNo == pol);
                foreach (Dependant d in dep)
                {
                    addamount += d.amount;
                    double s=Convert.ToDouble(Session["prem"]);
                    Session["addamount"] = addamount+s;
                }
            }
            return View();
        }

        // POST: Payers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "payerNo,payingFor,paymentType,initialPremium,firstName,lastName,idNo,relation,contactNo,payerEmail,policyNo")] Payer payer)
        {
            //if (ModelState.IsValid)
            //{
            if (Session["owner"] != null)
            {
                payer.payingFor = (Session["owner"]).ToString();
            }
            if (Session["fname"]!=null)
                {
                    payer.firstName = Session["fname"].ToString();
                }
                if (Session["lname"] != null)
                {
                    payer.lastName = Session["lname"].ToString();
                }
                if (Session["idnum"] != null)
                {
                    payer.idNo = Session["idnum"].ToString();
                }
                if (Session["pay"] != null)
                {
                    payer.relation = Session["pay"].ToString();
                }
                if(Session["contact"]!=null)
                {
                    payer.contactNo = Session["contact"].ToString();
                }
                if(Session["email"]!=null)
                {
                    payer.payerEmail = Session["email"].ToString();
                }
                if (Session["PolicyNo"]!=null)
                {
                    payer.policyNo = (Session["PolicyNo"]).ToString();
                }
                if (Session["addamount"] != null)
                {
                    payer.initialPremium = Convert.ToDouble(Session["addamount"]);
                }
                else if (Session["addamount"] == null)
                {
                    payer.initialPremium = Convert.ToDouble(Session["prem"]);
                }
                db.Payers.Add(payer);
                db.SaveChanges();
                Session["payerNo"] = payer.payerNo;
                if(payer!=null)
                {
                    Session["Payer"] = payer;
                }
            if (payer.paymentType == "Debit Order")
            {
                return RedirectToAction("Create", "DebitOrderAuthorizations");
            }
            else
                ModelState.Clear(); 
            //}
            return RedirectToAction("Customer_Details", "NewMembers", new { id = payer.policyNo });
        }

        // GET: Payers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payer payer = db.Payers.Find(id);
            if (payer == null)
            {
                return HttpNotFound();
            }
            ViewBag.policyNo = new SelectList(db.NewMembers, "policyNo", "title", payer.policyNo);
            return View(payer);
        }

        // POST: Payers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "payerNo,payingFor,paymentType,initialPremium,firstName,lastName,idNo,relation,contactNo,payerEmail,policyNo")] Payer payer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.policyNo = new SelectList(db.NewMembers, "policyNo", "title", payer.policyNo);
            return View(payer);
        }

        // GET: Payers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payer payer = db.Payers.Find(id);
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // POST: Payers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payer payer = db.Payers.Find(id);
            db.Payers.Remove(payer);
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
