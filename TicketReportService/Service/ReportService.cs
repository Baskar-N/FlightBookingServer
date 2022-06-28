using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TicketReportService.Services
{
    public class ReportService : IReportService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public byte[] GenerateReportAsync(string reportName, Dictionary<string, object> dataSourceCollection)
        {
            string fileDirPath = $"{_hostingEnvironment.WebRootPath}\\Reports\\{reportName}.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            LocalReport report = new LocalReport(fileDirPath);

            foreach (var dataSource in dataSourceCollection)
            {
                report.AddDataSource(dataSource.Key, dataSource.Value);
            }

            var result = report.Execute(RenderType.Pdf, 1, parameters);

            return result.MainStream;
        }
    }
}
