using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BibliothekLib;
using Microsoft.AspNet.Identity;

namespace BibliothekWebApp.Controllers
{
    public class CatalogController : Controller
    {
        private BibliothekModelContainer db = new BibliothekModelContainer();

        // GET: Catalog
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            User user = db.Users.FirstOrDefault(
                u => u.EMail == userName);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.OrderedIds = user.Orders.Select(o => o.Id).ToList();
            return View(db.Works.ToList());
        }

        // GET: Catalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        public ActionResult Order(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            var userName = User.Identity.GetUserName();
            User user = db.Users.FirstOrDefault(
                u => u.EMail == userName);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            user.Orders.Add(work);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Storno(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userName = User.Identity.GetUserName();
            User user = db.Users.FirstOrDefault(
                u => u.EMail == userName);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = user.Orders.FirstOrDefault(w => w.Id == id);
            if (work == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            user.Orders.Remove(work);

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
