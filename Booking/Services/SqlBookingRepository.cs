using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApiService.Services
{
    public class SqlBookingRepository : IBookingRepository
    {
        private readonly AppDbContext _appDbContext;

        public SqlBookingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Booking AddBooking(Booking booking)
        {
            booking.CreatedDate = DateTime.Now;
            booking.IsActive = true;
            booking.TicketPnr = $"PNR-{new Random().Next()}";

            _appDbContext.Booking.Add(booking);
            _appDbContext.SaveChanges();

            // Reduce the seats count in inventory
            QueueProducer.UpdateTicketCount(
                booking.ScheduleRecId,
                booking.IsBcs,
                booking.NumberOfSeats,
                true
            );

            return booking;
        }

        public Booking CancelBooking(string ticketPnr)
        {
            var bookingDetails = _appDbContext.Booking.FirstOrDefault(item => item.TicketPnr == ticketPnr 
                                        && item.IsActive && item.TicketCancelDate == null);

            if(bookingDetails != null)
            {
                bookingDetails.IsActive = false;
                bookingDetails.TicketCancelDate = DateTime.Now;

                // Increase the seats count in inventory
                QueueProducer.UpdateTicketCount(
                    bookingDetails.ScheduleRecId,
                    bookingDetails.IsBcs,
                    bookingDetails.NumberOfSeats
                );

                _appDbContext.SaveChanges();
            }

            return bookingDetails;
        }

        public Booking GetBookingDetails(string ticketPnr)
        {
            //var bookingDetails = _appDbContext.Booking.FirstOrDefault(item => item.TicketPnr == ticketPnr);
            var bookingDetails = (from bk in _appDbContext.Booking
                                  where bk.TicketPnr == ticketPnr
                                  select new Booking
                                     {
                                         BookingRecId = bk.BookingRecId,
                                         ScheduleRecId = bk.ScheduleRecId,
                                         EmailId = bk.EmailId,
                                         Name = bk.Name,
                                         NumberOfSeats = bk.NumberOfSeats,
                                         IsBcs = bk.IsBcs,
                                         TicketPnr = bk.TicketPnr,
                                         IsActive = bk.IsActive,
                                         CreatedDate = bk.CreatedDate,
                                         TicketCancelDate = bk.TicketCancelDate,
                                         Passenger = bk.Passenger,
                                         Schedule = bk.Schedule
                                     }).FirstOrDefault();

            return bookingDetails;
        }

        public IList<Booking> GetBookingHistory(string emailId)
        {
            //var history = _appDbContext.Booking.Where(item => item.EmailId == emailId).ToList();

            var history = (from bk in _appDbContext.Booking
                          where bk.EmailId == emailId
                          select new Booking {
                              BookingRecId = bk.BookingRecId,
                              ScheduleRecId = bk.ScheduleRecId,
                              EmailId = bk.EmailId,
                              Name = bk.Name,
                              NumberOfSeats = bk.NumberOfSeats,
                              IsBcs = bk.IsBcs,
                              TicketPnr = bk.TicketPnr,
                              IsActive = bk.IsActive,
                              CreatedDate = bk.CreatedDate,
                              TicketCancelDate = bk.TicketCancelDate,
                              Passenger = bk.Passenger,
                              Schedule = bk.Schedule,
                              Airline = bk.Schedule.Airline
                          }).ToList();

            return history;
        }
    }
}
