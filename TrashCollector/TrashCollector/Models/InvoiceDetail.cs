using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int LineId { get; set; }
        public string LineItem { get; set; }
        public string LineDate { get; set; }
        public decimal LinePrice { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}