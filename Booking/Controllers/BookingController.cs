using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels.Models;
using BookingApiService.Services;
using SharedModels.Utils;
using Newtonsoft.Json;
using BookingApiService.DtoModel;

namespace BookingApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpPost("AddBooking")]
        public ActionResult AddBookingTicket(BookingDto bookingDto)
        {
            try
            {
                if (bookingDto == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                var booking = new Booking
                {
                    ScheduleRecId = bookingDto.ScheduleRecId,
                    EmailId = bookingDto.EmailId,
                    Name = bookingDto.Name,
                    IsBcs = bookingDto.IsBcs,
                    DiscountRecId = bookingDto.DiscountRecId,
                    NumberOfSeats = bookingDto.NumberOfSeats,
                    Passenger = bookingDto.Passenger,
                    ReturnSheduleRecId = bookingDto.ReturnScheduleRecId
                };

                // update data into database
                var bookingInfo = _bookingRepository.AddBooking(booking);

                return Ok(new ApiResponseStatus
                {
                    StatusCode = StatusCodes.Status201Created,
                    Status = $"Flight ticket booked successfully(TicketPNR : {bookingInfo.TicketPnr})."
                });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("CancelTicket")]
        public ActionResult CancelTicket(string ticketPnr)
        {
            try
            {
                if (ticketPnr == null || ticketPnr == "")
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                // update data into database
                var bookingInfo = _bookingRepository.CancelBooking(ticketPnr);

                return Ok(new ApiResponseStatus
                {
                    StatusCode = StatusCodes.Status200OK,
                    Status = $"Flight ticket has been cancelled successfully(TicketPNR : {bookingInfo.TicketPnr})."
                });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }
    }
}
