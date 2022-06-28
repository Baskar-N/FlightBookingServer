using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApiService.Models
{
    public class ScheduleDto
    {
        public int ScheduleRecId { get; set; }

        public int AirlineId { get; set; }

        public string FlightNumber { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public int ScheduledDaysRecId { get; set; }

        public string InstrumentUsed { get; set; }

        public int Bcs { get; set; }

        public int NonBcs { get; set; }

        public double TicketCost { get; set; }

        public int Rows { get; set; }

        public int MealTypeRecId { get; set; }
    }
}
