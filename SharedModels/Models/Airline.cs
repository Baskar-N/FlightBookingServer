using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedModels.Models
{
    public class Airline
    {
        [Key]
        public int AirlineId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public int ContactNumber { get; set; }

        public string ContactAddress { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
