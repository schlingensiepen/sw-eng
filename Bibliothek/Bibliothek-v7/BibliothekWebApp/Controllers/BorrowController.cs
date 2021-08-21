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
    public class BorrowController : Controller
    {
        private BibliothekModelContainer db = new BibliothekModelContainer();
        private User LoggedInUser = null;

        public bool setUser()
        {
            var userName = User.Identity.GetUserName();
            User user = db.Users.FirstOrDefault(
                u => u.EMail == userName);
            if (user == null) { return false; }
            ViewBag.User = user;
            LoggedInUser = user;
            return true;
        }

        // GET: Borrow
        public ActionResult Index()
        {
            if (!setUser()) return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            var media = db.Media.Include(m => m.Work).Include(m => m.User);
            return View(media.ToList());
        }
        public ActionResult My()
        {
            if (!setUser()) return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            var media = db.Media
                .Where(m => m.User.Id == LoggedInUser.Id)
                .Include(m => m.Work).Include(m => m.User);
            return View("Index", media);
        }


        // GET: Borrow/Details/5
        public ActionResult Details(int? id)
        {
            if (!setUser()) return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
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

        public ActionResult Borrow(int? id)
        {
            if (!setUser()) return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }

            if (media.User != null)
            {
                ModelState.AddModelError("", "Medium ist schon verliehen");
                return View(media);
            }

            media.User = LoggedInUser;
            while (LoggedInUser.Orders.Remove(media.Work))
            {
            }
            db.SaveChanges();
            return View(media);
        }

        public ActionResult Return(int? id)
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
            if (!setUser()) return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            if (media.User == null)
            {
                ModelState.AddModelError("", "Medium ist nicht ausgeliehen.");
            }
            if (media.User != LoggedInUser)
            {
                ModelState.AddModelError("", "Medium ist nicht an Sie ausgeliehen.");
            }

            if (!ModelState.Values.SelectMany(v => v.Errors).Any())
            {
                media.User = null;
                db.SaveChanges();
            }
            return View(media);
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
