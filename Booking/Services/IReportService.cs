using System.Collections.Generic;

namespace BookingApiService.Services
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName, Dictionary<string, object> dataSource);
    }
}