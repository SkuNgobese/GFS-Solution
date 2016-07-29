using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GFS.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using Font = iTextSharp.text.Font;
using System.IO;
using System.Web.Helpers;
using GFS.Models.Policies;

namespace GFS.Controllers
{
    public class PaymentsController : Controller
    {
        private GFSContext db = new GFSContext();
        
        // GET: Payments
        public ActionResult Index(string searchString, string startdate, string enddate,string criteria,Payment paym)
        {
            var pay = from m in db.Payments.ToList()
                        select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                pay = pay.Where(s => s.policyNo.Contains(searchString));
            }
            else if (startdate != null && enddate != null)
            {
                pay = (from p in pay
                       where p.datePayed > Convert.ToDateTime(startdate) && p.datePayed <= Convert.ToDateTime(enddate)
                       select p).ToList();
            }
            if(criteria=="Over 60 Days")
            {
                DateTime start = paym.datePayed;
                DateTime end = DateTime.Now;
                int days = start.Subtract(end).Days;
                pay = from i in pay
                        where days > 2
                        select i;               
            }
            else if(criteria=="Over 30 Days")
            {
                DateTime start = paym.datePayed;
                DateTime end = DateTime.Now;
                int days = start.Subtract(end).Days;
                pay = from i in pay
                        where days <= 2
                        select i;
                
            }
            return View(pay);
        }
        public FileStreamResult PrintList(int? id)
        {
            //Set up the document and the MS to write it to and create the PDF writer instance
            MemoryStream memStream = new MemoryStream();
            // Create a Document object
            var document = new Document(PageSize.A4, 0, 0, 0, 0);

            // Create a new PdfWriter object, specifying the output stream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            //Set up fonts used in the document
            Font fontHeading3 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD + Font.UNDERLINE, BaseColor.BLACK);
            Font subHeaderFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLDITALIC, BaseColor.BLACK);
            Font PolicyDetailFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK);
            Font fontBody = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 10, Font.BOLD, BaseColor.BLACK);
            Font thFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK);
            Font redFont = FontFactory.GetFont(FontFactory.COURIER_BOLDOBLIQUE, 18, Font.BOLD, BaseColor.RED);
            Font fontData = FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 12, BaseColor.BLACK);


            //Open the PDF document
            document.Open();

            PdfPTable tblHeader = new PdfPTable(3)
            {
                SpacingBefore = 50f,
                SpacingAfter = 50f
            };

            PdfPCell hCell6 = new PdfPCell(new Phrase("\nGumbi Financial Services\n", fontHeading3))
            {
                HorizontalAlignment = 1,
                Colspan = 3,
                Border = 0
            };

            PdfPCell hCell7 = new PdfPCell(new Phrase("Gumbi Financial Services, Durban, South Africa", PolicyDetailFont))
            {
                Border = 0,
                Colspan = 3,
                HorizontalAlignment = 1
            };

            PdfPCell hCell8 = new PdfPCell(new Phrase("Tel: 086-111-1790 / Fax: 031-459-7600", PolicyDetailFont))
            {
                Border = 0,
                Colspan = 3,
                HorizontalAlignment = 1
            };
            PdfPCell hCell9 = new PdfPCell(new Phrase("Email :info@gumbifs.co.za", PolicyDetailFont))
            {
                Colspan = 3,
                Border = 0,
                HorizontalAlignment = 1
            };



            tblHeader.AddCell(hCell6);
            tblHeader.AddCell(hCell7);
            tblHeader.AddCell(hCell8);
            tblHeader.AddCell(hCell9);

            document.Add(tblHeader);

            //Get individual Payment Details for a Member
            //var obj = db.Payments.ToList().FindLast(x => x.policyNo == Session["det"].ToString());
            Payment obj = db.Payments.Find(id);

            var orderInfoTable = new PdfPTable(1);
            orderInfoTable.HorizontalAlignment = Element.ALIGN_CENTER;
            orderInfoTable.SpacingBefore = 40;
            orderInfoTable.SpacingAfter = 50;
            orderInfoTable.DefaultCell.Border = 0;
            orderInfoTable.WidthPercentage = 80;



            PdfPCell tc1 = new PdfPCell(new Phrase("Policy No: \t" + obj.policyNo, fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc1);
            PdfPCell tc2 = new PdfPCell(new Phrase("Customer Name: \t" + obj.CustomerName, fontData)) { Indent = 5, Border = 0 };
            orderInfoTable.AddCell(tc2);
            PdfPCell tc3 = new PdfPCell(new Phrase("Policy Plan: \t" + obj.plan, fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc3);
            PdfPCell tc4 = new PdfPCell(new Phrase("Amount Due: \t" + obj.dueAmount.ToString("R0.00"), fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc4);
            PdfPCell tc5 = new PdfPCell(new Phrase("Amount Paid: \t" + obj.amount.ToString("R0.00"), fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc5);
            PdfPCell tc6 = new PdfPCell(new Phrase("Outstanding Amount: \t" + obj.outstandingAmount.ToString("R0.00"), fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc6);
            PdfPCell tc7 = new PdfPCell(new Phrase("Date: \t" + obj.datePayed, fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc7);
            PdfPCell tc8 = new PdfPCell(new Phrase("Captured By: \t" + obj.cashierName, fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc8);
            PdfPCell tc9 = new PdfPCell(new Phrase("Branch: \t" + obj.branch, fontData)) { Indent = 5, Border = 0, Colspan = 2, };
            orderInfoTable.AddCell(tc9);



            var paragraph = new Paragraph("Customer Receipt", redFont);
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);
            document.Add(orderInfoTable);

            Paragraph gap = new Paragraph("\n\n");

            document.Add(gap);

            PdfPTable tblOfficeDetail = new PdfPTable(2);
            PdfPCell stampCell = new PdfPCell(new Phrase("Stamp  :.......................................", fontBody)) { Rowspan = 2, HorizontalAlignment = 3, Border = 0 };
            tblOfficeDetail.AddCell(stampCell);

            document.Add(tblOfficeDetail);
            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logo.png"));
            logo.Alignment = Element.ALIGN_CENTER; // Absolute position
            document.Add(logo);
            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", obj.policyNo));
            Response.BinaryWrite(output.ToArray());
            return new FileStreamResult(memStream, "application/pdf");
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
            Session["responce3"] = null;
            Session["polNo"] = null;
            Session["fullname"] = null;
            Session["plan"] = null;
            Session["iniPrem"] = null;
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
                var fee = db.JoiningFees.ToList().Find(p => p.policyNo == searchStr);
                if (d!=null)
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
                    if (fee != null)
                    {
                        Session["iniPrem"] = (du.initialPremium + stand.outstandingAmount);
                    }
                    else if (fee == null)
                    {
                        Session["iniPrem"] = (du.initialPremium + stand.outstandingAmount) + 50.00;
                    }

                }
                else if(stand==null)
                {
                    if (fee != null)
                    {
                        Session["iniPrem"] = (du.initialPremium);
                    }
                    else if (fee == null)
                    {
                        Session["iniPrem"] = du.initialPremium + 50.00;
                    }
                }
                
                return RedirectToAction("Create");
            }
            return View(payment);
        }
        

        // GET: Payments/Create
        public ActionResult Create()
        {
            var planList = new List<SelectListItem>();
            var PlanQuery = from e in db.Plan_Type select e;
            foreach (var m in PlanQuery)
            {
                planList.Add(new SelectListItem { Value = m.plan, Text = m.plan });
            }
            ViewBag.plnlist = planList;
            var branchList = new List<SelectListItem>();
            var branQuery = from e in db.Branches select e;
            foreach (var m in branQuery)
            {
                branchList.Add(new SelectListItem { Value = m.branchN, Text = m.branchN });
            }
            ViewBag.branlst = branchList;          
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "referenceNo,policyNo,CustomerName,plan,dueAmount,amount,outstandingAmount,datePayed,cashierName,branch,emailSlip")] Payment payment)
        {
            double outst = 0.00;
            if (Session["fullname"]==null)
            {
                var pol = db.NewMembers.ToList().Find(r => r.policyNo == payment.policyNo);
                if (pol == null)
                {
                    TempData["responce15"] = "Sorry, Member you making payment for does not exist in the database! please add the Member first.";
                    return RedirectToAction("Create");
                }
            }                
            if (Session["polNo"]!=null)
            {
                payment.policyNo = Session["polNo"].ToString();
            }
            var fee = db.JoiningFees.ToList().Find(p => p.policyNo == payment.policyNo);

            if (Session["fullname"]!=null)
            {
                payment.CustomerName = Session["fullname"].ToString();
            }
            else if (Session["fullname"] == null)
            {
                var d = db.NewMembers.ToList().Find(r => r.policyNo == payment.policyNo);
                payment.CustomerName = d.fName + " " + d.lName;
            }
            if (Session["plan"]!=null)
            {
                payment.plan = Session["plan"].ToString();
            }
            else if (Session["plan"] == null)
            {
                var d = db.NewMembers.ToList().Find(r => r.policyNo == payment.policyNo);
                payment.plan = d.Policyplan;
            }
            if (Session["iniPrem"] != null)
            {
                payment.dueAmount = Convert.ToDouble(Session["iniPrem"]);
                if (fee == null && payment.amount > 50.00)
                {
                    JoiningFee j = new JoiningFee();
                    j.policyNo = payment.policyNo;
                    j.CustomerName = payment.CustomerName;
                    j.AmountRendered = payment.amount;
                    j.change = payment.amount - 50.00;
                    j.Fee = 50.00;
                    j.date = DateTime.Now;
                    j.cashierName = User.Identity.Name;
                    j.branch = payment.branch;

                    db.JoiningFees.Add(j);
                    db.SaveChanges();

                    payment.amount = payment.amount - 50;
                    outst = payment.dueAmount - (payment.amount+50);
                    payment.outstandingAmount = outst;
                }
                else if(fee!=null)
                {
                    outst = payment.dueAmount - payment.amount;
                    payment.outstandingAmount = outst;
                }
            }
            else if (Session["iniPrem"] == null)
            {
                var du = db.Payers.ToList().Find(r => r.policyNo == payment.policyNo);
                var stand = db.Payments.ToList().FindLast(r => r.policyNo == payment.policyNo);
                    
                if (fee == null && payment.amount > 50.00)
                {
                    payment.dueAmount = du.initialPremium + stand.outstandingAmount+50.00;
                    JoiningFee j = new JoiningFee();
                    j.policyNo = payment.policyNo;
                    j.CustomerName = payment.CustomerName;
                    j.AmountRendered = payment.amount;
                    j.change = payment.amount - 50.00;
                    j.Fee = 50.00;
                    j.date = DateTime.Now;
                    j.cashierName = User.Identity.Name;
                    j.branch = payment.branch;

                    db.JoiningFees.Add(j);
                    db.SaveChanges();

                    payment.amount = payment.amount - 50;
                    outst = payment.dueAmount - (payment.amount + 50);
                    payment.outstandingAmount = outst;
                }
                else if (fee != null)
                {
                    payment.dueAmount = du.initialPremium + stand.outstandingAmount;
                    outst = payment.dueAmount - payment.amount;
                    payment.outstandingAmount = outst;
                }
            }
                
            payment.datePayed = DateTime.Now;
            payment.cashierName = User.Identity.Name;                
            db.Payments.Add(payment);
            db.SaveChanges();
            Session["iniPrem"] = null;
            Session["det"] = payment.policyNo;
            var update = db.NewMembers.ToList().Find(p=>p.policyNo==payment.policyNo);
            if(payment.amount>=payment.dueAmount)
            {
                if (update != null)
                {
                    if (update.Active == false)
                    {
                        update.Active = true;
                        db.SaveChanges();
                    }
                }
            }
            
            if (payment.emailSlip==true)
            {
                try
                {
                    var emailA = db.Payers.ToList().Find(p => p.policyNo == payment.policyNo);
                    var boddy = new StringBuilder();

                    boddy.Append("Dear " + payment.CustomerName + "<br/>" + "<br/>"+
                                    "Thank You For Being Gumbi Financial Services' Customer" + "<br/>" +
                                    "You Just made a payment with the following details:-" + "<br/>" +
                                    "Policy Number: " + payment.policyNo + "<br/>" +
                                    "Policy Plan: " + payment.plan + "<br/>" +
                                    "Amount That Was Due: R" + payment.dueAmount + "<br/>" +
                                    "Amount Paid: R" + payment.amount + "<br/>" +
                                    "Outstanding Amount: R" + payment.outstandingAmount + "<br/>" +
                                    "Date Paid: " + payment.datePayed + "<br/>" +
                                    "Your Cashier Was: " + payment.cashierName + "<br/>" +
                                    "Branch: " + payment.branch + "<br/>" + "<br/>" +
                                    "G.F.S Burial Society, Committed with Excellence." + "<br/>" +
                                    "==========================================" + "<br/>");

                    string body_for = boddy.ToString();
                    string to_for = "";
                    if(emailA!=null && emailA.payerEmail!=null)
                    {
                        to_for = emailA.payerEmail;
                    }                       
                    string subject_for = "G.F.S Payment On "+DateTime.Now.ToLongDateString();

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
