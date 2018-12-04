using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConvertTheString.Models;

namespace ConvertTheString.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Home
        public ActionResult Index(string sortBy)
        {

            ViewBag.SortID = string.IsNullOrEmpty(sortBy) ? "ID desc" : "";
            ViewBag.SortCreated = sortBy == "Created" ? "Created desc" : "Created";
            ViewBag.SortLocation = sortBy == "Location" ? "Location desc" : "Location";


            var locations = db.tblLocations.AsQueryable();

            switch (sortBy)
            {
                case "ID desc":
                    locations = locations.OrderByDescending(x => x.Id);
                    break;
                case "Created desc":
                    locations = locations.OrderByDescending(x => x.created);
                    break;
                case "Created":
                    locations = locations.OrderBy(x => x.created);
                    break;
                case "Location desc":
                    locations = locations.OrderByDescending(x => x.location);
                    break;
                case "Location":
                    locations = locations.OrderBy(x => x.location);
                    break;
                default:
                    locations = locations.OrderBy(x => x.Id);
                    break;
            }

            return View(locations.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLocation tblLocation = db.tblLocations.Find(id);
            if (tblLocation == null)
            {
                return HttpNotFound();
            }
            return View(tblLocation);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,created,location")] tblLocation tblLocation)
        {
            if (ModelState.IsValid)
            {
                db.tblLocations.Add(tblLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblLocation);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLocation tblLocation = db.tblLocations.Find(id);
            if (tblLocation == null)
            {
                return HttpNotFound();
            }
            return View(tblLocation);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,created,location")] tblLocation tblLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblLocation);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLocation tblLocation = db.tblLocations.Find(id);
            if (tblLocation == null)
            {
                return HttpNotFound();
            }
            return View(tblLocation);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLocation tblLocation = db.tblLocations.Find(id);
            db.tblLocations.Remove(tblLocation);
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
