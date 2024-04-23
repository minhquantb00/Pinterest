using Project_Pinterest.Constants;

namespace Project_Pinterest.Entities
{
    
    public class Report : BaseEntity
    {
        public int? PostId { get; set; }
        public int? UserReportId { get; set; }
        public int? UserReportedId { get; set; }
        public string Reason { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User? UserReport { get; set; }
        public virtual User? UserReported { get; set; }
    }
}
