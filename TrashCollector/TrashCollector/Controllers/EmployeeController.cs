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
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult MyAction(MyViewModel model)
        //{
        //    return Content(result);
        //}

        //[HttpPost]
        //public ActionResult MyAction(Model model, string zipInput, DateTime? dateInput)
        //{
        //    var pickups = db.Customers.Where(p => p.Schedule.DefaultPickupDay == dateInput && p.ZipCode == zipInput).ToList();
        //    return Content(pickups);
        //}

        [HttpGet]
        public PartialViewResult DisplayStops()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult DisplayStops(string zipInput, DateTime? dateInput)
        {
            return PartialView(db.Customers.Where(p => p.Schedule.DefaultPickupDay == dateInput && p.ZipCode == zipInput).ToList());
        }

        //public PartialViewResult DisplayStops()
        //{
        //    return PartialView(db.Customers.ToList());
        //}

        public PartialViewResult ShowMap()
        {
            return PartialView();
        }

        public void InvoiceAccounts()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var todayDate = DateTime.Now;
            var startDate = DateTime.Now.AddMonths(-1);
            var accountsToInvoice = db.Customers.ToList();
            foreach (var customer in accountsToInvoice)
            {
                var customerBillDate = customer.Schedule.BillDate;
                if (customerBillDate == todayDate)
                {
                    Invoice invoice = new Invoice();
                    invoice.InvoiceId = Guid.NewGuid();
                    invoice.InvoiceDate = DateTime.Now;
                    var customerPickups = customer.Pickups.Where(d => d.PickupDate >= startDate && d.PickupDate <= todayDate).ToList();
                    foreach (var pickup in customerPickups)
                    {
                        int incNum = 0;
                        string serviceDate = pickup.PickupDate.ToShortDateString();
                        invoice.InvoiceDetails.Add(new InvoiceDetail() { LineId = invoice.InvoiceId + "-I" + incNum++, LineItem = "Trash Pickup", LineDate = serviceDate, LinePrice = 20 });
                        invoice.Total = invoice.Total += 20;
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}