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
    public class PiscisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Piscis
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            var piscis = db.Piscis.Include(p => p.Autori).Include(p => p.Knjiga);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.snaslov = sortOrder == "snaslov_asc" ? "snaslov_desc" : "snaslov_asc";
            ViewBag.sime = sortOrder == "sime_asc" ? "sime_desc" : "sime_asc";
            if (searchString != null)
            {

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var pisci = from s in piscis select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                pisci = pisci.Where(s => s.Knjiga.Naslov.Contains(searchString) || s.Autori.Ime.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "snaslov_desc":
                    pisci = pisci.OrderByDescending(s => s.Knjiga.Naslov);
                    break;
                case "snaslov_asc":
                    pisci = pisci.OrderBy(s => s.Knjiga.Naslov);
                    break;
                case "sime_desc":
                    pisci = pisci.OrderByDescending(s => s.Autori.Ime);
                    break;
                case "sime_asc":
                    pisci = pisci.OrderBy(s => s.Autori.Ime);
                    break;
                default:

                    break;

            }
            return View(pisci.ToList());
        }

        // GET: Piscis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pisci pisci = db.Piscis.Find(id);
            if (pisci == null)
            {
                return HttpNotFound();
            }
            return View(pisci);
        }

        // GET: Piscis/Create
        public ActionResult Create()
        {
            ViewBag.AutorId = new SelectList(db.Autoris, "Id", "Ime");
            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov");
            return View();
        }

        // POST: Piscis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AutorId,KnjigaId")] Pisci pisci)
        {
            if (ModelState.IsValid)
            {
                db.Piscis.Add(pisci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutorId = new SelectList(db.Autoris, "Id", "Ime", pisci.AutorId);
            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov", pisci.KnjigaId);
            return View(pisci);
        }

        // GET: Piscis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pisci pisci = db.Piscis.Find(id);
            if (pisci == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutorId = new SelectList(db.Autoris, "Id", "Ime", pisci.AutorId);
            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov", pisci.KnjigaId);
            return View(pisci);
        }

        // POST: Piscis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AutorId,KnjigaId")] Pisci pisci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pisci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutorId = new SelectList(db.Autoris, "Id", "Ime", pisci.AutorId);
            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov", pisci.KnjigaId);
            return View(pisci);
        }

        // GET: Piscis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pisci pisci = db.Piscis.Find(id);
            if (pisci == null)
            {
                return HttpNotFound();
            }
            return View(pisci);
        }

        // POST: Piscis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pisci pisci = db.Piscis.Find(id);
            db.Piscis.Remove(pisci);
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
