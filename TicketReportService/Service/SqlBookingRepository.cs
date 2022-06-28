using TicketReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketReportService.Services
{
    public class SqlBookingRepository : IBookingRepository
    {
        private readonly AppDbContext _appDbContext;

        public SqlBookingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Dictionary<string, object> GetBookingDetails(string ticketPnr)
        {
            //var bookingDetails = _appDbContext.Booking.FirstOrDefault(item => item.TicketPnr == ticketPnr);
            //var passengerDetails = _appDbContext.Passenger.Where(item => item.BookingRecId == bookingDetails.BookingRecId);
            //var scheduleDetails = _appDbContext.

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
                                      Schedule = bk.Schedule,
                                      Airline = bk.Schedule.Airline
                                  }).FirstOrDefault();

            //var passengerDetails = _appDbContext.Passenger.Where(item => item.BookingRecId == bookingDetails.BookingRecId).ToList();
            //var scheduleDetails = _appDbContext.Schedule.Where(item => item.ScheduleRecId == bookingDetails.ScheduleRecId).ToList();
            //var airlineDetails = _appDbContext.Airline.Where(item => item.AirlineId == scheduleDetails[0].AirlineId).ToList();

            var datasource = new Dictionary<string, object>();

            datasource.Add("Airline", new List<Airline>{
                bookingDetails.Airline
            });
            datasource.Add("Passenger", bookingDetails.Passenger);
            datasource.Add("Schedule", new List<Schedule> { 
                bookingDetails.Schedule 
            });
            datasource.Add("Journey", new List<Journey>());
            datasource.Add("ReturnJourney", new List<ReturnJourney>());

            return datasource;
        }
    }
}
