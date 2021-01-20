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
    public class PosudiliSusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PosudiliSus
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            var posudiliSus = db.PosudiliSus.Include(p => p.Knjiga).Include(p => p.Posidjivac);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.snaslov = sortOrder == "snaslov_asc" ? "snaslov_desc" : "snaslov_asc";
            ViewBag.sime = sortOrder == "sime_asc" ? "sime_desc" : "sime_asc";
            ViewBag.sdatumposudbe = sortOrder == "sdatumposudbe_asc" ? "sdatumposudbe_desc" : "sdatumposudbe_asc";
            if (searchString != null)
            {

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var posudilisu = from s in posudiliSus select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                posudilisu = posudilisu.Where(s => s.Knjiga.Naslov.Contains(searchString) || s.Posidjivac.Ime.Contains(searchString)
                ||s.DatumPosudbe.ToString().Contains(searchString));

            }

            switch (sortOrder)
            {
                case "snaslov_desc":
                    posudilisu = posudilisu.OrderByDescending(s => s.Knjiga.Naslov);
                    break;
                case "snaslov_asc":
                    posudilisu = posudilisu.OrderBy(s => s.Knjiga.Naslov);
                    break;
                case "sime_desc":
                    posudilisu = posudilisu.OrderByDescending(s => s.Posidjivac.Ime);
                    break;
                case "sime_asc":
                    posudilisu = posudilisu.OrderBy(s => s.Posidjivac.Ime);
                    break;
                case "sdatumposudbe_desc":
                    posudilisu = posudilisu.OrderByDescending(s => s.DatumPosudbe);
                    break;
                case "sdatumposudbe_asc":
                    posudilisu = posudilisu.OrderBy(s => s.DatumPosudbe);
                    break;
                default:

                    break;

            }
            return View(posudilisu.ToList());
        }

        // GET: PosudiliSus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosudiliSu posudiliSu = db.PosudiliSus.Find(id);
            if (posudiliSu == null)
            {
                return HttpNotFound();
            }
            return View(posudiliSu);
        }

        // GET: PosudiliSus/Create
        public ActionResult Create()
        {
            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov");
            ViewBag.PosidjivacId = new SelectList(db.Posidjivacs, "Id", "Ime");
            return View();
        }

        // POST: PosudiliSus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PosidjivacId,KnjigaId,DatumPosudbe")] PosudiliSu posudiliSu)
        {
            if (ModelState.IsValid)
            {
                db.PosudiliSus.Add(posudiliSu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov", posudiliSu.KnjigaId);
            ViewBag.PosidjivacId = new SelectList(db.Posidjivacs, "Id", "Ime", posudiliSu.PosidjivacId);
            return View(posudiliSu);
        }

        // GET: PosudiliSus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosudiliSu posudiliSu = db.PosudiliSus.Find(id);
            if (posudiliSu == null)
            {
                return HttpNotFound();
            }
            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov", posudiliSu.KnjigaId);
            ViewBag.PosidjivacId = new SelectList(db.Posidjivacs, "Id", "Ime", posudiliSu.PosidjivacId);
            return View(posudiliSu);
        }

        // POST: PosudiliSus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PosidjivacId,KnjigaId,DatumPosudbe")] PosudiliSu posudiliSu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posudiliSu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KnjigaId = new SelectList(db.Knjigas, "Id", "Naslov", posudiliSu.KnjigaId);
            ViewBag.PosidjivacId = new SelectList(db.Posidjivacs, "Id", "Ime", posudiliSu.PosidjivacId);
            return View(posudiliSu);
        }

        // GET: PosudiliSus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosudiliSu posudiliSu = db.PosudiliSus.Find(id);
            if (posudiliSu == null)
            {
                return HttpNotFound();
            }
            return View(posudiliSu);
        }

        // POST: PosudiliSus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PosudiliSu posudiliSu = db.PosudiliSus.Find(id);
            db.PosudiliSus.Remove(posudiliSu);
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
