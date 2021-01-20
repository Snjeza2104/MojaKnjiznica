using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using mojaKnjiznica.Models;

namespace mojaKnjiznica.Controllers
{
    [Authorize]
    public class AutorisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: Autoris
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime_desc" : "";
            if (searchString != null)
            {

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var autori = from s in db.Autoris
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                autori = autori.Where(s => s.Ime.Contains(searchString));
            }
           

          
            switch (sortOrder)
            {
                case "Ime_desc":
                    autori = autori.OrderByDescending(s => s.Ime);
                    break;
                default:  // Name ascending 
                    autori = autori.OrderBy(s => s.Ime);
                    break;
            }

            

            return View(autori.ToList());
        }

        // GET: Autoris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // GET: Autoris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autoris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Autoris.Add(autori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autori);
        }

        // GET: Autoris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // POST: Autoris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autori);
        }

        // GET: Autoris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // POST: Autoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autori autori = db.Autoris.Find(id);
            db.Autoris.Remove(autori);
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
