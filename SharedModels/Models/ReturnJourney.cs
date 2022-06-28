using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedModels.Models
{
    public class ReturnJourney
    {
        [Key]
        public int ReturnJourneyRecId { get; set; }

        [ForeignKey("Passenger")]
        [Required]
        public int PassengerRecId { get; set; }

        [Required]
        public int MealTypeRecId { get; set; }

        [Required]
        public string SeatNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual Passenger Passenger { get; set; }
    }
}
