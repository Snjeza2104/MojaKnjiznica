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
    public class KnjigasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Knjigas
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            var knjigas = db.Knjigas.Include(k => k.Izdavac);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.snaslov = sortOrder == "snaslov_asc" ? "snaslov_desc" : "snaslov_asc";
            ViewBag.sizdavac = sortOrder == "sizdavac_asc" ? "sizdavac_desc" : "sizdavac_asc";
            ViewBag.sbrojstranica = sortOrder == "sbrojstranica_asc" ? "sbrojstranica_desc" : "sbrojstranica_asc";
            ViewBag.scijena = sortOrder == "scijena_asc" ? "scijena_desc" : "scijena_asc";
            ViewBag.sgodinaizdanja = sortOrder == "sgodinaizdanja_asc" ? "sgodinaizdanja_desc" : "sgodinaizdanja_asc";

            if (searchString != null)
            {

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var knjige = from s in knjigas select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                knjige = knjige.Where(s => s.Naslov.Contains(searchString) || s.Izdavac.Naziv.Contains(searchString)
                || s.BrojStranica.ToString().Contains(searchString) || s.Cijena.ToString().Contains(searchString)
                || s.GodinaIzdanja.ToString().Contains(searchString));

            }
            switch (sortOrder)
            {
                case "snaslov_desc":
                    knjige = knjige.OrderByDescending(s => s.Naslov);
                    break;
                case "snaslov_asc":
                    knjige = knjige.OrderBy(s => s.Naslov);
                    break;
                case "sizdavac_desc":
                    knjige = knjige.OrderByDescending(s => s.Izdavac.Naziv);
                    break;
                case "sizdavac_asc":
                    knjige = knjige.OrderBy(s => s.Izdavac.Naziv);
                    break;
                case "sbrojstranica_desc":
                    knjige = knjige.OrderByDescending(s => s.BrojStranica);
                    break;
                case "sbrojstranica_asc":
                    knjige = knjige.OrderBy(s => s.BrojStranica);
                    break;
                case "scijena_desc":
                    knjige = knjige.OrderByDescending(s => s.Cijena);
                    break;
                case "scijena_asc":
                    knjige = knjige.OrderBy(s => s.Cijena);
                    break;
                case "sgodinaizdanja_desc":
                    knjige = knjige.OrderByDescending(s => s.GodinaIzdanja);
                    break;
                case "sgodinaizdanja_asc":
                    knjige = knjige.OrderBy(s => s.GodinaIzdanja);
                    break;
                default:

                    break;

            }

            return View(knjige.ToList());
        }

        // GET: Knjigas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjiga knjiga = db.Knjigas.Find(id);
            if (knjiga == null)
            {
                return HttpNotFound();
            }
            return View(knjiga);
        }

        // GET: Knjigas/Create
        public ActionResult Create()
        {
            ViewBag.IdIzdavac = new SelectList(db.Izdavacis, "Id", "Naziv");
            return View();
        }

        // POST: Knjigas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naslov,IdIzdavac,BrojStranica,Cijena,GodinaIzdanja")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                db.Knjigas.Add(knjiga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdIzdavac = new SelectList(db.Izdavacis, "Id", "Naziv", knjiga.IdIzdavac);
            return View(knjiga);
        }

        // GET: Knjigas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjiga knjiga = db.Knjigas.Find(id);
            if (knjiga == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdIzdavac = new SelectList(db.Izdavacis, "Id", "Naziv", knjiga.IdIzdavac);
            return View(knjiga);
        }

        // POST: Knjigas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naslov,IdIzdavac,BrojStranica,Cijena,GodinaIzdanja")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knjiga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdIzdavac = new SelectList(db.Izdavacis, "Id", "Naziv", knjiga.IdIzdavac);
            return View(knjiga);
        }

        // GET: Knjigas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjiga knjiga = db.Knjigas.Find(id);
            if (knjiga == null)
            {
                return HttpNotFound();
            }
            return View(knjiga);
        }

        // POST: Knjigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knjiga knjiga = db.Knjigas.Find(id);
            db.Knjigas.Remove(knjiga);
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
