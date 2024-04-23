using Project_Pinterest.Payloads.DataResponses.DataUser;

namespace Project_Pinterest.Payloads.DataResponses.DataPost
{
    public class DataResponsePost : DataResponseBase
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DataResponseUser DataResponseUser { get; set; } 
        public int? NumberOfLikes { get; set; }
        public int? NumberOfComments { get; set; }
        public string PostStatusName { get; set; }
        public IQueryable<DataResponseComment>? DataResponseComments { get; set; }
        public IQueryable<DataResponseLike>? DataResponseLikes { get; set; }
    }
}
