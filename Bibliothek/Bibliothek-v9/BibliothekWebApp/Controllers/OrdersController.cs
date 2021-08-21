using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BibliothekLib;
using BibliothekWebApp.Models;
using Microsoft.AspNet.Identity;
using EntityState = System.Data.Entity.EntityState;

namespace BibliothekWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private BibliothekModelContainer db = new BibliothekModelContainer();

        private class OrderOrError
        {
            public ActionResult Error;
            public Order Order;
            public OrderOrError (ActionResult error)
            {
                Error = error;
            }

            public OrderOrError(HttpStatusCode errorStatus)
            {
                Error = new HttpStatusCodeResult(errorStatus);
            }
            public OrderOrError(Order order)
            {
                Order = order;
            }
        }
        private OrderOrError GetOrderOrError(int? userId, int? workId)
        {
            var userName = User.Identity.GetUserName();
            LoggedInUser = db.Users.FirstOrDefault(
                u => u.EMail == userName);
            ViewBag.User = LoggedInUser;
            if (LoggedInUser == null)
                return new OrderOrError(HttpStatusCode.Unauthorized);

            if (userId == null)
                return new OrderOrError(HttpStatusCode.BadRequest);

            if (workId == null)
                return new OrderOrError(HttpStatusCode.BadRequest);

            Work work = db.Works.Find(workId);
            if (work == null)
                return new OrderOrError(HttpStatusCode.BadRequest);

            User user = db.Users.Find(userId);
            if (user == null)
                return new OrderOrError(HttpStatusCode.BadRequest);

            return new OrderOrError(
                new Order() { User = user, Work = work });

        }


        // GET: Orders
        public ActionResult Index()
        {
            var orders =
                from user in db.Users
                from work in db.Works
                where user.Orders.Contains(work)
                select new Order() {User = user, Work = work};

            return View(orders.ToList());
        }

        public ActionResult SelectUser(int? workId)
        {
            if (workId != null)
            {
                var work = db.Works.Find(workId);
                if (work == null) 
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Work = work;
            }
            return View(db.Users.Where(u => u.RoleCostumer).ToList());
        }

        public ActionResult SelectWork(int? userId)
        {
            if (userId != null)
            {
                var user = db.Users.Find(userId);
                if (user == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Costumer = user;
            }
            return View(db.Works.ToList());
        }


        private User LoggedInUser = null;


        // GET: Orders/Details/5
        public ActionResult Details(int? userId, int? workId)
        {
            var tmp = GetOrderOrError(userId, workId);
            if (tmp.Error != null) return tmp.Error;
            return View(tmp.Order);
        }

        // GET: Orders/Create
        public ActionResult Create(int? userId, int? workId)
        {
            if (userId != null && workId != null)
                return Create(new Order {UserId = userId, WorkId = workId});

            ViewBag.UserId = new SelectList(db.Users, "Id", "AsString");
            ViewBag.WorkId = new SelectList(db.Works, "Id", "AsString");
            return View();
        }


        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,WorkId")] Order order)
        {
            var tmp = GetOrderOrError(order.UserId, order.WorkId);
            if (tmp.Error != null) return tmp.Error;

            if (ModelState.IsValid)
            {
                tmp.Order.User.Orders.Add(tmp.Order.Work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "AsString", order.UserId);
            ViewBag.WorkId = new SelectList(db.Works, "Id", "AsString", order.WorkId);
            return View(order);
        }


        // GET: Orders/Delete/5
        public ActionResult Delete(int? userId, int? workId)
        {
            var tmp = GetOrderOrError(userId, workId);
            if (tmp.Error != null) return tmp.Error;
            return View(tmp.Order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? userId, int? workId)
        {
            var tmp = GetOrderOrError(userId, workId);
            if (tmp.Error != null) return tmp.Error;
            tmp.Order.User.Orders.Remove(tmp.Order.Work);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult QuickEdit()
        {
            var viewModel = new OrderSelectViewModel();
            var works = db.Works.ToList();
            foreach (var user in db.Users.Where(u => u.RoleCostumer))
            {
                var currentUserView = new UserOrderSelectViewModel
                {
                    UserId = user.Id,
                    DisplayName = user.Name + " " + user.Familyname
                };
                foreach (var work in works)
                {
                    var currentUserWorkView = new WorkUserOrderSelectViewModel
                    {
                        DisplayName = work.Titel + " von " + work.Author,
                        WorkId = work.Id,
                        IsSelected = user.Orders.Contains(work)
                    };
                    currentUserView.WorkUserOrderSelects.Add(currentUserWorkView);
                }
                viewModel.UserOrderSelects.Add(currentUserView);                
            }
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult QuickEdit(OrderSelectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int[] userIds = viewModel.UserOrderSelects
                    .Select(e => e.UserId??-1).ToArray();
                if (userIds.Any(v => v < 0))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                int[] workIds = viewModel.UserOrderSelects
                    .SelectMany(s => s.WorkUserOrderSelects
                        .Select(w => w.WorkId ?? -1))
                    .Distinct().ToArray();
                if (workIds.Any(v => v < 0))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                Dictionary<int, User> users = db.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToDictionary(k => k.Id);

                Dictionary<int, Work> works = db.Works
                    .Where(w => workIds.Contains(w.Id))
                    .ToDictionary(k => k.Id);

                foreach (var userSelect in viewModel.UserOrderSelects)
                {
                    User user;
                    if (!users.TryGetValue(userSelect.UserId??-1, out user))
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    userSelect.User = user;
                    foreach (var workSelect in userSelect.WorkUserOrderSelects)
                    {
                        Work work;
                        if (works.TryGetValue(workSelect.WorkId??-1, out work))
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        workSelect.Work = work;

                        if (workSelect.IsSelected)
                        {
                            if (!user.Orders.Contains(work)) user.Orders.Add(work);
                        }
                        else
                        {
                            if (user.Orders.Contains(work)) user.Orders.Remove(work);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
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
