using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BibliothekLib;

namespace BibliothekWebApp.Controllers
{
    public class MediaController : Controller
    {
        private BibliothekModelContainer db = new BibliothekModelContainer();

        // GET: Media
        public ActionResult Index()
        {
            var media = db.Media.Include(m => m.Work).Include(m => m.User);
            return View(media.ToList());
        }

        // GET: Media/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // GET: Media/Create
        public ActionResult Create()
        {
            ViewBag.WorkId = new SelectList(db.Works, "Id", "Titel");
            ViewBag.UserId = new[] {new SelectListItem() {Text = "keiner", Value = ""}}.Union(
                new SelectList(db.Users, "Id", "Name"));
                //db.Users.Select(u => new {Id = u.Id, Value=u.Name})
                //.Union(new [] { new {Id="", Value="keiner" } }), "Id", "Name");
            return View();
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,WorkId,UserId")] Media media)
        {
            if (ModelState.IsValid)
            {
                db.Media.Add(media);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkId = new SelectList(db.Works, "Id", "Titel", media.WorkId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");

            //ViewBag.UserId = new SelectList(db.Users, "Id", "Name", media.UserId);
            return View(media);
        }

        // GET: Media/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkId = new SelectList(db.Works, "Id", "Titel", media.WorkId);

            //ViewBag.UserId = new[] { new SelectListItem() { Text = "keiner", Value = "" } }.Union(
            //    new SelectList(db.Users, "Id", "Name", media.UserId));


            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", media.UserId);
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,WorkId,UserId")] Media media)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkId = new SelectList(db.Works, "Id", "Titel", media.WorkId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", media.UserId);
            return View(media);
        }

        // GET: Media/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Media media = db.Media.Find(id);
            db.Media.Remove(media);
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
