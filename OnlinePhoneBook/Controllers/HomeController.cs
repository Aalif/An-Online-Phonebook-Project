using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlinePhoneBook.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace OnlinePhoneBook.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private dbphonebookEntitiesContext db = new dbphonebookEntitiesContext();

        // GET: Home
        public ActionResult Index(string search, int? page)
        {
            var contactdetails_tb = db.contactdetails_tb.Include(c => c.Country_tb).Include(c => c.type_tb);
            return View(contactdetails_tb.Where(x => x.fname.StartsWith(search) || x.maincontact_number.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1,3));
            //return View(contactdetails_tb.ToList());
        }

        // GET: Home/Details/5
        [ActionName("ContactDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contactdetails_tb contactdetails_tb = db.contactdetails_tb.Find(id);
            if (contactdetails_tb == null)
            {
                return HttpNotFound();
            }
            return View(contactdetails_tb);
        }

        // GET: Home/Create
        public ActionResult AddContact()
        {
            ViewBag.country_fid = new SelectList(db.Country_tb, "id", "country_name");
            ViewBag.type_fid = new SelectList(db.type_tb, "id", "type");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        //creating method for checking image type is ok or not
        //public bool isimagetype(string imgtypes)
        //{
        //    return imgtypes.Equals("image/png") || imgtypes.Equals("image/jpg") || imgtypes.Equals("image/jpeg");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContact(contactdetails_tb contactdetails_tbs, HttpPostedFileBase file)
        {
            //creating method for checking image type is ok or not
            if(file != null)
            {
                if(file.ContentLength > (2*1048576))
                {
                    ModelState.AddModelError("FileErrorMessage", "File size must me within 2mb");
                }
                string[] allowtype = new string[] { "png", "jpg" };
                string fileextension = Path.GetExtension(file.FileName).Substring(1);
                if (!allowtype.Contains(fileextension))
                {
                    ModelState.AddModelError("FileErrorMessage", "File  must me jpg or png");
                }


            }
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    string savepath = Server.MapPath("~/Image");
                    string filename =  Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/Image"), filename));
                    contactdetails_tbs.imagepath = "~/Image/"+ filename;
                     
                }
                db.contactdetails_tb.Add(contactdetails_tbs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.country_fid = new SelectList(db.Country_tb, "id", "country_name", contactdetails_tbs.country_fid);
            ViewBag.type_fid = new SelectList(db.type_tb, "id", "type", contactdetails_tbs.type_fid);
            return View(contactdetails_tbs);
        }

        // GET: Home/Edit/5
        [ActionName("EditContact")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contactdetails_tb contactdetails_tb = db.contactdetails_tb.Find(id);
            if (contactdetails_tb == null)
            {
                return HttpNotFound();
            }
            ViewBag.country_fid = new SelectList(db.Country_tb, "id", "country_name", contactdetails_tb.country_fid);
            ViewBag.type_fid = new SelectList(db.type_tb, "id", "type", contactdetails_tb.type_fid);
            return View(contactdetails_tb);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ActionName("EditContact")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(contactdetails_tb contactdetails_tbs, HttpPostedFileBase file)
        {
            //cheking file length 
            if (file != null)
            {
                if (file.ContentLength > (2 * 1048576))
                {
                    ModelState.AddModelError("FileErrorMessage", "File size must me within 2mb");
                }
                string[] allowtype = new string[] { "png", "jpg" };
                string fileextension = Path.GetExtension(file.FileName).Substring(1);
                if (!allowtype.Contains(fileextension))
                {
                    ModelState.AddModelError("FileErrorMessage", "File  must me jpg or png");
                }


            }
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    string savepath = Server.MapPath("~/Image");
                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/Image"), filename));
                    contactdetails_tbs.imagepath = "~/Image/" + filename;
                }
                db.Entry(contactdetails_tbs).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.country_fid = new SelectList(db.Country_tb, "id", "country_name", contactdetails_tbs.country_fid);
            ViewBag.type_fid = new SelectList(db.type_tb, "id", "type", contactdetails_tbs.type_fid);
            return View(contactdetails_tbs);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contactdetails_tb contactdetails_tb = db.contactdetails_tb.Find(id);
            if (contactdetails_tb == null)
            {
                return HttpNotFound();
            }
            return View(contactdetails_tb);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contactdetails_tb contactdetails_tb = db.contactdetails_tb.Find(id);
            db.contactdetails_tb.Remove(contactdetails_tb);
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
