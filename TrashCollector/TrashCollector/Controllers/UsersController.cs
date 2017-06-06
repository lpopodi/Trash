using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public ActionResult Index()
        {
            var user = User.Identity;
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var s = UserManager.GetRoles(user.GetUserId());

            do
            {
                if (s[0].ToString() == "Admin")
                {
                    //UserStatus = "Admin";
                    return View("Admin", "User");
                }
                else if (s[0].ToString() == "Employee")
                {
                    //UserStatus = "Employee";
                    return RedirectToAction("Index", "Employee");
                }
                else if (s[0].ToString() == "Customer")
                {
                    //UserStatus = "Customer";
                    return RedirectToAction("Create", "Customers");
                }
                else
                {
                    //UserStatus = "na";
                    return View("Index", "Home");
                }
            }
            while (User.Identity.IsAuthenticated);
        }
    }
}