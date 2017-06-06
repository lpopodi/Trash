using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using TrashCollector.Models;

[assembly: OwinStartupAttribute(typeof(TrashCollector.Startup))]
namespace TrashCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
            CheckUpdateNextPickup();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

  
            if (!roleManager.RoleExists("Admin"))
            {
 
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);             

                var user = new ApplicationUser();
                user.UserName = "superadmin";
                user.Email = "lapopodi@gmail.com";

                string userPWD = "Sup3r@dm1nU$eR";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);

            }
  
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
        }

        public void CheckUpdateNextPickup()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var todaysDate = DateTime.Today;
            var getSchedules = db.Customers.ToList();
            foreach (var schedule in getSchedules)
            {
                if (todaysDate > schedule.DefaultPickupDay)
                {
                    var newDate = schedule.DefaultPickupDay.AddDays(7);
                    if (schedule.VacationStartDate != null && schedule.VacationEndDate != null)
                    {
                        if (newDate >= schedule.VacationStartDate && newDate < schedule.VacationEndDate)
                        {
                            do
                            {
                                newDate = newDate.AddDays(7);
                            }
                            while (newDate < schedule.VacationEndDate);
                        }
                    }
                    schedule.DefaultPickupDay = newDate;
                    db.SaveChanges();
                }
            }
        }

        

    }
}
