using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketReportService.Models
{
    public class Meal
    {
        [Key]
        public int MealTypeRecId { get; set; }

        public string MealType { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
