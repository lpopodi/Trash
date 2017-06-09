using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,StreetAddress,City,State,ZipCode,Phone,Email,DefaultPickupDay,ExtraPickupDay,VacationStartDate,VacationEndDate,BillDate,AccountBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var holder = User.Identity.GetUserId();
                var user = db.Users.Where(u => u.Id == holder).FirstOrDefault();
                customer.userId = user;
                customer.Email = user.Email;
                db.Customers.Add(customer);
                db.SaveChanges();
                return View("Home");
            }

            return View("Home");
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,StreetAddress,City,State,ZipCode,Phone,Email,DefaultPickupDay,ExtraPickupDay,VacationStartDate,VacationEndDate,BillDate,AccountBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult MyAccount()
        {
            var currentUserId = User.Identity.GetUserId();
            var thisCustomer = db.Customers.Where(u => u.userId.Id == currentUserId).First();
            var thisId = thisCustomer.Id;
            Customer customer = db.Customers.Find(thisId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult EditAccountDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountDetails([Bind(Include = "Id,FirstName,LastName,StreetAddress,City,State,ZipCode,Phone,Email,DefaultPickupDay,ExtraPickupDay,VacationStartDate,VacationEndDate,BillDate,AccountBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyAccount", "Customers");
            }
            return RedirectToAction("MyAccount", "Customers");
        }

         // GET: Customers/Edit/5
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult EditBillDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBillDate([Bind(Include = "Id,FirstName,LastName,StreetAddress,City,State,ZipCode,Phone,Email,DefaultPickupDay,ExtraPickupDay,VacationStartDate,VacationEndDate,BillDate,AccountBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyAccount", "Customers");
            }
            return RedirectToAction("MyAccount", "Customers");
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult EditDefaultDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDefaultDate([Bind(Include = "Id,FirstName,LastName,StreetAddress,City,State,ZipCode,Phone,Email,DefaultPickupDay,ExtraPickupDay,VacationStartDate,VacationEndDate,BillDate,AccountBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyAccount", "Customers");
            }
            return RedirectToAction("MyAccount", "Customers");
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult EditExtraDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExtraDate([Bind(Include = "Id,FirstName,LastName,StreetAddress,City,State,ZipCode,Phone,Email,DefaultPickupDay,ExtraPickupDay,VacationStartDate,VacationEndDate,BillDate,AccountBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyAccount", "Customers");
            }
            return RedirectToAction("MyAccount", "Customers");
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult EditVacationDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVacationDate([Bind(Include = "Id,FirstName,LastName,StreetAddress,City,State,ZipCode,Phone,Email,DefaultPickupDay,ExtraPickupDay,VacationStartDate,VacationEndDate,BillDate,AccountBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyAccount", "Customers");
            }
            return RedirectToAction("MyAccount", "Customers");
        }

    }
}
