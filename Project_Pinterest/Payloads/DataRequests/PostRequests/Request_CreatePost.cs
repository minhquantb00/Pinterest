using System.ComponentModel.DataAnnotations;

namespace Project_Pinterest.Payloads.DataRequests.PostRequests
{
    public class Request_CreatePost
    {
        public string Title { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
