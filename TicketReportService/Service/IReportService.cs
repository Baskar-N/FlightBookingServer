using System.Collections.Generic;

namespace TicketReportService.Services
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName, Dictionary<string, object> dataSource);
    }
}