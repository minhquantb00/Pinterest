namespace Project_Pinterest.Payloads.DataRequests.ReportRequests
{
    public class Request_CreateReportUser
    {
        public int UserReportedId { get; set; }
        public string Reason { get; set; }
    }
}
