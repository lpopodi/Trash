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
        public string LineId { get; set; }
        public string LineItem { get; set; }
        public string LineDate { get; set; }
        public decimal LinePrice { get; set; }

        public virtual Invoice Invoice { get; set; }

        //public InvoiceDetail(int LineId, string LineItem, string LineDate, decimal LinePrice)
        //{

        //}

        public InvoiceDetail()
        {
        }
    }
}