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


        public ActionResult My()
        {
            var userName = User.Identity.GetUserName();
            User user = db.Users.FirstOrDefault(
                u => u.EMail == userName);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.OrderedIds = user.Orders.Select(o => o.Id).ToList();
            return View("Index", user.Orders.ToList());
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

        public ActionResult Order(int? id, string returnAction)
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
            if (returnAction == null)
            {
                returnAction = "Index";
            }
            if (!new[] {"Index", "My"}.Contains(returnAction))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userName = User.Identity.GetUserName();
            User user = db.Users.FirstOrDefault(
                u => u.EMail == userName);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            user.Orders.Add(work);

            db.SaveChanges();
            return RedirectToAction(returnAction);
        }

        public ActionResult Storno(int? id, string returnAction)
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
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            Work work = user.Orders.FirstOrDefault(w => w.Id == id);
            if (work == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (returnAction == null)
            {
                returnAction = "Index";
            }
            if (!new[] { "Index", "My" }.Contains(returnAction))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            user.Orders.Remove(work);

            db.SaveChanges();
            return RedirectToAction(returnAction);
        }

        public ActionResult Borrow(int? id)
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
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Media media = work.Media.FirstOrDefault(m => m.User == null);

            if (media == null)
            {
                ModelState.AddModelError("", "Kein verfügbares Medium gefunden.");
                return View(work);
            }
            return RedirectToAction("Borrow", "Borrow", new {id = media.Id});
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
