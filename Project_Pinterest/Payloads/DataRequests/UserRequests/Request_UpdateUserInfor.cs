using System.ComponentModel.DataAnnotations;

namespace Project_Pinterest.Payloads.DataRequests.UserRequests
{
    public class Request_UpdateUserInfor
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Avatar { get; set; }
        public string Email { get; set; }
    }
}
