using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GFS.Models;

using System.Web.Helpers;
using System.Text;
using System.Net.Mail;

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
                var stand = db.Payments.ToList().FindLast(r => r.policyNo == searchStr);
                if(d!=null)
                {
                    Session["polNo"] = d.policyNo;
                    Session["fullname"] = d.fName + " " + d.lName;
                    Session["plan"] = d.Policyplan;
                }
                else if(d==null)
                {
                    Session["responce3"] = "Sorry, Member you searched for does not exist in the database! please add the Member first.";
                    return View("Search");
                }
                if(stand!=null)
                {
                    Session["iniPrem"] = du.initialPremium + stand.outstandingAmount;
                }
                else if(stand==null)
                {
                    Session["iniPrem"] = du.initialPremium;
                }
                
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
        public ActionResult Create([Bind(Include = "referenceNo,policyNo,CustomerName,plan,dueAmount,amount,outstandingAmount,datePayed,cashierName,branch,emailSlip")] Payment payment)
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
                
                double outst = payment.dueAmount - payment.amount;
                payment.outstandingAmount = outst;
                payment.datePayed = DateTime.Now;

                db.Payments.Add(payment);
                db.SaveChanges();
                if(payment.emailSlip==true)
                {
                    try
                    {
                        var emailA = db.NewMembers.ToList().Find(p => p.policyNo == payment.policyNo);
                        var boddy = new StringBuilder();

                        boddy.Append("Dear " + payment.CustomerName + "<br/>" +
                                     "Thank You For Being G.F.S Customer" + "<br/>" +
                                     "You Just made a payment with the following details" +
                                      "Policy Number: " + payment.policyNo +
                                      "Policy Plan: " + payment.plan +
                                      "Amount That Was Due: R" + payment.dueAmount +
                                      "Amount Paid: R" + payment.amount +
                                      "Outstanding Amount: R" + payment.outstandingAmount +
                                      "Date Paid: " + payment.datePayed +
                                      "Your Cashier Was: " + payment.cashierName +
                                      "Branch: " + payment.branch + "<br/>" +
                                     "your satisfaction with our service is our priority" + "<br/>" +
                                     "==========================================" + "<br/>");

                        string body_for = boddy.ToString();
                        string to_for = "";
                        if(emailA!=null)
                        {
                            to_for = emailA.CustEmail;
                        }                       
                        string subject_for = "G.F.S Payment for "+DateTime.Now.Month.ToString();

                        WebMail.SmtpServer = "pod51014.outlook.com";
                        WebMail.SmtpPort = 587;

                        WebMail.UserName = "21353863@dut4life.ac.za";
                        WebMail.Password = "Dut930717";

                        WebMail.From = "21353863@dut4life.ac.za";
                        WebMail.EnableSsl = true;
                        WebMail.Send(to: to_for, subject: subject_for, body: body_for);
                    }
                    catch (Exception)
                    {
                        
                    }
                } 
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
        public ActionResult Edit([Bind(Include = "referenceNo,policyNo,CustomerName,plan,dueAmount,amount,outstandingAmount,datePayed,cashierName,branch,emailSlip")] Payment payment)
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
