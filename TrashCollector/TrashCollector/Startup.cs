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
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "superadmin";
                user.Email = "lapopodi@gmail.com";

                string userPWD = "Sup3r@dm1nU$eR";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
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
                    invoice.InvoiceDate = DateTime.Now;
                    var customerPickups = customer.Pickups.Where(d => d.PickupDate >= startDate && d.PickupDate <= todayDate).ToList();
                    foreach (var pickup in customerPickups)
                    {
                        //invoice.InvoiceDetails.Add(1, "Trash Pickup", pickup.PickupDate, 20);

                    }
                }
            }

        }
    }
}
