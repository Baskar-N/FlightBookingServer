using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketReportService.Models
{
    public class ScheduledDays
    {
        [Key]
        public int ScheduledDaysRecId { get; set; }

        public string ScheduledType { get; set; }

        public bool IsActive { get; set; }
    }
}
