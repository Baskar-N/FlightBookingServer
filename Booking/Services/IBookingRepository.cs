using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApiService.Services
{
    public interface IBookingRepository
    {
        Booking AddBooking(Booking booking);

        Booking CancelBooking(string ticketPnr);

        IList<Booking> GetBookingHistory(string emailId);

        Booking GetBookingDetails(string ticketPnr);
    }
}
