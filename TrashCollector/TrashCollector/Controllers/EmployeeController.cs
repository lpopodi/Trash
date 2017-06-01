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

        public PartialViewResult DisplayStops(string zipInput, DateTime? dateInput)
        {
            return PartialView(db.Customers.Where(p => p.Schedule.DefaultPickupDay == dateInput && p.ZipCode == zipInput).ToList());
        }

        public PartialViewResult ShowMap()
        {
            return PartialView();
        }
    }
}