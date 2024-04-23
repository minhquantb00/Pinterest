using Project_Pinterest.Constants;
using Project_Pinterest.Entities;

namespace Project_Pinterest.Payloads.DataResponses.DataReport
{
    public class DataResponseReport
    {
        public int? PostId { get; set; }
        public string UserReportedName { get; set; }
        public string UserReportName { get; set; }
        public string Reason { get; set; }
        public DateTime CreateAt { get; set; }
        public ReportType ReportType { get; set; }
    }
}
