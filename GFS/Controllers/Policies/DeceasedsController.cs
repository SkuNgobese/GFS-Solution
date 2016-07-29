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
using System.IO;

namespace GFS.Controllers.Policies
{
    public class DeceasedsController : Controller
    {
        private GFSContext db = new GFSContext();

        // GET: Deceaseds
        public ActionResult Index(string search)
        {
            var dec = from x in db.Deceaseds.ToList() select x;

            if(search!=null)
            {
                dec = dec.Where(p => p.idNo == search);
            }

            return View(dec);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deceased deceased)
        {
            //if (ModelState.IsValid)
            //{
            Deceased dec = db.Deceaseds.ToList().Find(x => x.idNo == Session["ID Number"].ToString());
            if (dec != null)
            {
                TempData["Response13"] = "**This person has already been declared as deceased**";
                Session["DeceasedNo"] = deceased.deceasedNo;
                return RedirectToAction("Create");
            }
            if(deceased.DateOfDeath>DateTime.Now)
            {
                TempData["Response13"] = "**Date of death cannot be future date**";
                return RedirectToAction("Create");
            }
            else
            {
                deceased.firstName = Session["First Name"].ToString();
                deceased.lastName = Session["Last Name"].ToString();
                deceased.idNo = Session["ID Number"].ToString();
                deceased.age = Convert.ToInt32(Session["Age"].ToString());
                deceased.policyNo = Session["PolicyNo"].ToString();
                List<FileDetail> fileDetails = new List<FileDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);

                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }

                deceased.FileDetails = fileDetails;
                db.Deceaseds.Add(deceased);
                db.SaveChanges();
                Dependant d = db.Dependants.ToList().Find(r => r.IdNo == deceased.idNo);
                db.Dependants.Remove(d);
                NewMember n = db.NewMembers.ToList().Find(r => r.IdNo == deceased.idNo);
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deceased deceased = db.Deceaseds.Include(s => s.FileDetails).SingleOrDefault(x => x.deceasedNo == id);
            if (deceased == null)
            {
                return HttpNotFound();
            }
            return View(deceased);
        }
        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Deceased deceased)
        {
            if (ModelState.IsValid)
            {
                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            deceasedN = deceased.deceasedNo
                        };
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }

                db.Entry(deceased).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(deceased);
        }



        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                FileDetail fileDetail = db.FileDetails.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileDetails.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
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

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,string id1)
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
    }
}
