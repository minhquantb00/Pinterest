using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Project_Pinterest.Payloads.DataRequests.UserRequests
{
    public class Request_UpdateAvatar
    {
        [DataType(DataType.Upload)]
        public IFormFile Avatar { get; set; }
    }
}
