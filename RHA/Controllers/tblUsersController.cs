using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RHA.Models;

namespace RHA.Controllers
{
    public class tblUsersController : Controller
    {
        private RHASTOREEntities db = new RHASTOREEntities();

        // GET: tblUsers
        [Authorize]
        public ActionResult Index()
        {
            var tblUsers = db.tblUsers.Include(t => t.tblRole);
            return View(tblUsers.ToList());
        }

        // GET: tblUsers/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: tblUsers/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserRole = new SelectList(db.tblRoles, "ID", "UserRole");
            return View();
        }

        // POST: tblUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LoginId,Password,FirstName,LastName,Address,Gender,Phonenumber,UserRole")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserRole = new SelectList(db.tblRoles, "ID", "UserRole", tblUser.UserRole);
            return View(tblUser);
        }
        [Authorize]
        // GET: tblUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRole = new SelectList(db.tblRoles, "ID", "UserRole", tblUser.UserRole);
            return View(tblUser);
        }

        // POST: tblUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LoginId,Password,FirstName,LastName,Address,Gender,Phonenumber,UserRole")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserRole = new SelectList(db.tblRoles, "ID", "UserRole", tblUser.UserRole);
            return View(tblUser);
        }
        [Authorize]
        // GET: tblUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }
        [Authorize]
        // POST: tblUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            db.tblUsers.Remove(tblUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AdminDashBoard()
        {
            var ISAdmin = User.IsInRole("1");
            if (ISAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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