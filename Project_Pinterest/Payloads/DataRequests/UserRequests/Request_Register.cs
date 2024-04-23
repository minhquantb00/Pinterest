namespace Project_Pinterest.Payloads.DataRequests.UserRequests
{
    public class Request_Register
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
