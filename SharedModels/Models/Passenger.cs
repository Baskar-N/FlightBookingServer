using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedModels.Models
{
    public class Passenger
    {
        [Key]
        public int PassengerRecId { get; set; }

        [ForeignKey("Booking")]
        [Required]
        public int BookingRecId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Journey Journey { get; set; }

        public virtual ReturnJourney ReturnJourney { get; set;}

        public bool IsActive { get; set; } = true;
    }
}
