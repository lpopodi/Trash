using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string zipInput = "53132";
        DateTime? dateInput = DateTime.Today;

        // GET: Employee
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Employee")]
        public ActionResult CustomerIndex()
        {
            return View(db.Customers.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult DisplayStops(string zipInput, DateTime dateInput)
        {
            List<Customer> theseStops = new List<Customer>();
            var dateCheck = dateInput.ToShortDateString();
            var checkStops = db.Customers.ToList();
            foreach (var stop in checkStops)
            {
                var dateString = stop.DefaultPickupDay.ToShortDateString();
                if (dateString == dateCheck && stop.ZipCode == zipInput)
                {
                    theseStops.Add(stop);
                }
                if (stop.ExtraPickupDay.HasValue)
                {
                    var equal = Nullable.Compare<DateTime>(dateInput, stop.ExtraPickupDay);
                    if (equal == 0)
                    {
                        theseStops.Add(stop);
                    }
                }
            }
            return View(theseStops);
        }

        [Authorize(Roles = "Admin,Employee")]
        public void InvoiceAccounts()
        {
            var todayDate = DateTime.Now;
            var today = todayDate.Date;
            var startDate = DateTime.Now.AddMonths(-1);
            var accountsToInvoice = db.Customers.ToList();
            foreach (var customer in accountsToInvoice)
            {
                var customerBillDate = customer.BillDate;
                var billDate = customerBillDate.Date;
                if (billDate == today)
                {
                    if (customer.AccountBalance == null)
                    {
                        customer.AccountBalance = 0;
                    }
                    var customerPickups = db.Pickups.Where(r => r.Customer.Id == customer.Id).ToList();
                    IEnumerable<Pickup> filterPickups = customerPickups;
                    var billableDates = filterPickups.Where(r => r.PickupDate >= startDate && r.PickupDate <= todayDate).ToList();
                    foreach (var pickup in billableDates)
                    {
                        customer.AccountBalance += 20;
                    }
                    db.SaveChanges();
                }
            }
            Response.Redirect(Request.UrlReferrer.ToString());
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Invoices have been run');", true);
            //Response.Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = "Admin,Employee")]
        public void ApplyPickup(int customerId)
        {
            var holder = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == holder).FirstOrDefault();
            var thisCustomer = db.Customers.Where(c => c.Id == customerId).FirstOrDefault();
            Pickup pickup = new Pickup();
            pickup.userId = user;
            pickup.PickupDate = DateTime.Now;
            pickup.Customer = thisCustomer;
            thisCustomer.Pickups.Add(pickup);
            db.Pickups.Add(pickup);
            db.SaveChanges();
            Response.Redirect("Index");
        }



    }
}