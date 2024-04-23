namespace Project_Pinterest.Payloads.DataRequests.PostRequests
{
    public class Request_CreateComment
    {
        public int PostId { get; set; }
        public string Comment { get; set; }
    }
}
