namespace Project_Pinterest.Payloads.DataResponses.DataPost
{
    public class DataResponseComment : DataResponseBase
    {
        public string FullName { get; set; }
        public string Content { get; set; }
        public int? NumberOfLikes { get; set; }
        public DateTime CreateAt { get; set; }
        public IQueryable<DataResponseLikeComment> DataResponseLikeComments { get; set; }
    }
}
