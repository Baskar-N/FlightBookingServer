using BookingApiService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels.Models;
using SharedModels.Utils;
using System;
using System.Collections.Generic;
using System.Data;

namespace BookingApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        private readonly IReportService _reportService;

        public TicketController(IBookingRepository bookingRepository, IReportService reportService)
        {
            _bookingRepository = bookingRepository;
            _reportService = reportService;
        }

        [HttpGet("GetHistory")]
        public ActionResult GetHistory(string emailId)
        {
            try
            {
                if (emailId == null || emailId == "")
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                // get data from database
                var bookingInfo = _bookingRepository.GetBookingHistory(emailId);

                return Ok(JsonConvert.SerializeObject(bookingInfo, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("GetTicketDetails")]
        public ActionResult GetTicketDetails(string ticketPnr)
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

                // get data from database
                var bookingInfo = _bookingRepository.GetBookingDetails(ticketPnr);

                return Ok(JsonConvert.SerializeObject(bookingInfo, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("DownloadReport")]
        public ActionResult DownloadReport(string ticketPnr)
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

                var reportName = "TicketReport";
                var airlines = new List<Airline>() { 
                    new Airline
                    {
                        AirlineId = 1,
                        Name = "Airline India",
                        ContactAddress = "India",
                        Logo = "airline.png"
                    },
                    new Airline
                    {
                        AirlineId = 1,
                        Name = "Airline India - sky",
                        ContactAddress = "Malasiya",
                        Logo = "malasiya.png"
                    }
                };

                var datasource = new Dictionary<string, object>();

                datasource.Add("Airline", airlines);
                datasource.Add("Passenger", new DataTable());
                datasource.Add("Schedule", new DataTable());

                var returnString = _reportService.GenerateReportAsync(reportName, datasource);

                return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + ".pdf");
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }
    }
}
