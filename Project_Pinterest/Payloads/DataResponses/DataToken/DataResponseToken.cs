using Project_Pinterest.Payloads.DataResponses.DataUser;

namespace Project_Pinterest.Payloads.DataResponses.DataToken
{
    public class DataResponseToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DataResponseUser DataResponseUser { get; set; }
    }
}
