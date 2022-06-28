using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketReportService.Models
{
    public class Location
    {
        [Key]
        public int LocationRecId { get; set; }

        public string LocationName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
