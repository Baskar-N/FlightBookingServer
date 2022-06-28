using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModels.Models
{
    public class Discount
    {
        [Key]
        public int DiscountRecId { get; set; }

        public double DiscountAmount { get; set; }

        public string DiscountCode { get; set; }

        public bool IsActive { get; set; }
    }
}
