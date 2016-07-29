using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GFS.Models;

namespace GFS.Controllers
{
    public class UsersController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Users
        public ActionResult Index(string SearchString)
        {
            var users = from m in db.Users
                        select m;

            if (!String.IsNullOrEmpty(SearchString))
            {
                users = users.Where(s => s.userid.Contains(SearchString));
            }
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userid,Id,firstname,lastname,CustEmail,user,password,ConfirmPassword,estatus,RememberMe")] User users, string Pass)
        {
            var seI = db.Users.ToList().Find(p => p.userid == users.userid);
            var seE = db.Users.ToList().Find(p => p.CustEmail == users.CustEmail);

            if (seI != null)
            {
                Session["UseAdd"] = "User ID already exists";
                return RedirectToAction("Create");
            }

            if (seE != null)
            {
                Session["UseAdd"] = "User Email already exists";
                return RedirectToAction("Create");
            }
            else if (ModelState.IsValid)
            {


                if (users.user == "Admin")
                {
                    users.Id = 1;
                }
                if (users.user == "Data Capturer")
                {
                    users.Id = 2;
                }
                if (users.user == "Cashier")
                {
                    users.Id = 3;
                }

                if (Pass != "GFSTech")
                {
                    Session["UseAdd"] = "Invalid Pass Key!!!";
                    return RedirectToAction("Create");
                }

                users.estatus = true;

                users.RememberMe = false;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = users.userid });
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userid,Id,firstname,lastname,CustEmail,user,password,ConfirmPassword,estatus,RememberMe")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
