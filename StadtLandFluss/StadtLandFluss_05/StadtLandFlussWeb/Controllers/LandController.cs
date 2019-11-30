using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StadtLandFlussLibDB;

namespace StadtLandFlussWeb.Controllers
{
    public class LandController : Controller
    {
        private StadtLandFlussModelContainer db = new StadtLandFlussModelContainer();

        // GET: Land
        public ActionResult Index()
        {
            var namedObjects = db.Laender.Include(l => l.HauptStadt);
            return View(namedObjects.ToList());
        }

        // GET: Land/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Land land = db.Laender.Find(id);
            if (land == null)
            {
                return HttpNotFound();
            }
            return View(land);
        }

        // GET: Land/Create
        public ActionResult Create()
        {
            ViewBag.HauptStadtId = new SelectList(db.Staedte, "Id", "Name");
            return View();
        }

        // POST: Land/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,HauptStadtId")] Land land)
        {
            if (ModelState.IsValid)
            {
                db.Laender.Add(land);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HauptStadtId = new SelectList(db.Staedte, "Id", "Name", land.HauptStadtId);
            return View(land);
        }

        // GET: Land/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Land land = db.Laender.Find(id);
            if (land == null)
            {
                return HttpNotFound();
            }
            ViewBag.HauptStadtId = new SelectList(db.Staedte, "Id", "Name", land.HauptStadtId);
            return View(land);
        }

        // POST: Land/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,HauptStadtId")] Land land)
        {
            if (ModelState.IsValid)
            {
                db.Entry(land).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HauptStadtId = new SelectList(db.Staedte, "Id", "Name", land.HauptStadtId);
            return View(land);
        }

        // GET: Land/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Land land = db.Laender.Find(id);
            if (land == null)
            {
                return HttpNotFound();
            }
            return View(land);
        }

        // POST: Land/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Land land = db.Laender.Find(id);
            db.NamedObjects.Remove(land);
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
