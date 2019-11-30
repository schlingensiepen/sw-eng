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
    public class FlussController : Controller
    {
        private StadtLandFlussModelContainer db = new StadtLandFlussModelContainer();

        // GET: Fluss
        public ActionResult Index()
        {
            return View(db.Fluesse.ToList());
        }

        // GET: Fluss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fluss fluss = db.Fluesse.Find(id);
            if (fluss == null)
            {
                return HttpNotFound();
            }
            return View(fluss);
        }

        // GET: Fluss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fluss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Male")] Fluss fluss)
        {
            if (ModelState.IsValid)
            {
                db.Fluesse.Add(fluss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fluss);
        }

        // GET: Fluss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fluss fluss = db.Fluesse.Find(id);
            if (fluss == null)
            {
                return HttpNotFound();
            }
            return View(fluss);
        }

        // POST: Fluss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Male")] Fluss fluss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fluss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fluss);
        }

        // GET: Fluss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fluss fluss = db.Fluesse.Find(id);
            if (fluss == null)
            {
                return HttpNotFound();
            }
            return View(fluss);
        }

        // POST: Fluss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fluss fluss = db.Fluesse.Find(id);
            db.NamedObjects.Remove(fluss);
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
