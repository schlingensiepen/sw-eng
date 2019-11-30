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
    public class StadtController : Controller
    {
        private StadtLandFlussModelContainer db = new StadtLandFlussModelContainer();

        // GET: Stadt
        public ActionResult Index()
        {
            var namedObjects = db.Staedte.Include(s => s.LiegtIn);
            return View(namedObjects.ToList());
        }

        // GET: Stadt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadt stadt = db.Staedte.Find(id);
            if (stadt == null)
            {
                return HttpNotFound();
            }
            return View(stadt);
        }

        // GET: Stadt/Create
        public ActionResult Create()
        {
            ViewBag.LiegtInId = new SelectList(db.Laender, "Id", "Name");
            return View();
        }

        // POST: Stadt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LiegtInId")] Stadt stadt)
        {
            if (ModelState.IsValid)
            {
                db.Staedte.Add(stadt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LiegtInId = new SelectList(db.Laender, "Id", "Name", stadt.LiegtInId);
            return View(stadt);
        }

        // GET: Stadt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadt stadt = db.Staedte.Find(id);
            if (stadt == null)
            {
                return HttpNotFound();
            }
            ViewBag.LiegtInId = new SelectList(db.Laender, "Id", "Name", stadt.LiegtInId);
            return View(stadt);
        }

        // POST: Stadt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LiegtInId")] Stadt stadt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stadt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LiegtInId = new SelectList(db.Laender, "Id", "Name", stadt.LiegtInId);
            return View(stadt);
        }

        // GET: Stadt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadt stadt = db.Staedte.Find(id);
            if (stadt == null)
            {
                return HttpNotFound();
            }
            return View(stadt);
        }

        // POST: Stadt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stadt stadt = db.Staedte.Find(id);
            db.NamedObjects.Remove(stadt);
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
