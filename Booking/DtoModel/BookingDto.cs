using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApiService.DtoModel
{
    public class BookingDto
    {
        public int BookingRecId { get; set; }

        public int ScheduleRecId { get; set; }

        public int? ReturnScheduleRecId { get; set; }

        public string EmailId { get; set; }

        public string Name { get; set; }

        public int NumberOfSeats { get; set; }

        public bool IsBcs { get; set; }

        public string TicketPnr { get; set; }

        public bool IsActive { get; set; }

        public int? DiscountRecId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? TicketCancelDate { get; set; }

        public virtual ICollection<Passenger> Passenger { get; set; }
    }
}
