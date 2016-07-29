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
using iTextSharp;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Owin;
using System.Web.Helpers;
using iTextSharp.text.pdf.draw;
using System.Text;
using System.Drawing;
using iTextSharp.text.html.simpleparser;
using Font = iTextSharp.text.Font;
using System.Globalization;

namespace GFS.Controllers.Policies
{
    public class NewMembersController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: NewMembers
        public ActionResult Index(string searchBy, string search)
        {

            int countPrincipals = (from x in db.NewMembers
                                    select x).Count();
            ViewBag.AllPrincipals = countPrincipals;

            int countActivePrincipals = (from x in db.NewMembers
                                       where x.Active == true
                                       select x).Count();
            ViewBag.ActivePrincipals = countActivePrincipals;

            var percentActive = (double)countActivePrincipals / (double)countPrincipals * 100;
            ViewBag.percentActive = percentActive;
            var member = from m in db.NewMembers.ToList()
                             select m;
            if (search != null)
            {
                if (searchBy == "policyNo")
                {
                    member = member.Where(r => r.policyNo == search);
                    if(member==null)
                    {
                        Session["response10"] = "Record not found!";
                    }
                    
                }

                if (searchBy == "IdNo")
                {
                    member = member.Where(x => x.IdNo == search);
                }

                if (searchBy == "nameorsurn")
                {
                    member = (from n in member
                              where n.fName.Contains(search) || n.lName.Contains(search)
                              select n).ToList();
                }
                return View(member);
            }
            else

            return View(member);

        }
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
            var PlanQuery = from e in db.Plan_Type orderby e.plan select e;
            foreach (var m in PlanQuery)
            {
                planList.Add(new SelectListItem { Value = m.plan, Text = m.plan });
            }
            ViewBag.polplanlist = planList;
            var branchList = new List<SelectListItem>();
            var branQuery = from e in db.Branches orderby e.branchN select e;
            foreach (var m in branQuery)
            {
                branchList.Add(new SelectListItem { Value = m.branchN, Text = m.branchN });
            }
            ViewBag.branlist = branchList;
            var salesP = new List<SelectListItem>();
            var slpQuery = from e in db.SalesPersons orderby e.lName select e;
            foreach (var m in slpQuery)
            {
                salesP.Add(new SelectListItem { Value = m.fName+" "+m.lName, Text = m.fName + " " + m.lName });
            }
            ViewBag.slplist = salesP;
            Session["fname"] = null;
            Session["lname"] = null;
            Session["idnum"] = null;
            Session["pay"] = null;
            Session["contact"] = null;
            Session["email"] = null;


            return View();
        }

        // POST: NewMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "policyNo,title,fName,lName,IdNo,dOb,gender,maritalStat,telNo,cellNo,CustEmail,fascimileNo,physicalAddress,postalAddress,dateAdded,Policyplan,Premium,Category,Branch,SalesPerson,capturedby,addDep,paying,Active")] NewMember newMember)
        {
            
            NewMember polNo = db.NewMembers.ToList().Find(x => x.policyNo == newMember.policyNo);
            NewMember idno = db.NewMembers.ToList().Find(x => x.IdNo == newMember.IdNo);
            
            if (polNo != null)
            {
                TempData["responce"] = "Member Exists, Check The Policy Number! This one is Assigned to: " + polNo.fName + " " + polNo.lName;
                return RedirectToAction("Create");
            }
            if (idno != null)
            {
                TempData["responce"] = "Member Exists, Check ID Number! This one Belongs to: "+idno.fName+" "+idno.lName;
                return RedirectToAction("Create");
            }
            else if (ModelState.IsValid)
            {
                if (newMember.IdNo != null)
                {
                    int year = Convert.ToInt32(newMember.IdNo.Substring(0, 2));                    
                    int month = Convert.ToInt16(newMember.IdNo.Substring(2, 2));
                    int day = Convert.ToInt16(newMember.IdNo.Substring(4, 2));

                    var d= (year + "-" + month + "-" + day);
                    DateTime d1 = DateTime.Parse(d);
                    newMember.dOb = d1;
                }
                int age = new DateTime(DateTime.Now.Subtract(newMember.dOb).Ticks).Year - 1;
                if (newMember.Category == "Single Member")
                {
                    //var plan = db.PolicyPlans.Where(p => p == newMember.PolicyPlans);
                    if (newMember.Policyplan == "Plan A")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 90;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 170;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 320;
                        }
                        else if (age > 84)
                        {
                            TempData["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan B")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 65;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 125;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 200;
                        }
                        else if(age > 84)
                        {
                            newMember.Premium = 330;
                        }
                    }
                }
                if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan C1")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 50;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 93;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 131;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 218;
                        }
                    }
                }
                if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan C2")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 65;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 119;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 192;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 322;
                        }
                    }
                }
                if (newMember.Category == "Single Member")
                {
                    if (newMember.Policyplan == "Plan C3")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 83;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 155;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 252;
                        }
                        else if (age > 84)
                        {
                            TempData["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan A")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 110;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 200;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 386;
                        }
                        else if (age > 84)
                        {
                            TempData["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan B")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 90;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 160;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 260;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 400;
                        }
                    }
                }
                if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan C1")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 60;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 110;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 170;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 264;
                        }
                    }
                }
                if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan C2")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 85;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 160;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 242;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 391;
                        }
                    }
                }
                if (newMember.Category == "Single Parent")
                {
                    if (newMember.Policyplan == "Plan C3")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 110;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 210;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 319;
                        }
                        else if (age > 84)
                        {
                            TempData["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan A")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 120;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 220;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 530;
                        }
                        else if (age > 84)
                        {
                            TempData["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan B")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 100;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 180;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 350;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 500;
                        }
                    }
                }
                if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan C1")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 80;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 140;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 220;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 320;
                        }
                    }
                }
                if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan C2")
                    {
                        if (age <= 64)
                        {
                            newMember.Premium = 108;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 170;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 328;
                        }
                        else if (age > 84)
                        {
                            newMember.Premium = 475;
                        }
                    }
                }
                if (newMember.Category == "Immediate Family")
                {
                    if (newMember.Policyplan == "Plan C3")
                    {
                        var min = db.PolicyPlans.Where(p => p.policyType == newMember.Policyplan && p.category == newMember.Category);
                        if (age <= 64)
                        {
                            newMember.Premium = 140;
                        }
                        else if (age <= 74)
                        {
                            newMember.Premium = 200;
                        }
                        else if (age <= 84)
                        {
                            newMember.Premium = 434;
                        }
                        else if (age > 84)
                        {
                            TempData["responce"] = "Cannot add person over 84 years from this plan!";
                            ModelState.Clear();
                            return RedirectToAction("Create");
                        }
                    }
                }
                if(age<18)
                {
                    TempData["responce"] = "Cannot add person under the age of 18!";
                    ModelState.Clear();
                    return RedirectToAction("Create");
                }
                newMember.policyNo = "GFS" + newMember.policyNo;              
                newMember.dateAdded = DateTime.Now;
                newMember.capturedby = User.Identity.Name;
                newMember.Active = false;
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
                    if(newMember.cellNo!=null)
                    {
                        Session["contact"] = newMember.cellNo;
                    }
                    if(newMember.CustEmail!=null)
                    {
                        Session["email"] = newMember.CustEmail;
                    }                    
                }               
                if (newMember.Category == "Single Member")
                {
                    if (newMember.addDep == true)
                    {
                        return RedirectToAction("Create", "Beneficiaries");
                    }
                    else
                    return RedirectToAction("Create", "Beneficiaries");
                }
                else if (newMember.Category == "Immediate Family")
                {
                    return RedirectToAction("Create", "Dependants");
                }
                else if (newMember.addDep == true)
                {
                    return RedirectToAction("Create", "Dependants");
                }
                else 
                    return RedirectToAction("Create", "Beneficiaries");
            }
            return View(newMember);
        }

        public FileStreamResult Customer_Details(string id)
        {
            //Set up the document and the MS to write it to and create the PDF writer instance
            MemoryStream memStream = new MemoryStream();
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            PdfWriter writer = PdfWriter.GetInstance(document, memStream);
            writer.CloseStream = false;
            
            //Set up fonts used in the document
            Font fontHeading3 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD + Font.UNDERLINE, BaseColor.BLACK);
            Font subHeaderFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLDITALIC, BaseColor.BLACK);
            Font PolicyDetailFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK);
            Font fontBody = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD, BaseColor.BLACK);
            Font thFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD, BaseColor.BLACK);
            Font redFont = FontFactory.GetFont(FontFactory.COURIER_BOLDOBLIQUE, 18, Font.BOLD, BaseColor.BLACK);
            Font fontData = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, BaseColor.BLACK);

            //Open the PDF document\

            document.Open();
            Paragraph paraSubHeaderz = new Paragraph("    _____________" +
                                                    "____________________________________Customer Details_____________________________________________________", subHeaderFont);

            PdfPTable tblHeader = new PdfPTable(3)
            {
                SpacingBefore = 50f,
                SpacingAfter = 50f
            };

            PdfPCell hCell0 = new PdfPCell(new Phrase("\nGumbi Financial Services\n", fontHeading3))
            {
                HorizontalAlignment = 3,
                Colspan = 3,
                Border = 0,

            };

            PdfPCell hCell1 = new PdfPCell(new Phrase("Gumbi Financial Services, Durban, South Africa", PolicyDetailFont))
            {
                Border = 0,
                Colspan = 3,
                HorizontalAlignment = 3
            };

            PdfPCell hCell2 = new PdfPCell(new Phrase("Tel: 031-459-7500 / Fax: 031-459-7600", PolicyDetailFont))
            {
                Border = 0,
                Colspan = 3,
                HorizontalAlignment = 3
            };
            PdfPCell hCell3 = new PdfPCell(new Phrase("Email :info@gumbifs.com / Enquiry@gumbi.com", PolicyDetailFont))
            {
                Colspan = 3,
                Border = 0,
                HorizontalAlignment = 3
            };





            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Image/Logo Main 2.png"));
            logo.SetAbsolutePosition(355, 710);
            logo.IndentationRight = 100;
            logo.IndentationLeft = 100;
            document.Add(logo);

            tblHeader.AddCell(hCell0);
            tblHeader.AddCell(hCell1);
            tblHeader.AddCell(hCell2);
            tblHeader.AddCell(hCell3);


            document.Add(tblHeader);

            Paragraph space = new Paragraph("\n\n");

            document.Add(space);
            Paragraph gap = new Paragraph("\n");


            //To get one  ready for print
            var one = db.NewMembers.ToList().Find(x => x.policyNo == id);
            var dep = db.Dependants.ToList().FindAll(x => x.policyNo == one.policyNo);
            var ben = db.Beneficiaries.ToList().FindAll(x => x.policyNo == one.policyNo);
            var pay = db.Payers.ToList().Find(x => x.policyNo == one.policyNo);
            var debit = db.DebitOrderAuthorizations.ToList().Find(x => x.policyNo == one.policyNo);
            PdfContentByte cb = writer.DirectContent;
            PdfPTable tblContent = new PdfPTable(2);
            PdfPTable depTable = new PdfPTable(2);
            PdfPTable benTable = new PdfPTable(2);
            PdfPTable payTable = new PdfPTable(2);
            PdfPTable debitTable = new PdfPTable(2);

            if(one!=null)
            {
                var paragraph = new Paragraph("Holder Details", redFont);
                paragraph.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph);
                document.Add(gap);

                PdfPCell h1 = new PdfPCell(new Phrase("Policy Number    :" + one.policyNo, thFont)) { Colspan = 2, Border = 0, };
                tblContent.AddCell(h1);
                PdfPCell h2 = new PdfPCell(new Phrase("Title    :" + one.title, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h2);
                PdfPCell h3 = new PdfPCell(new Phrase("First Name    :" + one.fName, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h3);
                PdfPCell h4 = new PdfPCell(new Phrase("Last Name    :" + one.lName, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h4);
                PdfPCell h5 = new PdfPCell(new Phrase("ID Number    :" + one.IdNo, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h5);
                PdfPCell h6 = new PdfPCell(new Phrase("Date Of Birth    :" + one.dOb.ToShortDateString(), thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h6);
                //PdfPCell h7 = new PdfPCell(new Phrase("Age    :" + one.age, thFont)) { Colspan = 2, Border = 0 };
                //tblContent.AddCell(h7);
                PdfPCell h8 = new PdfPCell(new Phrase("Gender    :" + one.gender, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h8);
                PdfPCell h9 = new PdfPCell(new Phrase("Marital Status    :" + one.maritalStat, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h9);
                PdfPCell h10 = new PdfPCell(new Phrase("Telephone    :" + one.telNo, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h10);
                PdfPCell h11 = new PdfPCell(new Phrase("Cell Number    :" + one.cellNo, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h11);
                PdfPCell h12 = new PdfPCell(new Phrase("Customer Email    :" + one.CustEmail, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h12);
                PdfPCell h13 = new PdfPCell(new Phrase("Fascimile    :" + one.fascimileNo, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h13);
                PdfPCell h14 = new PdfPCell(new Phrase("Physical Address    :" + one.physicalAddress, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h14);
                PdfPCell h15 = new PdfPCell(new Phrase("Postal Address    :" + one.postalAddress, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h15);
                PdfPCell h16 = new PdfPCell(new Phrase("Date Added    :" + one.dateAdded, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h16);
                PdfPCell h17 = new PdfPCell(new Phrase("Policy Plan    :" + one.Policyplan, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h17);
                PdfPCell h18 = new PdfPCell(new Phrase("Premium    :" + one.Premium.ToString("R0.00"), thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h18);
                PdfPCell h19 = new PdfPCell(new Phrase("Category    :" + one.Category, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h19);
                //PdfPCell h20 = new PdfPCell(new Phrase("Sales Person    :" + one.SalesPerson, thFont)) { Colspan = 2, Border = 0 };
                //tblContent.AddCell(h20);
                PdfPCell h21 = new PdfPCell(new Phrase("Branch    :" + one.Branch, thFont)) { Colspan = 2, Border = 0 };
                tblContent.AddCell(h21);


                document.Add(tblContent);
                document.Add(gap);
            }
            

            if(dep!=null)
            {
                var paragraph1 = new Paragraph("Dependants Details", redFont);
                paragraph1.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph1);
                document.Add(gap);
                foreach (Dependant d in dep)
                {
                    PdfPCell d1 = new PdfPCell(new Phrase("First Name    :" + d.fName, thFont)) { Colspan = 2, Border = 0 };
                    depTable.AddCell(d1);
                    PdfPCell d2 = new PdfPCell(new Phrase("Last Name    :" + d.lName, thFont)) { Colspan = 2, Border = 0 };
                    depTable.AddCell(d2);
                    PdfPCell d3 = new PdfPCell(new Phrase("ID Number    :" + d.IdNo, thFont)) { Colspan = 2, Border = 0 };
                    depTable.AddCell(d3);
                    PdfPCell d4 = new PdfPCell(new Phrase("Date Of Birth    :" + d.dOb.ToShortDateString(), thFont)) { Colspan = 2, Border = 0 };
                    depTable.AddCell(d4);
                    PdfPCell d5 = new PdfPCell(new Phrase("Age    :" + d.age, thFont)) { Colspan = 2, Border = 0 };
                    depTable.AddCell(d5);
                    PdfPCell d6 = new PdfPCell(new Phrase("Relationship    :" + d.relationship, thFont)) { Colspan = 2, Border = 0 };
                    depTable.AddCell(d6);
                    PdfPCell d7 = new PdfPCell(new Phrase("Amount   :" + d.amount.ToString("R0.00"), thFont)) { Colspan = 2, Border = 0 };
                    depTable.AddCell(d7);
                }
                document.Add(depTable);
                document.Add(gap);
            }
            
            if(ben!=null)
            {
                var paragraph2 = new Paragraph("Beneficiaries Details", redFont);
                paragraph2.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph2);
                document.Add(gap);
                foreach (Beneficiary b in ben)
                {
                    PdfPCell b1 = new PdfPCell(new Phrase("First Name    :" + b.firstName, thFont)) { Colspan = 2, Border = 0 };
                    benTable.AddCell(b1);
                    PdfPCell b2 = new PdfPCell(new Phrase("Last Name    :" + b.lastName, thFont)) { Colspan = 2, Border = 0 };
                    benTable.AddCell(b2);
                    PdfPCell b3 = new PdfPCell(new Phrase("ID Number    :" + b.idNo, thFont)) { Colspan = 2, Border = 0 };
                    benTable.AddCell(b3);
                    PdfPCell b4 = new PdfPCell(new Phrase("Relationship   :" + b.relation, thFont)) { Colspan = 2, Border = 0 };
                    benTable.AddCell(b4);
                    PdfPCell b5 = new PdfPCell(new Phrase("Split    :" + b.split, thFont)) { Colspan = 2, Border = 0 };
                    benTable.AddCell(b5);                  
                }
                document.Add(benTable);
                document.Add(gap);
            }
            
            if(pay!=null)
            {
                var paragraph3 = new Paragraph("Payer's Details", redFont);
                paragraph3.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph3);
                document.Add(gap);

                PdfPCell p1 = new PdfPCell(new Phrase("First Name    :" + pay.firstName, thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p1);
                PdfPCell p2 = new PdfPCell(new Phrase("Last Name    :" + pay.lastName, thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p2);
                PdfPCell p3 = new PdfPCell(new Phrase("ID Number    :" + pay.idNo, thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p3);
                PdfPCell p4 = new PdfPCell(new Phrase("Relationship    :" + pay.relation, thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p4);
                PdfPCell p7 = new PdfPCell(new Phrase("Contact Number    :" + pay.contactNo, thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p7);
                PdfPCell p8 = new PdfPCell(new Phrase("Email Address    :" + pay.payerEmail, thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p8);
                PdfPCell p5 = new PdfPCell(new Phrase("Payment Type    :" + pay.paymentType, thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p5);
                PdfPCell p6 = new PdfPCell(new Phrase("Initial Premium   :" + pay.initialPremium.ToString("R0.00"), thFont)) { Colspan = 2, Border = 0 };
                payTable.AddCell(p6);

                document.Add(payTable);
                document.Add(gap);
            }

            if(debit!=null)
            {
                var paragraph4 = new Paragraph("Debit Order Authorization's Details", redFont);
                paragraph4.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph4);
                document.Add(gap);

                PdfPCell de1 = new PdfPCell(new Phrase("Bank Name    :" + debit.bankName, thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(de1);
                PdfPCell de2 = new PdfPCell(new Phrase("Account Number    :" + debit.accNo, thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(de2);
                PdfPCell de3 = new PdfPCell(new Phrase("Account Type    :" + debit.accountType, thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(de3);
                PdfPCell de5 = new PdfPCell(new Phrase("Branch Code    :" + debit.branchcode, thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(de5);
                PdfPCell de6 = new PdfPCell(new Phrase("Amount To Deduct   :" + debit.amount.ToString("R0.00"), thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(de6);
                PdfPCell de7 = new PdfPCell(new Phrase("Commence Date    :" + debit.commenceDate.ToLongDateString(), thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(de7);

                document.Add(debitTable);
                document.Add(gap);
            }

            if (pay.paymentType== "ATM Cash Deposit" || pay.paymentType=="EFT")
            {
                var paragraph5 = new Paragraph("GFS Banking Details", redFont);
                paragraph5.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph5);
                document.Add(gap);

                PdfPCell b1 = new PdfPCell(new Phrase("Bank Name    :" + "Nedbank", thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(b1);
                PdfPCell b2 = new PdfPCell(new Phrase("Account Number    :" + "1015134897", thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(b2);
                PdfPCell b3 = new PdfPCell(new Phrase("Creditor Name    :" + "Gumbi Financial Services", thFont)) { Colspan = 2, Border = 0 };
                debitTable.AddCell(b3);

                document.Add(debitTable);
                document.Add(gap);
            }

            document.Add(gap);
            PdfPTable tblOfficeDetail = new PdfPTable(2);
            PdfPCell signCell = new PdfPCell(new Phrase("Signature: .....................................", fontBody)) { Colspan = 2, HorizontalAlignment = 3, Border = 0 };
            tblOfficeDetail.AddCell(signCell);
            PdfPCell dateCell = new PdfPCell(new Phrase("Date Issued: "+DateTime.Now.ToLongDateString(), fontBody)) { HorizontalAlignment = 3, Border = 0 };
            tblOfficeDetail.AddCell(dateCell);
            PdfPCell stampCell = new PdfPCell(new Phrase("Stamp  :.......................................", fontBody)) { Rowspan = 2, HorizontalAlignment = 3, Border = 0 };
            tblOfficeDetail.AddCell(stampCell);

            document.Add(tblOfficeDetail);

            document.Close();

            byte[] file = memStream.ToArray();
            memStream.Write(file, 0, file.Length);
            memStream.Position = 0;

            return new FileStreamResult(memStream, "application/pdf");
        }

        public FileStreamResult PrintAll()
        {
            //Set up the document and the MS to write it to and create the PDF writer instance
            MemoryStream memStream = new MemoryStream();
            Document document = new Document(PageSize.A3, 0, 0, 0, 0);
            PdfWriter writer = PdfWriter.GetInstance(document, memStream);
            writer.CloseStream = false;

            //Set up fonts used in the document
            Font fontHeading3 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD + Font.UNDERLINE, BaseColor.BLACK);
            Font subHeaderFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLDITALIC, BaseColor.BLACK);
            Font farmDetailFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK);
            Font fontBody = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD, BaseColor.BLACK);
            Font thFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD, BaseColor.BLACK);
            Font redFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD, BaseColor.RED);
            Font fontData = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, BaseColor.BLACK);

            //Open the PDF document
            document.Open();

            //Create the sub dividing heading paragraph with the sub-heading font
            Paragraph paraSubHeader = new Paragraph(" __________________________________________________________________________Policy Holders' List_________________________________________________________________________", subHeaderFont);


            PdfPTable tblHeader = new PdfPTable(3)
            {
                SpacingBefore = 50f,
                SpacingAfter = 50f
            };





            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logo.png"));
            logo.Alignment = Element.ALIGN_CENTER; // Absolute position
            document.Add(logo);
            document.Add(tblHeader);
            document.Add(paraSubHeader);

            //To get all new members
            var list = db.NewMembers.ToList();

            PdfPTable tblContent = new PdfPTable(15);
            tblContent.SpacingBefore = 50f;
            tblContent.WidthPercentage = 90;




            PdfPCell h1 = new PdfPCell(new Phrase("Policy Number\t\t\t\t                 ", thFont));
            tblContent.AddCell(h1);
            PdfPCell h2 = new PdfPCell(new Phrase("Title\t\t\t\t                                   ", thFont));
            tblContent.AddCell(h2);
            PdfPCell h3 = new PdfPCell(new Phrase("First Name   \t\t\t\t                     ", thFont));
            tblContent.AddCell(h3);
            PdfPCell h4 = new PdfPCell(new Phrase("Last Name    \t\t\t\t                     ", thFont));
            tblContent.AddCell(h4);
            PdfPCell h5 = new PdfPCell(new Phrase("ID No    \t\t\t\t                    ", thFont));
            tblContent.AddCell(h5);
            //PdfPCell h6 = new PdfPCell(new Phrase("Date Of Birth    \t\t\t\t                ", thFont));
            //tblContent.AddCell(h6);
            //PdfPCell h7 = new PdfPCell(new Phrase("Age    \t\t\t\t                                  ", thFont));
            //tblContent.AddCell(h7);
            PdfPCell h8 = new PdfPCell(new Phrase("Gender       \t\t\t                         ", thFont));
            tblContent.AddCell(h8);
            PdfPCell h9 = new PdfPCell(new Phrase("Marital Status \t\t\t                    ", thFont));
            tblContent.AddCell(h9);
            //PdfPCell h10 = new PdfPCell(new Phrase("Telephone       \t\t\t                    ", thFont));
            //tblContent.AddCell(h10);
            PdfPCell h11 = new PdfPCell(new Phrase("Cell No  \t\t\t\t                     ", thFont));
            tblContent.AddCell(h11);
            PdfPCell h12 = new PdfPCell(new Phrase("Email \t\t\t\t               ", thFont));
            tblContent.AddCell(h12);
            PdfPCell h13 = new PdfPCell(new Phrase("Fax \t\t\t\t                           ", thFont));
            tblContent.AddCell(h13);
            //PdfPCell h14 = new PdfPCell(new Phrase("Physical Address \t\t\t\t             ", thFont));
            //tblContent.AddCell(h14);
            PdfPCell h15 = new PdfPCell(new Phrase("Postal Address \t\t\t\t                 ", thFont));
            tblContent.AddCell(h15);
            PdfPCell h16 = new PdfPCell(new Phrase("Date Added \t                      ", thFont));
            tblContent.AddCell(h16);
            PdfPCell h17 = new PdfPCell(new Phrase("Policy Plan \t\t\t\t                       ", thFont));
            tblContent.AddCell(h17);
            PdfPCell h18 = new PdfPCell(new Phrase("Premium    \t\t\t\t                       ", thFont));
            tblContent.AddCell(h18);
            PdfPCell h19 = new PdfPCell(new Phrase("Category   \t\t\t\t                         ", thFont));
            tblContent.AddCell(h19);


            foreach (var a in list)
            {
                PdfPCell tc1 = new PdfPCell(new Phrase(a.policyNo, fontData));
                tblContent.AddCell(tc1);

                PdfPCell tc2 = new PdfPCell(new Phrase(a.title, fontData));
                tblContent.AddCell(tc2);

                PdfPCell tc3 = new PdfPCell(new Phrase(a.fName, fontData));
                tblContent.AddCell(tc3);

                PdfPCell tc4 = new PdfPCell(new Phrase(a.lName, fontData));
                tblContent.AddCell(tc4);

                PdfPCell tc5 = new PdfPCell(new Phrase(a.IdNo, fontData));
                tblContent.AddCell(tc5);

                //PdfPCell tc6 = new PdfPCell(new Phrase(Convert.ToString(a.dOb), fontData));
                //tblContent.AddCell(tc6);

                //PdfPCell tc7 = new PdfPCell(new Phrase(Convert.ToString(a.age), fontData));
                //tblContent.AddCell(tc7);

                PdfPCell tc8 = new PdfPCell(new Phrase(a.gender, fontData));
                tblContent.AddCell(tc8);


                PdfPCell tc9 = new PdfPCell(new Phrase(a.maritalStat, fontData));
                tblContent.AddCell(tc9);

                //   PdfPCell tc10 = new PdfPCell(new Phrase(a.telNo, fontData));
                //tblContent.AddCell(tc10);
                PdfPCell tc11 = new PdfPCell(new Phrase(a.cellNo, fontData));
                tblContent.AddCell(tc11);
                PdfPCell tc12 = new PdfPCell(new Phrase(a.CustEmail, fontData));
                tblContent.AddCell(tc12);
                PdfPCell tc13 = new PdfPCell(new Phrase(a.fascimileNo, fontData));
                tblContent.AddCell(tc13);
                PdfPCell tc14 = new PdfPCell(new Phrase(a.physicalAddress, fontData));
                tblContent.AddCell(tc14);
                PdfPCell tc16 = new PdfPCell(new Phrase(Convert.ToString(a.dateAdded), fontData));
                tblContent.AddCell(tc16);
                PdfPCell tc17 = new PdfPCell(new Phrase(a.Policyplan, fontData));
                tblContent.AddCell(tc17);
                PdfPCell tc18 = new PdfPCell(new Phrase(Convert.ToString(a.Premium), fontData));
                tblContent.AddCell(tc18);
                PdfPCell tc19 = new PdfPCell(new Phrase(a.Category, fontData));
                tblContent.AddCell(tc19);
            }

            document.Add(tblContent);

            Paragraph an = new Paragraph("\n\n");

            document.Add(an);
            Paragraph bn = new Paragraph("\n\n");

            document.Add(bn);
            Paragraph cn = new Paragraph("\n\n");

            document.Add(cn);

            PdfPTable tblOfficeDetail = new PdfPTable(2);
            PdfPCell dateCell = new PdfPCell(new Phrase("Date Issued: "+DateTime.Now.ToLongDateString(), fontBody)) { HorizontalAlignment = 3, Border = 0 };
            tblOfficeDetail.AddCell(dateCell);
            PdfPCell stampCell = new PdfPCell(new Phrase("Stamp  :........................", fontBody)) { Rowspan = 2, HorizontalAlignment = 3, Border = 0 };
            tblOfficeDetail.AddCell(stampCell);

            document.Add(tblOfficeDetail);

            document.Close();

            byte[] file = memStream.ToArray();
            memStream.Write(file, 0, file.Length);
            memStream.Position = 0;

            return new FileStreamResult(memStream, "application/pdf");
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
        public ActionResult Edit([Bind(Include = "policyNo,title,fName,lName,IdNo,dOb,gender,maritalStat,telNo,cellNo,CustEmail,fascimileNo,physicalAddress,postalAddress,dateAdded,Policyplan,Premium,Category,Branch,SalesPerson,capturedby,addDep,paying,Active")] NewMember newMember)
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

                db.Payers.Where(p => p.policyNo == id).ToList().ForEach(p => db.Payers.Remove(p));
                db.SaveChanges();

                db.Dependants.Where(p => p.policyNo == id).ToList().ForEach(p => db.Dependants.Remove(p));
                db.SaveChanges();

                db.Beneficiaries.Where(p => p.policyNo == id).ToList().ForEach(p => db.Beneficiaries.Remove(p));
                db.SaveChanges();

                db.DebitOrderAuthorizations.Where(p => p.policyNo == id).ToList().ForEach(p => db.DebitOrderAuthorizations.Remove(p));
                db.SaveChanges();

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
