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
    public class IzdavacisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Izdavacis
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.snaziv = sortOrder == "snaziv_asc" ? "snaziv_desc":"snaziv_asc";
            ViewBag.sadresa = sortOrder == "sadresa_asc" ? "sadresa_desc" : "sadresa_asc";
            ViewBag.smobitel = sortOrder == "smobitel_asc" ? "smobitel_desc" : "smobitel_asc";
            ViewBag.semail = sortOrder == "semail_asc"? "semail_desc" : "semail_asc";

            if (searchString != null)
            {

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var izdavaci = from s in db.Izdavacis select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                izdavaci = izdavaci.Where(s => s.Naziv.Contains(searchString) || s.Adresa.Contains(searchString)
                ||  s.Mobitel.Contains(searchString) ||  s.Email.Contains(searchString));
            }

           
            switch (sortOrder)
            {
                case "snaziv_desc":
                    izdavaci = izdavaci.OrderByDescending(s => s.Naziv);
                    break;
                case "snaziv_asc":
                    izdavaci = izdavaci.OrderBy(s => s.Naziv);
                    break;
                case "sadresa_desc":
                    izdavaci = izdavaci.OrderByDescending(s => s.Adresa);
                    break;
                case "sadresa_asc":
                    izdavaci = izdavaci.OrderBy(s => s.Adresa);
                    break;
                case "smobitel_desc":
                    izdavaci = izdavaci.OrderByDescending(s => s.Mobitel);
                    break;
                case "smobitel_asc":
                    izdavaci = izdavaci.OrderBy(s => s.Mobitel);
                    break;
                case "semail_desc":
                    izdavaci = izdavaci.OrderByDescending(s => s.Email);
                    break;
                case "semail_asc":
                    izdavaci = izdavaci.OrderBy(s => s.Email);
                    break;
                default:
                   
                    break;

            }
            return View(izdavaci.ToList());
        }

        // GET: Izdavacis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izdavaci izdavaci = db.Izdavacis.Find(id);
            if (izdavaci == null)
            {
                return HttpNotFound();
            }
            return View(izdavaci);
        }

        // GET: Izdavacis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Izdavacis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Adresa,Mobitel,Email")] Izdavaci izdavaci)
        {
            if (ModelState.IsValid)
            {
                db.Izdavacis.Add(izdavaci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(izdavaci);
        }

        // GET: Izdavacis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izdavaci izdavaci = db.Izdavacis.Find(id);
            if (izdavaci == null)
            {
                return HttpNotFound();
            }
            return View(izdavaci);
        }

        // POST: Izdavacis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Adresa,Mobitel,Email")] Izdavaci izdavaci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(izdavaci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(izdavaci);
        }

        // GET: Izdavacis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izdavaci izdavaci = db.Izdavacis.Find(id);
            if (izdavaci == null)
            {
                return HttpNotFound();
            }
            return View(izdavaci);
        }

        // POST: Izdavacis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Izdavaci izdavaci = db.Izdavacis.Find(id);
            db.Izdavacis.Remove(izdavaci);
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
