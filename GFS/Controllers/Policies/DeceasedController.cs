using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using GFS.Models;
using GFS.Models.Policies;

namespace GFS.Controllers.Policies
{
    public class DeceasedController : Controller
    {
        private GFSContext db = new GFSContext();
        // GET: Deceased
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeclareDeceased(string id,string search)
        {
            var dep = from d in db.Dependants select d;
            if(search=="principal")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    NewMember d = db.NewMembers.ToList().Find(r => r.IdNo == id);
                    if (d != null)
                    {
                        Session["First Name"] = d.fName;
                        Session["Last Name"] = d.lName;
                        Session["ID Number"] = d.IdNo;
                        //Session["Age"] = d.age;
                        Session["PolicyNo"] = d.policyNo;

                        return RedirectToAction("Create", "Deceaseds");
                    }
                    else if(d==null)
                    {
                        TempData["responc"] = "Sorry, Member you searched for does not exist! Please check the ID Number.";
                        return View("DeclareDeceased");
                    }
                }
            }
            else if(search == "dependant")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    Dependant d = db.Dependants.ToList().Find(r => r.IdNo == id);
                    if (d != null)
                    {
                        Session["First Name"] = d.fName;
                        Session["Last Name"] = d.lName;
                        Session["ID Number"] = d.IdNo;
                        Session["Age"] = d.age;
                        Session["PolicyNo"] = d.policyNo;

                        return RedirectToAction("Create", "Deceaseds");
                    }
                    else if (d == null)
                    {
                        TempData["responc"] = "Sorry, Member you searched for does not exist! Please check the ID Number.";
                        return View("DeclareDeceased");
                    }
                }         
            }

            return View();
        }
    }
}