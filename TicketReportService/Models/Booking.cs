using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketReportService.Models
{
    public class Booking
    {
        [Key]
        public int BookingRecId { get; set; }

        [ForeignKey("Schedule")]
        [Required]
        public int ScheduleRecId { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int NumberOfSeats { get; set; }

        public bool IsBcs { get; set; }

        [Required]
        [MaxLength(20)]
        public string TicketPnr { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? TicketCancelDate { get; set; }

        public virtual ICollection<Passenger> Passenger { get; set; }

        public virtual Schedule Schedule { get; set; }

        public virtual Airline Airline { get; set; }
    }
}
