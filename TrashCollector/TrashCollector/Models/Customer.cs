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

        [Display(Name = "Default Pickup Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DefaultPickupDay { get; set; }

        [Display(Name = "One Time Extra Pickup Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExtraPickupDay { get; set; }

        [Display(Name = "Start Vacation")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VacationStartDate { get; set; }

        [Display(Name = "End Vacation")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public DateTime? VacationEndDate { get; set; }

        [Display(Name = "Billing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BillDate { get; set; }

        [Display(Name = "Account Balance")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? AccountBalance { get; set; }

        public virtual ApplicationUser userId { get; set; }

        public ICollection<Pickup> Pickups { get; set; }
    }
}