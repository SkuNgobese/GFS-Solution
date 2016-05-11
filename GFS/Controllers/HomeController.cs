//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.AspNet.Identity; !!!! Add something to enable this 2
//using Microsoft.Owin.Security;
using GFS.Domain;
using GFS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GFS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }

        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }

        public ActionResult Tables()
        {
            return View("Tables");
        }

        public ActionResult Forms()
        {
            return View("Forms");
        }

        public ActionResult Panels()
        {
            return View("Panels");
        }

        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        public ActionResult Notifications()
        {
            return View("Notifications");
        }

        public ActionResult Typography()
        {
            return View("Typography");
        }

        public ActionResult Icons()
        {
            return View("Icons");
        }

        public ActionResult Grid()
        {
            return View("Grid");
        }

        public ActionResult Blank()
        {
            return View("Blank");
        }

        public ActionResult Login()
        {
            return View("Login");
        }
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(User user, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Login");
        //    }
        //    var data = new Data();
        //    var users = data.users();

        //    if (users.Any(p => p.user == user.CustEmail && p.password == user.password))
        //    {
        //        //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.firstname,user.lastname), }, DefaultAuthenticationTypes.ApplicationCookie);

        //        //Authentication.SignIn(new AuthenticationProperties
        //        //{
        //        //    IsPersistent = user.RememberMe
        //        //}, identity);

        //        return RedirectToAction("Index", "NewMembers");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Invalid login attempt.");
        //        return View("Login");
        //    }
        //}

    }
}