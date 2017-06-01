using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        public Customer()
        {
            Invoices = new List<Invoice>();
            Pickups = new List<Pickup>();
        }

        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }

        [Required, StringLength(5)]
        [Display(Name = "Zip")]
        public string ZipCode { get; set; }

        [StringLength(10)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ApplicationUser userId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Pickup> Pickups { get; set; }
    }
}