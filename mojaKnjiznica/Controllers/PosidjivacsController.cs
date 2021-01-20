using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mojaKnjiznica.Models;

namespace mojaKnjiznica.Controllers
{
    [Authorize]
    public class PosidjivacsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posidjivacs
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.sime = sortOrder == "sime_asc" ? "sime_desc" : "sime_asc";
            ViewBag.smobitel = sortOrder == "smobitel_asc" ? "smobitel_desc" : "smobitel_asc";
            ViewBag.semail = sortOrder == "semail_asc" ? "semail_desc" : "semail_asc";

            if (searchString != null)
            {

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var posudjivaci = from s in db.Posidjivacs select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                posudjivaci = posudjivaci.Where(s => s.Ime.Contains(searchString) || s.Mobitel.Contains(searchString)
                || s.Email.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "sime_desc":
                    posudjivaci = posudjivaci.OrderByDescending(s => s.Ime);
                    break;
                case "sime_asc":
                    posudjivaci = posudjivaci.OrderBy(s => s.Ime);
                    break;
                case "smobitel_desc":
                    posudjivaci = posudjivaci.OrderByDescending(s => s.Mobitel);
                    break;
                case "smobitel_asc":
                    posudjivaci = posudjivaci.OrderBy(s => s.Mobitel);
                    break;
                case "semail_desc":
                    posudjivaci = posudjivaci.OrderByDescending(s => s.Email);
                    break;
                case "semail_asc":
                    posudjivaci = posudjivaci.OrderBy(s => s.Email);
                    break;
                default:

                    break;

            }
            return View(posudjivaci.ToList());
        }

        // GET: Posidjivacs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posidjivac posidjivac = db.Posidjivacs.Find(id);
            if (posidjivac == null)
            {
                return HttpNotFound();
            }
            return View(posidjivac);
        }

        // GET: Posidjivacs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posidjivacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Mobitel,Email")] Posidjivac posidjivac)
        {
            if (ModelState.IsValid)
            {
                db.Posidjivacs.Add(posidjivac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posidjivac);
        }

        // GET: Posidjivacs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posidjivac posidjivac = db.Posidjivacs.Find(id);
            if (posidjivac == null)
            {
                return HttpNotFound();
            }
            return View(posidjivac);
        }

        // POST: Posidjivacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ime,Mobitel,Email")] Posidjivac posidjivac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posidjivac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posidjivac);
        }

        // GET: Posidjivacs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posidjivac posidjivac = db.Posidjivacs.Find(id);
            if (posidjivac == null)
            {
                return HttpNotFound();
            }
            return View(posidjivac);
        }

        // POST: Posidjivacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posidjivac posidjivac = db.Posidjivacs.Find(id);
            db.Posidjivacs.Remove(posidjivac);
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
