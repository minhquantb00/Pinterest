namespace Project_Pinterest.Payloads.DataResponses.DataPost
{
    public class DataResponseLikeComment : DataResponseBase
    {
        public string FullName { get; set; }
        public DateTime LikeTime { get; set; }
        public bool? Unlike { get; set; }
    }
}
