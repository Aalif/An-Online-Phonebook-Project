using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlinePhoneBook.Models;
using System.IO;
using System.Web.Security;

namespace OnlinePhoneBook.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private dbphonebookEntitiesContext db = new dbphonebookEntitiesContext();
        // GET: Account
        //login
        [HttpGet]
        public ActionResult login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(user_tb user_tbs)
        {
            int counts = db.user_tb.Where(x => x.user_email == user_tbs.user_email &&  x.password == user_tbs.password).Count();
            if(counts == 0)
            {
                ViewBag.invaliduser = "Please enter a valid email and password";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user_tbs.user_email, false);
                return RedirectToAction("Index", "Home");
            }
            
        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Account");
        }

        //register
        [HttpGet]
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register(user_tb user_tbs)
        {
            if (ModelState.IsValid)
            {
                db.user_tb.Add(user_tbs);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View(user_tbs);
        }
    }
}