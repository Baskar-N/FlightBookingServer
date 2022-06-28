using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketReportService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TicketReportService.Services;

namespace TicketReportService.Controllers
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

        [HttpGet("DownloadTicket")]
        public ActionResult DownloadReport(string ticketPnr)
        {
            try
            {
                if (ticketPnr == null || ticketPnr == "")
                {
                    return BadRequest(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                var reportName = "TicketReport";

                var bookingInfo = _bookingRepository.GetBookingDetails(ticketPnr);

                var returnString = _reportService.GenerateReportAsync(reportName, bookingInfo);

                return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + ".pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Status = ex.Message
                });
            }
        }
    }
}
