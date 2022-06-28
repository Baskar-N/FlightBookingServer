using TicketReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketReportService.Services
{
    public interface IBookingRepository
    {
        Dictionary<string, object> GetBookingDetails(string ticketPnr);
    }
}
