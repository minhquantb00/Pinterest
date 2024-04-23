using Project_Pinterest.Entities;

namespace Project_Pinterest.Payloads.DataResponses.DataPost
{
    public class DataResponseLike : DataResponseBase
    {
        public string FullName { get; set; }
        public DateTime LikeTime { get; set; }
        public bool? Unlike { get; set; }
    }
}
