using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Invoice
    {
        [Key]
        [Display(Name = "Invoice Id")]
        public Guid InvoiceId { get; set; }
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        public virtual Customer Customer { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}