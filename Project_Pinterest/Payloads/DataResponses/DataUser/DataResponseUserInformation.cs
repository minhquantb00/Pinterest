using Project_Pinterest.Payloads.DataResponses.DataPost;

namespace Project_Pinterest.Payloads.DataResponses.DataUser
{
    public class DataResponseUserInformation
    {
        public int PostNumber { get; set; }
        public int NumberOfFollower { get; set; }
        public int NumberOfFollowing { get; set;}
        public IQueryable<DataResponsePost> Posts { get; set; }
    }
}
