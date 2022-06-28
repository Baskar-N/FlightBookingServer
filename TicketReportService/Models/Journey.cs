using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketReportService.Models
{
    public class Journey
    {
        [Key]
        public int JourneyRecId { get; set; }

        [ForeignKey("Passenger")]
        [Required]
        public int PassengerRecId { get; set; }

        [Required]
        public int MealTypeRecId { get; set; }

        [Required]
        public int SeatNunber { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual Passenger Passenger { get; set; }
    }
}
