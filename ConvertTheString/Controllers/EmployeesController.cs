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
    public class EmployeesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Employees
        public ActionResult Index(int? locationId, string sortBy)
        {
            ViewBag.LocationID = locationId;
            ViewBag.SortLastName = string.IsNullOrEmpty(sortBy) ? "LastName desc" : "";
            ViewBag.SortFirstName = sortBy == "FirstName" ? "FirstName desc" : "FirstName";

            var tblEmployees = db.tblEmployees.Include(t => t.tblLocation).Where(i => i.employeeType == locationId);


            switch (sortBy)
            {
                case "LastName desc":
                    tblEmployees = tblEmployees.OrderByDescending(x => x.lastname);
                    break;
                case "FirstName desc":
                    tblEmployees = tblEmployees.OrderByDescending(x => x.firstname);
                    break;
                case "FirstName":
                    tblEmployees = tblEmployees.OrderBy(x => x.firstname);
                    break;
                default:
                    tblEmployees = tblEmployees.OrderBy(x => x.lastname);
                    break;
            }


            return View(tblEmployees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.employeeType = new SelectList(db.tblLocations, "Id", "location");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,firstname,lastname,employeeType")] tblEmployee tblEmployee)
        {
            if (ModelState.IsValid)
            {
                db.tblEmployees.Add(tblEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employeeType = new SelectList(db.tblLocations, "Id", "location", tblEmployee.employeeType);
            return View(tblEmployee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeType = new SelectList(db.tblLocations, "Id", "location", tblEmployee.employeeType);
            return View(tblEmployee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,firstname,lastname,employeeType")] tblEmployee tblEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeType = new SelectList(db.tblLocations, "Id", "location", tblEmployee.employeeType);
            return View(tblEmployee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            db.tblEmployees.Remove(tblEmployee);
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
