using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedModels.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleRecId { get; set; }

        [ForeignKey("Airline")]
        public int AirlineId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FlightNumber { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        [Required]
        public DateTime? StartDateTime { get; set; }

        [Required]
        public DateTime? EndDateTime { get; set; }

        [Required]
        public int ScheduledDaysRecId { get; set;}

        public string InstrumentUsed { get; set; }

        public int Bcs { get; set; }

        public int NonBcs { get; set; }

        public double TicketCost { get; set; }

        public int Rows { get; set; }

        [Required]
        public int MealTypeRecId { get; set; }

        public virtual Airline Airline { get; set; }

        [NotMapped]
        public string MealType { get; set; }

        [NotMapped]
        public string ScheduleType { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
